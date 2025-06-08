using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class SparePartUsageRepository : GenericRepository<SparePartUsage>, ISparePartUsageRepository
    {
        public SparePartUsageRepository(GRRWSContext context) : base(context) { }

        public async Task<List<RequestTakeSparePartUsage>> GetAllRequestTakeSparePartUsagesAsync()
        {
            return await _context.RequestTakeSparePartUsages
                .Include(rtspu => rtspu.SparePartUsages)
                    .ThenInclude(spu => spu.SparePart)
                .ToListAsync();
        }

        public async Task<List<RequestTakeSparePartUsage>> GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus status)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Status == status)
                .Include(rtspu => rtspu.SparePartUsages)
                    .ThenInclude(spu => spu.SparePart)
                .ToListAsync();
        }

        public async Task<RequestTakeSparePartUsage> GetRequestTakeSparePartUsageByIdAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Id == id)
                .Include(rtspu => rtspu.SparePartUsages)
                    .ThenInclude(spu => spu.SparePart)
                .FirstOrDefaultAsync();
        }

        public async Task<SparePartUsage> GetByIdAsync(Guid id)
        {
            return await _context.SparePartUsages
                .Where(spu => spu.Id == id)
                .Include(spu => spu.SparePart)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SparePartUsage>> GetByRequestTakeSparePartUsageIdAsync(Guid requestId)
        {
            return await _context.SparePartUsages
                .Where(spu => spu.RequestTakeSparePartUsageId == requestId)
                .Include(spu => spu.SparePart)
                .ToListAsync();
        }
    }
}