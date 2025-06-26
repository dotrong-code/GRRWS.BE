using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ErrorGuidelineRepository : GenericRepository<ErrorGuideline>, IErrorGuidelineRepository
    {
        public ErrorGuidelineRepository(GRRWSContext context) : base(context) { }
        public async Task<List<ErrorGuideline>> GetAllByErrorIdAsync(Guid errorId)
        {
            return await _context.ErrorGuidelines
                .Where(eg => eg.ErrorId == errorId)
                .Include(eg => eg.ErrorFixSteps)
                .Include(eg => eg.ErrorSpareparts)
                .ToListAsync();
        }
        public async Task<ErrorGuideline> GetByIdInclueErrorFixStepErrorSparepartssAsync(Guid id)
        {
            return await _context.ErrorGuidelines
            .Where(eg => eg.Id == id)
            .Include(eg => eg.ErrorFixSteps)
            .Include(eg => eg.ErrorSpareparts)
            .FirstOrDefaultAsync();
        }

        public async Task<ErrorGuideline> GetFirstByErrorIdAsync(Guid errorId)
        {
            return await _context.ErrorGuidelines
                .Where(eg => eg.ErrorId == errorId)
                .Include(eg => eg.ErrorFixSteps)
                .Include(eg => eg.ErrorSpareparts)
                .FirstOrDefaultAsync(); // Assuming you want the first one
        }
    }
}
