using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class RequestMachineReplacementService : IRequestMachineReplacementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequestMachineReplacementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> GetAllAsync(
    int pageNumber,
    int pageSize,
    string? status = null,
    string? sortBy = null,
    bool isAscending = true)
        {
            var (items, totalCount) = await _unitOfWork.RequestMachineReplacementRepository.GetAllAsync(
                pageNumber,
                pageSize,
                status,
                sortBy,
                isAscending);
            var response = new PagedResponse<RequestMachineReplacement>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }
    }
}
