using GRRWS.Application.Common.Result;

namespace GRRWS.Application.Interface.IService
{
    public interface IWarrantyClaimService
    {
        Task<Result> GetWarrantyClaimWithTasksAsync(Guid warrantyClaimId);
        Task<Result> GetWarrantyClaimByTaskIdAsync(Guid taskId, string taskType);
        Task<Result> GetWarrantyClaimsWithTasksAsync(int pageNumber, int pageSize);
    }
}