using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class TaskGroupRepository : GenericRepository<Domain.Entities.TaskGroup>, ITaskGroupRepository
    {
        public TaskGroupRepository(GRRWSContext context) : base(context)
        {
        }

        public Task<List<TaskGroup>> GetTaskGroupsByReportIdAsync(Guid reportId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskGroup?> GetTaskGroupWithTasksAsync(Guid taskGroupId)
        {
            throw new NotImplementedException();
        }
    }
}
