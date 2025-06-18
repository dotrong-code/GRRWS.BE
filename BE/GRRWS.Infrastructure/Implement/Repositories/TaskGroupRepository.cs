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
    }
}
