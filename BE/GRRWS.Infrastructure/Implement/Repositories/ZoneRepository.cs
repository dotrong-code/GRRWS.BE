using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Zone;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<GetZoneResponse> Zones, int TotalCount)> GetAllZonesAsync(Guid? areaId, int pageNumber, int pageSize)
        {
            var query = _context.Zones.AsQueryable();

            if (areaId.HasValue)
                query = query.Where(z => z.AreaId == areaId.Value);

            query = query.Where(z => !z.IsDeleted);

            int totalCount = await query.CountAsync();

            var zones = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(z => new GetZoneResponse
                {
                    Id = z.Id,
                    ZoneName = z.ZoneName,
                    AreaId = z.AreaId,
                    CreatedDate = z.CreatedDate,
                    ModifiedDate = z.ModifiedDate
                })
                .ToListAsync();
            return (zones, totalCount);
        }

        public async Task<int> DeleteZoneAsync(Guid id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null) return 0;

            zone.IsDeleted = true;
            _context.Zones.Update(zone);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Zone>> GetZonesByAreaIdAsync(Guid areaId)
        {
            return await _context.Zones
                .Where(z => z.AreaId == areaId)
                .ToListAsync();
        }
    }
}
