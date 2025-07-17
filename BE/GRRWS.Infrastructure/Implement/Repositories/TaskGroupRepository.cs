using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

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
        public async Task<TaskGroup> GetByRequestIdAsync(Guid requestId)
        {
            return await _context.TaskGroups
                .Include(tg => tg.Tasks)
                    .ThenInclude(t => t.RequestMachineReplacement)
                .Include(tg => tg.Tasks)
                    .ThenInclude(t => t.WarrantyClaim)
                .FirstOrDefaultAsync(tg => tg.Report.RequestId == requestId && !tg.IsDeleted);
        }

    }
}
