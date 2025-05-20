using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;
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

        public RequestService(IRequestRepository requestRepository)
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
                    DueDate = r.DueDate,
                    Priority = r.Priority,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        IssueTitle = ri.Issue.IssueKey,
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
                DueDate = r.DueDate,
                Priority = r.Priority,
                Issues = r.RequestIssues.Select(ri => new IssueDTO
                {
                    Id = ri.Issue.Id,
                    IssueTitle = ri.Issue.IssueKey,
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
                    DueDate = r.DueDate,
                    Priority = r.Priority,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        IssueTitle = ri.Issue.IssueKey,
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
                    DueDate = r.DueDate,
                    Priority = r.Priority,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Issue.Id,
                        IssueTitle = ri.Issue.IssueKey,
                        ImageUrls = ri.Images.Select(img => img.ImageUrl).ToList()
                    }).ToList()
                }).ToList<object>();
            return Result.SuccessWithObject(dtos);
        }
        public async Task<Result> CreateAsync(CreateRequestDTO dto)
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
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
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
    }
}
