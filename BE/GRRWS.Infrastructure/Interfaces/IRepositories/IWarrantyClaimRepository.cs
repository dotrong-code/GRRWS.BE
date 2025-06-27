using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.WarrantyClaim;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IWarrantyClaimRepository : IGenericRepository<WarrantyClaim>
    {
        Task<WarrantyClaimWithTasksDTO> GetWarrantyClaimWithTasksAsync(Guid warrantyClaimId);
        Task<WarrantyClaimWithTasksDTO> GetWarrantyClaimByTaskIdAsync(Guid taskId, bool isSubmissionTask);
        Task<List<WarrantyClaimWithTasksDTO>> GetWarrantyClaimsWithTasksAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
    }
}
