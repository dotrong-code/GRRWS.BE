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
    public class IssueTechnicalSymptomRepository : GenericRepository<IssueTechnicalSymptom>, IIssueTechnicalSymptomRepository
    {
        public IssueTechnicalSymptomRepository(GRRWSContext context) : base(context) { }
        public async Task CreateRangeAsync(IEnumerable<IssueTechnicalSymptom> issueSymptoms)
        {
            await _context.AddRangeAsync(issueSymptoms);
        }

        public async Task<List<IssueTechnicalSymptom>> GetByIssueIdAsync(Guid issueId)
        {
            return await _context.IssueTechnicalSymptoms
                .Where(ie => ie.IssueId == issueId)
                .Include(ie => ie.Issue)
                .Include(ie => ie.TechnicalSymptom)
                .ToListAsync();
        }

        public async Task<List<IssueTechnicalSymptom>> GetBySymptomIdAsync(Guid symptomId)
        {
            return await _context.IssueTechnicalSymptoms
                .Where(ie => ie.TechnicalSymptomId == symptomId)
                .Include(ie => ie.Issue)
                .Include(ie => ie.TechnicalSymptom)
                .ToListAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<IssueTechnicalSymptom> issueSymptoms)
        {
            _context.IssueTechnicalSymptoms.RemoveRange(issueSymptoms);
            await Task.CompletedTask;
        }
    }
}
