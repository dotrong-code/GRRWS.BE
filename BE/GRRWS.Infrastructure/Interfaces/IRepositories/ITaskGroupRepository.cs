using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ITaskGroupRepository : IGenericRepository<Domain.Entities.TaskGroup>
    {
        Task<TaskGroup?> GetTaskGroupWithTasksAsync(Guid taskGroupId);
        Task<List<TaskGroup>> GetTaskGroupsByReportIdAsync(Guid reportId);
    }
}
