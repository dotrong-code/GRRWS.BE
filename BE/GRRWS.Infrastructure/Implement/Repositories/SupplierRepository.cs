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
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(GRRWSContext context) : base(context) { }
        public async Task<(List<Supplier> Items, int TotalCount)> GetAllActiveSuppliersAsync(int pageNumber, int pageSize)
        {
            var query = _context.Suppliers
                .Where(s => !s.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.SupplierName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
