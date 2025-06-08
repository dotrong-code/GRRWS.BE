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
        public async Task<List<Supplier>> GetAllActiveSuppliersAsync()
        {
            return await _context.Suppliers
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }
    }
}
