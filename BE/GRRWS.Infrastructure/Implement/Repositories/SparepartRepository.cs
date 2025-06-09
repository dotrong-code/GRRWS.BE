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
    public class SparepartRepository : GenericRepository<Sparepart>, ISparepartRepository
    {
        public SparepartRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<Sparepart> Items, int TotalCount)> GetSparepartsBySupplierIdAsync(Guid supplierId, int pageNumber, int pageSize)
        {
            var query = _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.SupplierId == supplierId && !sp.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(sp => sp.SparepartName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(List<Sparepart> Items, int TotalCount)> GetAllActiveSparepartsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => !sp.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(sp => sp.SparepartName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(List<Sparepart> Items, int TotalCount)> GetLowStockSparepartsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.StockQuantity < 10 && sp.StockQuantity >= 0 && !sp.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(sp => sp.SparepartName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<(List<Sparepart> Items, int TotalCount)> GetOutOfStockSparepartsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Spareparts
                .Include(sp => sp.Supplier)
                .Include(sp => sp.MachineSpareparts)
                .ThenInclude(ms => ms.Machine)
                .Where(sp => sp.StockQuantity == 0 && !sp.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(sp => sp.SparepartName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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