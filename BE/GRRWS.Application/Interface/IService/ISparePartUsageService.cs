using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.SparePartUsage;
using System;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ISparePartUsageService
    {
        Task<Result> GetByIdAsync(Guid id);
        //Task<Result> GetAllByErrorDetailIdAsync(Guid errorDetailId);
        Task<Result> UpdateIsTakenFromStockAsync(UpdateIsTakenFromStockRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetAllRequestTakeSparePartUsagesAsync();
        
        Task<Result> GetRequestTakeSparePartUsageByIdAsync(Guid id);
        Task<Result> GetRequestTakeSparePartUsagesByStatusUnconfirmedAsync();
        Task<Result> GetRequestTakeSparePartUsagesByStatusConfirmedAsync();
        Task<Result> GetRequestTakeSparePartUsagesByStatusInsufficientAsync();
        Task<Result> GetRequestTakeSparePartUsagesByStatusDeliveredAsync();
        Task<Result> GetRequestTakeSparePartUsagesByStatusCancelledAsync();

        Task<Result> UpdateRequestTakeSparePartUsageStatusAsync(UpdateRequestStatusRequest request);
    }
}