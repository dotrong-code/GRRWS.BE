using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Request;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Status = r.Status,
                    CreatedDate = r.CreatedDate,
                    CreatedBy = r.CreatedBy,
                    ModifiedDate = r.ModifiedDate,
                    ModifiedBy = r.ModifiedBy,
                    DueDate = r.DueDate,
                    Priority = r.Priority,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        IssueTitle = ri.Issue.IssueKey
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
                Status = r.Status,
                CreatedDate = r.CreatedDate,
                CreatedBy = r.CreatedBy,
                ModifiedDate = r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                DueDate = r.DueDate,
                Priority = r.Priority,
                Issues = r.RequestIssues.Select(ri => new IssueDTO
                {
                    Id = ri.Issue.Id,
                    IssueTitle = ri.Issue.IssueKey
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

        public async Task<Result> CreateAsync(CreateRequestDTO dto)
        {
            var request = new Request
            {
                Id = Guid.NewGuid(),
                RequestTitle = dto.RequestTitle,
                Description = dto.Description,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
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
                Status = Enum.TryParse<Status>(dto.Status, out var status) ? status : Status.Pending,
                DueDate = dto.DueDate,
                Priority = Enum.TryParse<Priority>(dto.Priority, out var priority) ? priority : Priority.Low,
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

            var issues = await MapIssuesWithImagesAsync(requestIssues);
            return Result.SuccessWithObject(issues);
        }

        public async Task<Result> CancelRequestAsync(Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO.CancelRequestDTO dto)
        {
            var r = await _requestRepository.GetByIdAsync(dto.RequestId);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            var restrictedStatuses = new[] { Status.Approved, Status.InProgress, Status.Completed };
            if (restrictedStatuses.Contains(r.Status))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Cannot cancel this request! You can only cancel request if the status is Pending.", 0));

            r.Status = Status.Pending; // Use appropriate enum value
            r.Description = "Yêu cầu đã bị hủy với lý do: " + dto.Reason;
            r.ModifiedDate = DateTime.UtcNow;
            await _requestRepository.UpdateAsync(r);

            return Result.Success();
        }

    }
}
