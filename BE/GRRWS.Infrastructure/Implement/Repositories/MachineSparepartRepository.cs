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
    public class MachineSparepartRepository : GenericRepository<MachineSparepart>, IMachineSparepartRepository
    {
        public MachineSparepartRepository(GRRWSContext context) : base(context) { }

        public async Task<List<MachineSparepart>> GetSparepartsByMachineIdAsync(Guid machineId)
        {
            return await _context.MachineSpareparts
                .Include(ms => ms.Sparepart)
                .ThenInclude(sp => sp.Supplier)
                .Include(ms => ms.Machine) // Thêm include cho Machine
                .Where(ms => ms.MachineId == machineId && !ms.IsDeleted)
                .ToListAsync();
        }
    }
}
