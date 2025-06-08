using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
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

        public async Task<Result> GetAllAsync()
        {
            var requests = await _requestRepository.GetAllRequestAsync();
            var dtos = requests
                .Where(r => !r.IsDeleted)
                .Select(r => new RequestDTO
                {
                    Id = r.Id,
                    RequestTitle = r.RequestTitle,
                    Description = r.Description,
                    //Status = r.Status,
                    CreatedDate = r.CreatedDate,
                    CreatedBy = r.CreatedBy,
                    ModifiedDate = r.ModifiedDate,
                    ModifiedBy = r.ModifiedBy,
                    //DueDate = r.DueDate,
                    //Priority = r.Priority,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        //IssueTitle = ri.Issue.IssueKey
                    }).ToList()
                }).ToList<object>();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var r = await _requestRepository.GetRequestByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            var dto = new RequestDTO
            {
                Id = r.Id,
                RequestTitle = r.RequestTitle,
                Description = r.Description,
                //Status = r.Status,
                CreatedDate = r.CreatedDate,
                CreatedBy = r.CreatedBy,
                ModifiedDate = r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                //DueDate = r.DueDate,
                //Priority = r.Priority,
                Issues = r.RequestIssues.Select(ri => new IssueDTO
                {
                    Id = ri.Issue.Id,
                    //IssueTitle = ri.Issue.IssueKey
                }).ToList()
            };

            //var issueImageMap = dto.IssueImages.ToDictionary(x => x.IssueId, x => x.Images);
            //foreach (var requestIssue in request.RequestIssues)
            //{
            //    if (issueImageMap.TryGetValue(requestIssue.IssueId, out var imageFiles) && imageFiles != null)
            //    {
            //        foreach (var imageFile in imageFiles)
            //        {
            //            if (imageFile != null && imageFile.Length > 0)
            //            {
            //                var imageRequest = new AddImageRequest(imageFile, "RequestIssues");
            //                var uploadResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
            //                if (!uploadResult.Success)
            //                {
            //                    return Result.Failure(uploadResult.Error);
            //                }

            //                requestIssue.Images.Add(new Image
            //                {
            //                    Id = Guid.NewGuid(),
            //                    ImageUrl = uploadResult.FilePath,
            //                    Type = imageFile.ContentType ?? "image/jpeg",
            //                    RequestIssueId = requestIssue.Id,
            //                    CreatedDate = DateTime.UtcNow,
            //                    IsDeleted = false
            //                });
            //            }
            //        }
            //    }
            //}

            //await _requestRepository.CreateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Request created successfully!" });
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
                CreatedDate = DateTime.UtcNow,
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
            //r.Status = "Delete";
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
            if (requestIssues == null || !requestIssues.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No issues found for the request.", 0));
            }

            //var issues = await MapIssuesWithImagesAsync(requestIssues);
            return Result.SuccessWithObject(requestIssues);
        }

        public async Task<Result> CancelRequestAsync(Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO.CancelRequestDTO dto)
        {
            var r = await _requestRepository.GetByIdAsync(dto.RequestId);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));


            r.Description = "Yêu cầu đã bị hủy với lý do: " + dto.Reason;
            r.ModifiedDate = DateTime.UtcNow;
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
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
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

        public Task<Result> GetRequestByDeviceIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetRequestByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateRequestIssueStatusAsync(Guid requestId, Guid issueId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateRequestStatusAsync(Guid requestId, bool isRejected, string rejectionReason, string rejectionDetails)
        {
            throw new NotImplementedException();
        }
    }
}
