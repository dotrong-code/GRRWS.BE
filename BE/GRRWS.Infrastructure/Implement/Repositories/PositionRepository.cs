using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Position;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<GetPositionResponse> Positions, int TotalCount)> GetAllPositionsAsync(Guid? zoneId, int pageNumber, int pageSize)
        {
            var query = _context.Positions.AsQueryable();

            if (zoneId.HasValue)
                query = query.Where(p => p.ZoneId == zoneId.Value);

            query = query.Where(p => !p.IsDeleted);

            int totalCount = await query.CountAsync();

            var positions = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new GetPositionResponse
                {
                    Id = p.Id,
                    Index = p.Index,
                    ZoneId = p.ZoneId,
                    DeviceId = p.DeviceId,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate
                })
                .ToListAsync();

            return (positions, totalCount);
        }

        public async Task<int> DeletePositionAsync(Guid id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null) return 0;

            position.IsDeleted = true;
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync();
        }
    }
}
