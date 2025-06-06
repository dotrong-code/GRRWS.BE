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
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(GRRWSContext context) : base(context) { }

        public async Task<List<Report>> GetReportsWithRequestAsync()
        {
            return await _context.Set<Report>()
                .Include(r => r.Request)
                .Include(r => r.ErrorDetails)
                .ThenInclude(ri => ri.Error)
                .Include(r => r.TechnicalSymptomReports)
                .ThenInclude(tsr => tsr.TechnicalSymptom)
                .Where(r => r.IsDeleted != true)
                .OrderByDescending(r => r.CreatedDate).ToListAsync();
        }

        public async Task<Report> GetReportWithRequestAsync(Guid id)
        {
            return await _context.Reports
                .Include(r => r.ErrorDetails)
                .ThenInclude(ri => ri.Error)
                .Include(r => r.TechnicalSymptomReports)
                .ThenInclude(tsr => tsr.TechnicalSymptom)
                .Where(r => r.IsDeleted != true)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Report>> GetAllReportAsync()
        {
            return await _context.Set<Report>()
                .Include(r => r.Request)
                .Include(r => r.ErrorDetails)
                .ThenInclude(ri => ri.Error)
                .Include(r => r.TechnicalSymptomReports)
                .ThenInclude(tsr => tsr.TechnicalSymptom)
                .Where(r => r.IsDeleted != true)
                .OrderByDescending(r => r.CreatedDate).ToListAsync();
        }
        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            return await _context.Reports
                .Include(r => r.ErrorDetails)
                .ThenInclude(ri => ri.Error)
                .Include(r => r.TechnicalSymptomReports)
                .ThenInclude(tsr => tsr.TechnicalSymptom)
                .Where(r => r.IsDeleted != true)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task UpdateReportAsync(Report updatedReport, List<Guid> newErrorIds)
        {
            var existingReport = await _context.Reports
                .Include(r => r.ErrorDetails)
                .FirstOrDefaultAsync(r => r.Id == updatedReport.Id);

            if (existingReport == null)
                throw new Exception("Report not found.");

            existingReport.Location = updatedReport.Location;
            existingReport.ModifiedDate = DateTime.Now;
            existingReport.ErrorDetails.Clear();

            var newErrorDetails = newErrorIds.Select(errorId => new ErrorDetail
            {
                ReportId = updatedReport.Id,
                ErrorId = errorId
            }).ToList();

            foreach (var errorDetail in newErrorDetails)
            {
                existingReport.ErrorDetails.Add(errorDetail);
            }

            await _context.SaveChangesAsync();
        }

    }

}
