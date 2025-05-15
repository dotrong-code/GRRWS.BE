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

            return Result.SuccessWithObject(dto);
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
