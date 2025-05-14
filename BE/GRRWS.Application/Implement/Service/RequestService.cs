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
            var requests = await _requestRepository.GetAllAsync();
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
                    Priority = r.Priority
                }).ToList<object>();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var r = await _requestRepository.GetByIdAsync(id);
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
                ModifiedBy = r.ModifiedBy
            };

            return Result.SuccessWithObject(new { Message = "Successfully!" });
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

        public async Task<Result> UpdateAsync(UpdateRequestDTO dto)
        {
            var r = await _requestRepository.GetByIdAsync(dto.Id);
            if (r == null || r.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));

            r.RequestTitle = dto.RequestTitle;
            r.Description = dto.Description;
            r.Status = dto.Status;
            r.DueDate = dto.DueDate;
            r.Priority = dto.Priority;
            r.ModifiedBy = dto.ModifiedBy;
            r.ModifiedDate = DateTime.UtcNow;
            r.RequestIssues?.Clear();
            r.RequestIssues = dto.IssueIds.Select(issueId => new RequestIssue
            {
                RequestId = r.Id,
                IssueId = issueId
            }).ToList();
            await _requestRepository.UpdateAsync(r);
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
