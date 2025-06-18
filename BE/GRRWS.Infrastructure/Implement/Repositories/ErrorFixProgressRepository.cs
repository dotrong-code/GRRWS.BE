using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ErrorFixProgressRepository : GenericRepository<ErrorFixProgress>, IErrorFixProgressRepository
    {
        public ErrorFixProgressRepository(GRRWSContext context) : base(context) { }

        public async Task<List<ErrorFixProgress>> GetAllByErrorDetailIdAsync(Guid errorDetailId)
        {
            return await _context.ErrorFixProgresses
                .Where(efp => efp.ErrorDetailId == errorDetailId)
                .Include(efp => efp.ErrorFixStep)
                .ToListAsync();
        }

        public async Task<ErrorFixProgress> GetByIdAsync(Guid id)
        {
            return await _context.ErrorFixProgresses
                .Where(efp => efp.Id == id)
                .Include(efp => efp.ErrorFixStep)
                .FirstOrDefaultAsync();
        }
        public async Task AddRangeAsync(IEnumerable<ErrorFixProgress> entities)
        {
            await _context.Set<ErrorFixProgress>().AddRangeAsync(entities);
        }

    }
}