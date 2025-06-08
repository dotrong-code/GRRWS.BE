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
    public class SparepartRepository : GenericRepository<Sparepart>, ISparepartRepository
    {
        public SparepartRepository(GRRWSContext context) : base(context) { }
        public async Task<List<Sparepart>> GetSparepartsBySupplierIdAsync(Guid supplierId)
        {
            return await _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.SupplierId == supplierId && !sp.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Sparepart>> GetAllActiveSparepartsAsync()
        {
            return await _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => !sp.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Sparepart>> GetLowStockSparepartsAsync()
        {
            return await _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.StockQuantity < 10 && sp.StockQuantity >= 0 && !sp.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Sparepart>> GetOutOfStockSparepartsAsync()
        {
            return await _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.StockQuantity == 0 && !sp.IsDeleted)
                .ToListAsync();
        }
        public async Task<Sparepart> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.Id == id && !sp.IsDeleted)
                .FirstOrDefaultAsync();
        }
    }
}
