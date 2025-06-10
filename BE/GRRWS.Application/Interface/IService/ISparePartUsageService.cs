using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.SparePartUsage;
using System;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ISparePartUsageService
    {
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllRequestTakeSparePartUsagesAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsagesByStatusUnconfirmedAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsagesByStatusConfirmedAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsagesByStatusInsufficientAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsagesByStatusDeliveredAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsagesByStatusCancelledAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<Result> GetRequestTakeSparePartUsageByIdAsync(Guid id);
        Task<Result> UpdateIsTakenFromStockAsync(UpdateIsTakenFromStockRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> UpdateRequestTakeSparePartUsageStatusAsync(UpdateRequestStatusRequest request);
    }
}