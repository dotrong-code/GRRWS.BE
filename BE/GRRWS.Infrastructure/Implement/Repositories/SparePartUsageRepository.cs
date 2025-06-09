using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.DTOs.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class SparePartUsageRepository : GenericRepository<SparePartUsage>, ISparePartUsageRepository
    {
        public SparePartUsageRepository(GRRWSContext context) : base(context) { }

        public async Task<PagedResponse<RequestTakeSparePartUsage>> GetAllRequestTakeSparePartUsagesAsync(int pageNumber, int pageSize)
        {
            var query = _context.RequestTakeSparePartUsages
                .Include(rtspu => rtspu.SparePartUsages)
                .OrderByDescending(rtspu => rtspu.RequestDate);

            var totalCount = await query.CountAsync();
            var requests = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<RequestTakeSparePartUsage>
            {
                Data = requests,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResponse<RequestTakeSparePartUsage>> GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus status, int pageNumber, int pageSize)
        {
            var query = _context.RequestTakeSparePartUsages
                .Include(rtspu => rtspu.SparePartUsages)
                .Where(rtspu => rtspu.Status == status)
                .OrderByDescending(rtspu => rtspu.RequestDate);

            var totalCount = await query.CountAsync();
            var requests = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<RequestTakeSparePartUsage>
            {
                Data = requests,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<RequestTakeSparePartUsage> GetRequestTakeSparePartUsageByIdAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Include(rtspu => rtspu.SparePartUsages)
                .Where(rtspu => rtspu.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<SparePartUsage> GetByIdAsync(Guid id)
        {
            return await _context.SparePartUsages
                .Where(spu => spu.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SparePartUsage>> GetByRequestTakeSparePartUsageIdAsync(Guid requestId)
        {
            return await _context.SparePartUsages
                .Where(spu => spu.RequestTakeSparePartUsageId == requestId)
                .ToListAsync();
        }
    }
}