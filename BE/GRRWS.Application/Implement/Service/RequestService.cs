using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Application.Implement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ITokenService _tokenService;
        public RequestService(IRequestRepository requestRepository, ITokenService tokenService)
        {
            _requestRepository = requestRepository;
            _tokenService = tokenService;
        }

        public async Task<Result> GetAllAsync()
        {
            var requests = await _requestRepository.GetAllRequestAsync();
            var dtos = requests
                .Where(r => !r.IsDeleted)
                .Select(r => new RequestDTO
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
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        DisplayName = ri.Issue.DisplayName,
                        ImageUrls = ri.Images.Select(img => img.ImageUrl).ToList()
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
                Issues = r.RequestIssues.Select(ri => new IssueDTO
                {
                    Id = ri.Issue.Id,
                    DisplayName = ri.Issue.DisplayName,
                    ImageUrls = ri.Images.Select(img => img.ImageUrl).ToList()
                }).ToList()
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetRequestByDeviceIdAsync(Guid id)
        {
            var requests = await _requestRepository.GetRequestByDeviceIdAsync(id);
            var dtos = requests
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new RequestDTO
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
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        DisplayName = ri.Issue.DisplayName,
                        ImageUrls = ri.Images.Select(img => img.ImageUrl).ToList()
                    }).ToList()
                }).ToList<object>();
            return Result.SuccessWithObject(dtos);
        }
        public async Task<Result> GetRequestByUserIdAsync(Guid userId)
        {
            var requests = await _requestRepository.GetRequestByUserIdAsync(userId);
            var dtos = requests
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new RequestDTO
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
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        DisplayName = ri.Issue.DisplayName,
                        ImageUrls = ri.Images.Select(img => img.ImageUrl).ToList()
                    }).ToList()
                }).ToList<object>();
            return Result.SuccessWithObject(dtos);
        }
        public async Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId)
        {
            if (dto.DeviceId == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "DeviceId cannot be null.", 0));
            }

            var request = new Request
            {
                Id = Guid.NewGuid(),
                DeviceId = dto.DeviceId,
                RequestTitle = dto.RequestTitle,
                Description = dto.Description,
                Status = "Pending",
                CreatedBy = userId,
                RequestedById = userId,
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.Now,
                Priority = "None",
                IsDeleted = false,
                RequestIssues = dto.IssueIds.Select(issueId => new RequestIssue
                {
                    IssueId = issueId
                }).ToList()
            };

            await _requestRepository.CreateAsync(request);

            return Result.SuccessWithObject(new { Message = "Successfully!" });
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
            return Result.SuccessWithObject(new { Message = "Successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var r = await _requestRepository.GetByIdAsync(id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            r.IsDeleted = true;
            r.ModifiedDate = DateTime.UtcNow;
            await _requestRepository.UpdateAsync(r);

            return Result.Success();
        }
        public async Task<Result> GetIssuesByRequestIdAsync(Guid requestId)
        {
            var issues = await _requestRepository.GetIssuesByRequestIdAsync(requestId);
            if (issues == null || !issues.Any())
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No issues found for the request.", 0));

            return Result.SuccessWithObject(issues);
        }

        public async Task<Result> GetRequestSummary()
        {
            var list = await _requestRepository.GetRequestSummaryAsync();
            if (list == null || !list.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not Found", "Not found any list"));
            return Result.SuccessWithObject(list);
        }
    }
}
