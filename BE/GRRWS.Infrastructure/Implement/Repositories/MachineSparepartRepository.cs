using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MachineSparepartRepository : GenericRepository<MachineSparepart>, IMachineSparepartRepository
    {
        public MachineSparepartRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<MachineSparepart> Items, int TotalCount)> GetSparepartsByMachineIdAsync(Guid machineId, int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var query = _context.MachineSpareparts
                .Include(ms => ms.Sparepart)
                .ThenInclude(sp => sp.Supplier)
                .Include(ms => ms.Machine)
                .Where(ms => ms.MachineId == machineId && !ms.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchSparepartName))
            {
                query = query.Where(ms => ms.Sparepart.SparepartName.Contains(searchSparepartName));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(ms => ms.Sparepart.SparepartName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}