using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Application.Implement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository requestRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
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
                    Status = ri.Status,
                    IsRejected = ri.IsRejected, // Ánh xạ trường này
                    RejectionReason = ri.RejectionReason, // Ánh xạ trường này
                    RejectionDetails = ri.RejectionDetails // Ánh xạ trường này
                });
            }
            return issues;
        }
        public async Task<Result> UpdateRequestIssueStatusAsync(Guid requestId, Guid issueId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.IsDeleted)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));
            }

            var requestIssue = request.RequestIssues?.FirstOrDefault(ri => ri.IssueId == issueId);
            if (requestIssue == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestIssue not found.", 0));
            }

            requestIssue.IsRejected = isRejected;
            requestIssue.RejectionReason = rejectionReason;
            requestIssue.RejectionDetails = rejectionDetails;
            requestIssue.Status = isRejected ? "Rejected" : "Pending"; // Cập nhật trạng thái

            await _requestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "RequestIssue status updated successfully!" });
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
                Status = r.Status,
                CreatedDate = r.CreatedDate,
                CreatedBy = r.RequestedById,
                ModifiedDate = r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                IsRejected = r.IsRejected, // Ánh xạ trường này
                RejectionReason = r.RejectionReason, // Ánh xạ trường này
                RejectionDetails = r.RejectionDetails, // Ánh xạ trường này
                Issues = await MapIssuesWithImagesAsync(r.RequestIssues)
            };
        }

        public async Task<Result> GetAllAsync()
        {
            var requests = await _requestRepository.GetAllRequestAsync();
            var dtos = new List<object>();
            foreach (var r in requests.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }
            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var r = await _requestRepository.GetRequestByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            var dto = await MapRequestToDTOAsync(r);
            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetRequestByDeviceIdAsync(Guid id)
        {
            var requests = await _requestRepository.GetRequestByDeviceIdAsync(id);
            var dtos = new List<object>();
            foreach (var r in requests.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }
            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetRequestByUserIdAsync(Guid userId)
        {
            var requests = await _requestRepository.GetRequestByUserIdAsync(userId);
            var dtos = new List<object>();
            foreach (var r in requests.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestToDTOAsync(r);
                dtos.Add(dto);
            }
            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId)
        {
            if (dto.DeviceId == Guid.Empty)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Device not found.", 0));
            }

            if (!await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(dto.DeviceId))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Device not found.", 0));
            }

            if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "User not found.", 0));
            }

            var missingIssues = await _unitOfWork.IssueRepository.GetNotFoundIssueDisplayNamesAsync(dto.IssueIds);
            if (missingIssues.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error(
                    "NotFound", "Some issues do not exist: " + string.Join(", ", missingIssues.Select(x => x.Id)), 0));
            }

            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(dto.DeviceId);
            var restrictStatus = new[] { "Pending", "InProgress" };
            if (existingRequests.Any(r => !r.IsDeleted && restrictStatus.Contains(r.Status)))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error(
                    "RequestFailed", "Cannot create a new request for this device because it has pending or in-progress requests.", 0));
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
                Status = "Pending",
                CreatedBy = userId,
                RequestedById = userId,
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "None",
                IsDeleted = false,
                RequestIssues = dto.IssueIds.Select(issueId => new RequestIssue
                {
                    Id = Guid.NewGuid(),
                    IssueId = issueId,
                    Status = "Pending",
                    Images = new List<Image>()
                }).ToList()
            };

            var issueImageMap = dto.IssueImages.ToDictionary(x => x.IssueId, x => x.Images);
            foreach (var requestIssue in request.RequestIssues)
            {
                if (issueImageMap.TryGetValue(requestIssue.IssueId, out var imageFiles) && imageFiles != null)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        if (imageFile != null && imageFile.Length > 0)
                        {
                            var imageRequest = new AddImageRequest(imageFile, "RequestIssues");
                            var uploadResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
                            if (!uploadResult.Success)
                            {
                                return Result.Failure(uploadResult.Error);
                            }

                            requestIssue.Images.Add(new Image
                            {
                                Id = Guid.NewGuid(),
                                ImageUrl = uploadResult.FilePath,
                                Type = imageFile.ContentType ?? "image/jpeg",
                                RequestIssueId = requestIssue.Id,
                                CreatedDate = DateTime.UtcNow,
                                IsDeleted = false
                            });
                        }
                    }
                }
            }

            await _requestRepository.CreateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Request created successfully!" });
        }


        public async Task<Result> CreateTestAsync(TestCreateRequestDTO dto, Guid userId)
        {
            if (dto.DeviceId == Guid.Empty)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Device not found.", 0));
            }

            if (!await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(dto.DeviceId))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Device not found.", 0));
            }

            if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "User not found.", 0));
            }

            var missingIssues = await _unitOfWork.IssueRepository.GetNotFoundIssueDisplayNamesAsync(dto.IssueIds ?? new List<Guid>());
            if (missingIssues.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error(
                    "NotFound", "Some issues do not exist: " + string.Join(", ", missingIssues.Select(x => x.Id)), 0));
            }

            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(dto.DeviceId);
            var restrictStatus = new[] { "Pending", "InProgress" };
            if (existingRequests.Any(r => !r.IsDeleted && restrictStatus.Contains(r.Status)))
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error(
                    "RequestFailed", "Cannot create a new request for this device because it has pending or in-progress requests.", 0));
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
                Description = createTitle,
                Status = "Pending",
                CreatedBy = userId,
                RequestedById = userId,
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "None",
                IsDeleted = false,
                RequestIssues = (dto.IssueIds ?? new List<Guid>()).Select(issueId => new RequestIssue
                {
                    Id = Guid.NewGuid(),
                    IssueId = issueId,
                    Status = "Pending",
                    Images = new List<Image>()
                }).ToList()
            };

            // Gán ảnh dựa trên IssueIdsMatchWithImage
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
                                CreatedDate = DateTime.UtcNow,
                                IsDeleted = false
                            });
                        }
                    }
                }
            }

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
                Status = dto.Status,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = DateTime.UtcNow
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
            r.Status = "Delete";
            r.ModifiedDate = DateTime.UtcNow;
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

        //        public async Task<Result> CreateRequestAsync(CreateRequest request, Guid userId)
        //        {
        //            if (!await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(request.DeviceId))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device does not exist."));
        //            }
        //            // check in request : Can create when request status is Approved or Deinied

        //            if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "User does not exist."));
        //            }
        //            var missingIssues = await _unitOfWork.IssueRepository.GetNotFoundIssueDisplayNamesAsync(request.IssueIds);
        //            if (missingIssues.Any())
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
        //    "NotFound",
        //    "Some issues do not exist: " + string.Join(", ", missingIssues.Select(x => x.Id))
        //));
        //            }

        //            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(request.DeviceId);
        //            var restrictStatus = new[] { "Pending", "InProgress" };
        //            if (existingRequests.Any(r => !r.IsDeleted && restrictStatus.Contains(r.Status)))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
        //                    "RequestFailed", "Cannot create a new request for this device because it has pending or in-progress requests."));
        //            }

        //            var getDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(request.DeviceId);
        //            var createTitle = "";
        //            try
        //            {
        //                createTitle = TitleHelper.GenerateRequestTitle(getDevice.Position.Zone.Area.AreaCode, getDevice.Position.Zone.ZoneCode, getDevice.Position.Index, getDevice.DeviceCode);
        //            }
        //            catch (Exception)
        //            {

        //                createTitle = "Create title fail";
        //            }
        //            var newRequest = new Request
        //            {
        //                Id = Guid.NewGuid(),
        //                DeviceId = request.DeviceId,
        //                RequestTitle = createTitle,
        //                Description = "This is description",
        //                Status = "Pending",
        //                CreatedBy = userId,
        //                RequestedById = userId,
        //                CreatedDate = DateTime.UtcNow,
        //                DueDate = DateTime.Now.AddDays(7), // Default due date is 7 days from now
        //                Priority = "None",
        //                IsDeleted = false,
        //                RequestIssues = request.IssueIds.Select(issueId => new RequestIssue
        //                {
        //                    IssueId = issueId
        //                }).ToList()
        //            };
        //            await _requestRepository.CreateAsync(newRequest);
        //            return Result.SuccessWithObject(new { Message = "Request created successfully!" });

        public async Task<Result> GetIssuesByRequestIdAsync(Guid requestId)
        {
            var requestIssues = await _requestRepository.GetIssuesByRequestIdAsync(requestId);
            if (requestIssues == null || !requestIssues.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No issues found for the request.", 0));

            }

            var issues = await MapIssuesWithImagesAsync(requestIssues);
            return Result.SuccessWithObject(issues);
        }

        //        public async Task<Result> CreateRequestAsync(CreateRequest request, Guid userId)
        //        {
        //            if (!await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(request.DeviceId))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device does not exist."));
        //            }
        //            // check in request : Can create when request status is Approved or Deinied

        //            if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "User does not exist."));
        //            }
        //            var missingIssues = await _unitOfWork.IssueRepository.GetNotFoundIssueDisplayNamesAsync(request.IssueIds);
        //            if (missingIssues.Any())
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
        //    "NotFound",
        //    "Some issues do not exist: " + string.Join(", ", missingIssues.Select(x => x.Id))
        //));
        //            }

        //            var existingRequests = await _unitOfWork.RequestRepository.GetRequestByDeviceIdAsync(request.DeviceId);
        //            var restrictStatus = new[] { "Pending", "InProgress" };
        //            if (existingRequests.Any(r => !r.IsDeleted && restrictStatus.Contains(r.Status)))
        //            {
        //                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
        //                    "RequestFailed", "Cannot create a new request for this device because it has pending or in-progress requests."));
        //            }

        //        }

        //            var getDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(request.DeviceId);
        //            var createTitle = "";
        //            try
        //            {
        //                createTitle = TitleHelper.GenerateRequestTitle(getDevice.Position.Zone.Area.AreaCode, getDevice.Position.Zone.ZoneCode, getDevice.Position.Index, getDevice.DeviceCode);
        //            }
        //            catch (Exception)
        //            {

        //                createTitle = "Create title fail";
        //            }
        //            var newRequest = new Request
        //            {
        //                Id = Guid.NewGuid(),
        //                DeviceId = request.DeviceId,
        //                RequestTitle = createTitle,
        //                Description = "This is description",
        //                Status = "Pending",
        //                CreatedBy = userId,
        //                RequestedById = userId,
        //                CreatedDate = DateTime.UtcNow,
        //                DueDate = DateTime.Now.AddDays(7), // Default due date is 7 days from now
        //                Priority = "None",
        //                IsDeleted = false,
        //                RequestIssues = request.IssueIds.Select(issueId => new RequestIssue
        //                {
        //                    IssueId = issueId
        //                }).ToList()
        //            };
        //            await _requestRepository.CreateAsync(newRequest);
        //            return Result.SuccessWithObject(new { Message = "Request created successfully!" });
        //        }
        public async Task<Result> CancelRequestAsync(Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO.CancelRequestDTO dto)
        {
            var r = await _requestRepository.GetByIdAsync(dto.RequestId);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));
            if (r.Status == "Approved" || r.Status == "Denied" || r.Status == "InProgress" || r.Status == "Completed")
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Cannot cancel this request! You can only cancel request if the status is Pending.", 0));
            r.Status = "Denied";
            r.Description = "Yêu cầu đã bị hủy với lý do: " + dto.Reason;
            r.ModifiedDate = DateTime.UtcNow;
            await _requestRepository.UpdateAsync(r);

            return Result.SuccessWithObject(new { Message = "Request canceled successfully!" });
        }
        public async Task<Result> UpdateRequestStatusAsync(Guid requestId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null || request.IsDeleted)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));
            }

            request.IsRejected = isRejected;
            request.RejectionReason = rejectionReason;
            request.RejectionDetails = rejectionDetails;
            request.Status = isRejected ? "Rejected" : "Pending"; // Cập nhật trạng thái

            await _requestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Request status updated successfully!" });
        }


    }
}
