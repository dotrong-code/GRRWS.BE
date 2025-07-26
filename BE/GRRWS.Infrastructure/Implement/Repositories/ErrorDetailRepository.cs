using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ErrorDetailRepository : GenericRepository<ErrorDetail>, IErrorDetailRepository
    {
        public ErrorDetailRepository(GRRWSContext context) : base(context) { }

        public async Task<List<ErrorDetail>> GetByRequestIdAsync(Guid requestId)
        {
            // Get the reportId from the request first
            var reportId = await GetReportIdByRequestIdAsync(requestId);
            
            return await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId /*&& !ed.IsDeleted*/)
                .Include(ed => ed.Error)
                .ToListAsync();
        }

        public async Task<bool> ErrorDetailExistsAsync(Guid errorId, Guid reportId)
        {
            return await _context.ErrorDetails
                .AnyAsync(ed => ed.ErrorId == errorId && ed.ReportId == reportId /*&& !ed.IsDeleted*/);
        }

        public async Task<Guid> GetReportIdByRequestIdAsync(Guid requestId)
        {
            return await _context.Requests
                .Where(r => r.Id == requestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync() ?? Guid.Empty;
        }
        public async Task<ErrorDetail> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.ErrorDetails
                .Include(ed => ed.Error)
   
                .FirstOrDefaultAsync(ed => ed.Id == id && !ed.IsDeleted);
        }
        
    }
}