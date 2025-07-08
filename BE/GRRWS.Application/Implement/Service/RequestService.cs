using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Application.Interfaces;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.DTOs.Notification;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System.Collections.Concurrent;

namespace GRRWS.Application.Implement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        // Add the missing field declaration for _notificationService
        private readonly INotificationService _notificationService;


        public RequestService(IRequestRepository requestRepository, ITokenService tokenService, IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _requestRepository = requestRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result> GetAllAsync(int pageNumber, int pageSize, string? search, string? searchType)
        {
            var (requests, totalCount) = await _requestRepository.GetAllRequestAsync(pageNumber, pageSize, search, searchType);
            var dtos = new List<RequestDTO>();
            foreach (var r in requests.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }
            var response = new PagedResponse<RequestDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }
        public async Task<Result> GetRequestByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        string? search,
        string? searchType)
        {
            // 1. Call the updated repository method
            var (requests, totalCount) = await _requestRepository.GetRequestByUserIdAsync(
                userId,
                pageNumber,
                pageSize,
                search,
                searchType);

            // 2. Map to DTOs
            var dtos = new List<RequestDTO>();
            foreach (var r in requests.Where(r => !r.IsDeleted))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }

            // 3. Return flat, paginated list as usual
            var response = new PagedResponse<RequestDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        private async Task<RequestDTO> MapRequestToDTOAsync(Request r)
        {
            return new RequestDTO
            {
                Id = r.Id,
                ReportId = r.ReportId,
                DeviceId = r.DeviceId,
                DeviceName = r.Device?.DeviceName,
                DeviceCode = r.Device?.DeviceCode,
                PositionIndex = r.Device?.Position?.Index,
                ZoneName = r.Device?.Position?.Zone?.ZoneName,
                AreaName = r.Device?.Position?.Zone?.Area?.AreaName,
                RequestDate = r.CreatedDate,
                RequestTitle = r.RequestTitle,
                Description = r.Description,
                Status = r.Status.ToString(),
                Priority = r.Priority.ToString(),
                CreatedDate = r.CreatedDate,
                CreatedBy = r.RequestedById,
                ModifiedDate = r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                Issues = await MapIssuesWithImagesAsync(r.RequestIssues)
            };
        }

        private async Task<List<IssueDTO>> MapIssuesWithImagesAsync(ICollection<RequestIssue> requestIssues)
        {
            var issues = new List<IssueDTO>();
            foreach (var ri in requestIssues)
            {
                var imageUrls = new List<string>();
                if (ri.Images != null && ri.Images.Any())
                {
                    foreach (var image in ri.Images.Where(img => !img.IsDeleted))
                    {
                        var getImageRequest = new GetImageRequest(image.ImageUrl);
                        var imageResult = await _unitOfWork.FirebaseRepository.GetImageAsync(getImageRequest);
                        if (imageResult.Success && !string.IsNullOrEmpty(imageResult.ImageUrl))
                        {
                            imageUrls.Add(imageResult.ImageUrl);
                        }
                        // If image fetch fails, skip it (no need to fail the entire request)
                    }
                }

                issues.Add(new IssueDTO
                {
                    Id = ri.Issue.Id,
                    DisplayName = ri.Issue.DisplayName,
                    ImageUrls = imageUrls,

                });
            }
            return issues;
        }


        public async Task<Result> GetByIdAsync(Guid id)
        {
            var r = await _requestRepository.GetRequestByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));
            var dto = await MapRequestToDTOAsync(r);
            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> CreateAsync(CreateRequestDTO dto)
        {
            var request = new Request
            {
                Id = Guid.NewGuid(),
                //RequestTitle = dto.RequestTitle,
                //Description = dto.Description,
                //Status = dto.Status,
                //CreatedBy = dto.CreatedBy,
                CreatedDate = TimeHelper.GetHoChiMinhTime(),
                //DueDate = dto.DueDate,
                //Priority = dto.Priority,
                IsDeleted = false,
                RequestIssues = (dto.IssueIds ?? new List<Guid>()).Select(issueId => new RequestIssue
                {
                    IssueId = issueId
                }).ToList()
            };

            await _requestRepository.CreateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Test request created successfully!" });
        }

        public async Task<Result> UpdateAsync(UpdateRequestDTO dto, Guid id)
        {
            var r = await _requestRepository.GetRequestByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            var updatedRequest = new Request
            {
                Id = id,
                RequestTitle = dto.RequestTitle,
                Description = dto.Description,
                //Status = Enum.TryParse<Status>(dto.Status, out var status) ? status : Status.Pending,
                DueDate = dto.DueDate,
                //Priority = Enum.TryParse<Priority>(dto.Priority, out var priority) ? priority : Priority.Low,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = TimeHelper.GetHoChiMinhTime()
            };

            await _requestRepository.UpdateRequestAsync(updatedRequest, dto.IssueIds);
            return Result.SuccessWithObject(new { Message = "Request updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var r = await _requestRepository.GetByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            r.IsDeleted = true;
            //r.Status = "Delete";
            r.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            await _requestRepository.UpdateAsync(r);

            return Result.SuccessWithObject(new { Message = "Request canceled successfully!" });
        }

        public async Task<Result> GetRequestSummary()
        {
            var list = await _requestRepository.GetRequestSummaryAsync();
            if (list == null || !list.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not Found", "Not found any list"));
            return Result.SuccessWithObject(list);
        }

        public async Task<Result> GetRequestDetailWebByIdAsync(Guid requestId)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }
            var requestDetail = await _requestRepository.GetRequestDetailWebByIdAsync(requestId);




            if (requestDetail == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }
            return Result.SuccessWithObject(requestDetail);
        }

        public async Task<Result> GetErrorsForRequestDetailWebAsync(Guid requestId)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device does not exist."));
            }
            var errors = await _requestRepository.GetErrorsForRequestDetailWebAsync(requestId);
            if (errors == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No errors found for the request."));
            }
            return Result.SuccessWithObject(errors);
        }

        public async Task<Result> GetTasksForRequestDetailWebAsync(Guid requestId)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }
            var tasks = await _requestRepository.GetTasksForRequestDetailWebAsync(requestId);
            if (tasks == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No tasks found for the request."));
            }
            return Result.SuccessWithObject(tasks);
        }

        public async Task<Result> GetTechnicalIssuesForRequestDetailWebAsync(Guid requestId)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }

            var technicalIssues = await _requestRepository.GetTechnicalIssuesForRequestDetailWebAsync(requestId);
            if (technicalIssues == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No technical issues found for the request."));
            }

            return Result.SuccessWithObject(technicalIssues);
        }

        public async Task<Result> GetIssuesByRequestIdAsync(Guid requestId)
        {
            var requestIssues = await _requestRepository.GetIssuesByRequestIdAsync(requestId);


            var issues = await MapIssuesWithImagesAsync(requestIssues);
            return Result.SuccessWithObject(issues);
        }

        public async Task<Result> CancelRequestAsync(Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO.CancelRequestDTO dto, Guid userId)
        {
            var r = await _requestRepository.GetByIdAsync(dto.RequestId);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            r.Status = Status.Cancelled;
            r.RejectionReason = "Yêu cầu đã bị hủy với lý do: " + dto.Reason;
            r.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            r.ModifiedBy = userId;
            await _requestRepository.UpdateAsync(r);

            return Result.Success();
        }

        public Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CreateTestAsync(TestCreateRequestDTO dto, Guid userId)
        {
            if (dto.DeviceId == Guid.Empty || !await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(dto.DeviceId))
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device not found."));
            }

            if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device not found."));
            }
            var missingIssues = await _unitOfWork.IssueRepository.GetNotFoundIssueDisplayNamesAsync(dto.IssueIds ?? new List<Guid>());
            if (missingIssues.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some issues do not exist: " + string.Join(", ", missingIssues.Select(x => x.Id))));
            }

            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(dto.DeviceId);
            var restrictStatuses = new[] { Status.Pending, Status.InProgress };
            if (existingRequests.Any(r => !r.IsDeleted && restrictStatuses.Contains(r.Status)))
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "RequestFailed", "Cannot create a new request for this device because it has pending or in-progress requests."));
            }

            var getDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(dto.DeviceId);
            var createTitle = "";
            try
            {
                createTitle = TitleHelper.GenerateRequestTitle(
                    getDevice.Position.Zone.Area.AreaCode,
                    getDevice.Position.Zone.ZoneCode,
                    getDevice.Position.Index,
                    getDevice.DeviceCode);
            }
            catch (Exception)
            {
                createTitle = "Create title fail";
            }

            var request = new Request
            {
                Id = Guid.NewGuid(),
                DeviceId = dto.DeviceId,
                RequestTitle = createTitle,
                Description = DescriptionHelper.GenerateRequestDescription(getDevice.DeviceName),
                Status = Status.Pending, // Use enum value
                RequestedById = userId,
                CreatedDate = TimeHelper.GetHoChiMinhTime(),
                DueDate = TimeHelper.GetHoChiMinhTime().AddDays(7),
                Priority = Priority.Low, // Use enum value
                IsDeleted = false,
                RequestIssues = (dto.IssueIds ?? new List<Guid>()).Select(issueId => new RequestIssue
                {
                    Id = Guid.NewGuid(),
                    IssueId = issueId,

                    Images = new List<Image>()
                }).ToList()
            };

            // Handle image uploads
            if (request.RequestIssues.Any() && dto.ImageFiles != null && dto.ImageFiles.Any() && dto.IssueIdsMatchWithImage != null)
            {
                for (int i = 0; i < Math.Min(dto.ImageFiles.Count, dto.IssueIdsMatchWithImage.Count); i++)
                {
                    var imageFile = dto.ImageFiles[i];
                    var issueId = dto.IssueIdsMatchWithImage[i];

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imageRequest = new AddImageRequest(imageFile, "RequestIssues");
                        var uploadResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
                        if (!uploadResult.Success)
                        {
                            return Result.Failure(uploadResult.Error);
                        }

                        var requestIssue = request.RequestIssues.FirstOrDefault(ri => ri.IssueId == issueId);
                        if (requestIssue != null)
                        {
                            requestIssue.Images.Add(new Image
                            {
                                Id = Guid.NewGuid(),
                                ImageUrl = uploadResult.FilePath,
                                Type = imageFile.ContentType ?? "image/jpeg",
                                RequestIssueId = requestIssue.Id,
                                CreatedDate = TimeHelper.GetHoChiMinhTime(),
                                IsDeleted = false
                            });
                        }
                    }
                }
            }
            // Send notification to Head of Technical (HOT)


            await _requestRepository.CreateAsync(request);
            await _unitOfWork.SaveChangesAsync();
            // Notify Head of Technical (HOT)
            var notificationRequestForHOT = new NotificationRequest
            {
                SenderId = userId,
                Role = 2, // HOT
                ReceiverId = null,
                Title = "Yêu cầu mới",
                Body = $"Yêu cầu cho thiết bị {getDevice.DeviceName} đã được tạo.",
                Type = NotificationType.RequestCreated,
                Channel = NotificationChannel.Both,
                Data = new
                {
                    RequestId = request.Id,
                    DeviceId = request.DeviceId,
                    DeviceName = getDevice.DeviceName,
                    CreatedBy = userId,
                    CreatedDate = request.CreatedDate
                },
                SaveToDatabase = true
            };
            await _notificationService.SendNotificationAsync(notificationRequestForHOT);
            // Notify yourself (the creator)
            var notificationRequestForSelf = new NotificationRequest
            {
                SenderId = userId,
                ReceiverId = userId,
                Title = "Bạn đã tạo một yêu cầu mới",
                Body = $"Yêu cầu cho thiết bị {getDevice.DeviceName} đã được tạo thành công.",
                Type = NotificationType.RequestCreated,
                Channel = NotificationChannel.Both,
                Data = new
                {
                    RequestId = request.Id,
                    DeviceId = request.DeviceId,
                    DeviceName = getDevice.DeviceName,
                    CreatedBy = userId,
                    CreatedDate = request.CreatedDate
                },
                SaveToDatabase = true
            };
            await _notificationService.SendNotificationAsync(notificationRequestForSelf);

            return Result.SuccessWithObject(new { Message = "Test request with notification created successfully!" });
        }

        public async Task<Result> GetRequestByDeviceIdAsync(Guid id)
        {
            var dtos = new List<RequestDTO>();
            var requests = await _requestRepository.GetRequestByDeviceIdAsync(id);
            if (requests == null || !requests.Any())
            {

                return Result.SuccessWithObject(dtos);
            }

            foreach (var r in requests.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }

            return Result.SuccessWithObject(dtos);
        }


        public Task<Result> UpdateRequestIssueStatusAsync(Guid requestId, Guid issueId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateRequestStatusAsync(Guid requestId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> BulkCreateRandomRequestsAsync(int count, Guid initiatorUserId)
        {
            try
            {
                if (count <= 0 || count > 100)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("InvalidCount", "Count must be between 1 and 100"));

                var random = new Random();
                var results = new ConcurrentBag<(bool Success, string Message, Guid? DeviceId, Guid? UserId)>();
                var successCount = 0;

                // 1. Get all areas and prepare issue keywords
                var areas = await _unitOfWork.AreaRepository.GetAllAsync();
                if (!areas.Any())
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NoAreas", "No areas found"));

                // 2. Prepare issue keywords
                var rawKeywords = new[] { "KIM", "Lỗi máy", "Trục kẹt", "Chập mạch", "Động cơ", "Không lên nguồn" };
                var keywords = rawKeywords
                    .Select(k => k.Trim())
                    .Where(k => !string.IsNullOrWhiteSpace(k))
                    .Select(k => StringHelper.RemoveDiacritics(k))
                    .Select(k => k.ToLowerInvariant())
                    .Distinct()
                    .ToArray();

                // 3. Fetch issues for all keywords sequentially (to avoid DbContext threading issues)
                var issuesByKeyword = new ConcurrentDictionary<string, List<SuggestObject>>();
                foreach (var keyword in keywords)
                {
                    var issues = await _unitOfWork.IssueRepository.GetIssueSuggestionsAsync(keyword, 20);
                    if (issues != null && issues.Any())
                    {
                        issuesByKeyword[keyword] = issues;
                    }
                }

                if (!issuesByKeyword.Any())
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NoIssues", "No issues found"));

                // 4. Create a semaphore to limit concurrency
                using var semaphore = new SemaphoreSlim(Math.Min(Environment.ProcessorCount * 2, 8));

                // 5. Process areas in batches until we create the requested number of requests
                var areaList = areas.ToList();
                var processedAreasTracker = new HashSet<Guid>(); // Track which areas we've tried
                var attemptsRemaining = count * 10; // Safety limit to prevent infinite loops

                // Keep processing areas until we've created enough requests or run out of attempts
                while (successCount < count && attemptsRemaining > 0)
                {
                    // Reset area tracker if we've gone through all areas once
                    if (processedAreasTracker.Count >= areaList.Count)
                        processedAreasTracker.Clear();

                    // Find areas we haven't processed yet in this round
                    var remainingAreas = areaList
                        .Where(a => !processedAreasTracker.Contains(a.Id))
                        .ToList();

                    // If no remaining areas, start over with all areas
                    if (!remainingAreas.Any())
                        remainingAreas = areaList;

                    // Take only as many areas as we need for remaining request count
                    var batchAreas = remainingAreas
                        .OrderBy(_ => random.Next()) // Shuffle to try different areas
                        .Take(Math.Min(count - successCount, remainingAreas.Count))
                        .ToList();

                    // Process selected areas concurrently with semaphore for limiting concurrency
                    var tasks = new List<Task>();

                    foreach (var area in batchAreas)
                    {
                        processedAreasTracker.Add(area.Id);
                        attemptsRemaining--;

                        tasks.Add(Task.Run(async () =>
                        {
                            // Acquire semaphore to limit concurrency
                            await semaphore.WaitAsync();
                            try
                            {
                                var result = await ProcessAreaForRequestCreationAsync(
                                    area,
                                    random,
                                    issuesByKeyword,
                                    keywords,
                                    initiatorUserId,
                                    results);

                                if (result)
                                    Interlocked.Increment(ref successCount);
                            }
                            finally
                            {
                                semaphore.Release();
                            }
                        }));
                    }

                    // Wait for current batch to complete
                    await Task.WhenAll(tasks);

                    Console.WriteLine($"[PROGRESS] Created {successCount}/{count} requests so far, attempts remaining: {attemptsRemaining}");
                }

                // Return summary of results
                return Result.SuccessWithObject(new
                {
                    Requested = count,
                    Created = successCount,
                    Details = results.ToList()
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Exception", $"Error creating bulk requests: {ex.Message}"));
            }
        }

        // Helper method to process an area and try to create a request
        private async Task<bool> ProcessAreaForRequestCreationAsync(
            Area area,
            Random random,
            ConcurrentDictionary<string, List<SuggestObject>> issuesByKeyword,
            string[] keywords,
            Guid initiatorUserId,
            ConcurrentBag<(bool Success, string Message, Guid? DeviceId, Guid? UserId)> results)
        {
            Console.WriteLine($"[INFO] Processing area: {area.AreaName}");

            try
            {
                // 1. Get all zones for this area
                var zones = await _unitOfWork.ZoneRepository.GetZonesByAreaIdAsync(area.Id);
                if (!zones.Any())
                {
                    results.Add((false, $"No zones found for area '{area.AreaName}'", null, null));
                    return false;
                }

                // 2. Shuffle zones to try different ones randomly
                var shuffledZones = zones.OrderBy(_ => random.Next()).ToList();

                // 3. Try each zone until we find a suitable one
                foreach (var zone in shuffledZones)
                {
                    // 4. Get all positions for this zone
                    var positions = await _unitOfWork.PositionRepository.GetPositionsByZoneIdAsync(zone.Id);
                    if (!positions.Any())
                    {
                        Console.WriteLine($"[WARN] No positions in zone '{zone.ZoneName}'");
                        continue; // Try next zone
                    }

                    // 5. Shuffle positions to try different ones randomly
                    var shuffledPositions = positions.OrderBy(_ => random.Next()).ToList();

                    // 6. Try each position until we find a suitable one
                    foreach (var position in shuffledPositions)
                    {
                        // 7. Get devices for this position
                        var devices = await _unitOfWork.DeviceRepository.GetDevicesByPositionIdAsync(position.Id);
                        var activeDevices = devices
                            .Where(d => (d.Status == DeviceStatus.InUse || d.Status == DeviceStatus.Active) && !d.IsDeleted)
                            .ToList();

                        if (!activeDevices.Any())
                        {
                            Console.WriteLine($"[WARN] No active devices at position {position.Index} in zone '{zone.ZoneName}'");
                            continue; // Try next position
                        }

                        // 8. Shuffle devices to try different ones randomly
                        var shuffledDevices = activeDevices.OrderBy(_ => random.Next()).ToList();

                        // 9. Try each device until we find one without pending/in-progress requests
                        foreach (var device in shuffledDevices)
                        {
                            // 10. Check for existing pending/in-progress requests
                            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(device.Id);
                            var restrictStatuses = new[] { Status.Pending, Status.InProgress };
                            if (existingRequests.Any(r => !r.IsDeleted && restrictStatuses.Contains(r.Status)))
                            {
                                Console.WriteLine($"[INFO] Device {device.DeviceName} already has pending requests");
                                continue; // Try next device
                            }

                            // 11. Get users for this area
                            var areaUsers = await GetUsersByAreaAsync(area.Id);
                            var validUsers = areaUsers.Where(u => !u.IsDeleted).ToList();

                            // 12. Select user from this area or fall back to initiator
                            Guid userId;
                            string userInfo;

                            if (!validUsers.Any())
                            {
                                userId = initiatorUserId;
                                userInfo = $"Using initiator user (no users in area '{area.AreaName}')";
                            }
                            else
                            {
                                var randomUser = validUsers[random.Next(validUsers.Count)];
                                userId = randomUser.Id;
                                userInfo = $"Using area user '{randomUser.FullName}'";
                            }

                            // 13. Select random issues from available keywords
                            var randomKeyword = keywords[random.Next(keywords.Length)];

                            if (!issuesByKeyword.TryGetValue(randomKeyword, out var availableIssues) || !availableIssues.Any())
                            {
                                Console.WriteLine($"[WARN] No issues found for keyword '{randomKeyword}'");
                                continue; // Try with different device (which will try different keyword)
                            }

                            // 14. Create 1-5 random issues for this request
                            var selectedIssues = availableIssues
                                .OrderBy(_ => random.Next())
                                .Take(random.Next(1, 6))
                                .ToList();

                            // 15. Create the request
                            var requestDto = new TestCreateRequestDTO
                            {
                                DeviceId = device.Id,
                                IssueIds = selectedIssues.Select(i => i.Id).ToList()
                            };

                            Console.WriteLine($"[DEBUG] Creating request for device: {device.DeviceName}, user: {userId}");

                            var createResult = await CreateTestAsync(requestDto, userId);

                            if (createResult.IsSuccess)
                            {
                                Console.WriteLine($"[SUCCESS] Created request for '{device.DeviceName}'");
                                results.Add((true, $"Created request for '{device.DeviceName}' in '{area.AreaName}'. {userInfo}", device.Id, userId));
                                return true; // Successfully created a request for this area
                            }
                            else
                            {
                                Console.WriteLine($"[ERROR] Failed to create request: {createResult.Error?.Description}");
                                results.Add((false, $"Failed: {createResult.Error?.Description ?? "Unknown error"}", device.Id, userId));
                            }
                        }
                    }
                }

                // If we get here, we've tried all zones, positions, and devices in this area without success
                Console.WriteLine($"[WARN] No suitable device found in area '{area.AreaName}'");
                results.Add((false, $"No suitable device found in area '{area.AreaName}'", null, null));
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION] {ex.Message}");
                results.Add((false, $"Error in area '{area.AreaName}': {ex.Message}", null, null));
                return false;
            }
        }

        // Helper method to get users by area (no changes needed)
        private async Task<List<Domain.Entities.User>> GetUsersByAreaAsync(Guid areaId)
        {
            return await _unitOfWork.UserRepository.GetUsersByAreaIdAsync(areaId);
        }
    }
}
