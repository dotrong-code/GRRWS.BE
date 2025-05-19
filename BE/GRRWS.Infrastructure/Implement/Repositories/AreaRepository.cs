using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Area;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<GetAreaResponse> Areas, int TotalCount)> GetAllAreasAsync(int pageNumber, int pageSize)
        {
            var query = _context.Areas.AsQueryable();
            query = query.Where(a => !a.IsDeleted);

            int totalCount = await query.CountAsync();

            var areas = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new GetAreaResponse
                {
                    Id = a.Id,
                    AreaName = a.AreaName,
                    CreatedDate = a.CreatedDate,
                    ModifiedDate = a.ModifiedDate
                })
                .ToListAsync();
            return (areas, totalCount);
        }

        public async Task<int> DeleteAreaAsync(Guid id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null) return 0;

            area.IsDeleted = true;
            _context.Areas.Update(area);
            return await _context.SaveChangesAsync();
        }
    }
}
