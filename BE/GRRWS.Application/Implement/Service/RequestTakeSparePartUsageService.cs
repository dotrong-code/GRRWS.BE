using GRRWS.Application.Common;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.RequestTakeSparePartUsage;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Application.Implement.Service
{
    public class RequestTakeSparePartUsageService : IRequestTakeSparePartUsageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckIsExist _checkIsExist;
        public RequestTakeSparePartUsageService(IUnitOfWork unitOfWork, CheckIsExist checkIsExist)
        {
            _unitOfWork = unitOfWork;
            _checkIsExist = checkIsExist;
        }
        public async Task<Result> GetAllSparePartRequestsAsync(
        int pageNumber,
        int pageSize,
        string? status = null,
        string? sortBy = null,
        bool isAscending = true)
        {
            var result = await _unitOfWork.RequestTakeSparePartUsageRepository.GetAllAsync(
                pageNumber,
                pageSize,
                status,
                sortBy,
                isAscending);
            if (result.items == null || !result.items.Any())
                {
                return Result.Failure(Error.NotFound("NoRequestTakeSparePartUsagesFound", "No RequestTakeSparePartUsages found."));
            }
            return Result.SuccessWithObject(result);
        }
    }
}
