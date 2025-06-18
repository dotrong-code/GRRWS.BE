using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class RequestTakeSparePartUsageRepository : GenericRepository<RequestTakeSparePartUsage>, IRequestTakeSparePartUsageRepository
    {
        public RequestTakeSparePartUsageRepository(GRRWSContext context) : base(context) { }

        public async Task<RequestTakeSparePartUsage> GetByIdAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<RequestTakeSparePartUsage> GetByIdIncludeSparePartUsagesAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Id == id)
                .Include(rtspu => rtspu.SparePartUsages)
                .FirstOrDefaultAsync();
        }
    }
}