using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestMachineReplacementRepository : IGenericRepository<Domain.Entities.RequestMachineReplacement>
    {
        public Task<(List<RequestMachineReplacement> items, int totalCount)> GetAllAsync(
    int pageNumber,
    int pageSize,
    string? status = null,
    string? sortBy = null,
    bool isAscending = true);
        Task<RequestMachineReplacement> GetByTaskGroupIdAsync(Guid taskGroupId);
        Task<RequestMachineReplacement> GetByTaskIdAsync(Guid taskId);
    }

}
