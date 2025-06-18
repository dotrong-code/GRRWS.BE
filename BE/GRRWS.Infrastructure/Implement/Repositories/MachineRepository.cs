using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MachineRepository : GenericRepository<Machine>, IMachineRepository
    {
        public MachineRepository(GRRWSContext context) : base(context) { }
        public async Task<(List<Machine> Items, int TotalCount)> GetAllActiveMachinesAsync(int pageNumber, int pageSize)
        {
            var query = _context.Machines
                .Where(m => !m.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(m => m.MachineName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
