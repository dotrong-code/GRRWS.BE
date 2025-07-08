using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.TechnicalSymtom;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class TechnicalSymtomRepository : GenericRepository<TechnicalSymptom>, ITechnicalSymtomRepository
    {
        public TechnicalSymtomRepository(GRRWSContext context) : base(context)
        {
        }
        public async Task<List<SuggestObject>> GetNotFoundTechnicalSymtomDisplayNamesAsync(IEnumerable<Guid> technicalSymtomIds)
        {
            var idSet = technicalSymtomIds.ToHashSet();

            var existingTechnicalSymtoms = await _context.TechnicalSymptoms
                .AsNoTracking()
                .Where(i => idSet.Contains(i.Id) && !i.IsDeleted)
                .Select(i => new { i.Id, i.Name })
                .ToListAsync();

            var foundIds = existingTechnicalSymtoms.Select(i => i.Id).ToHashSet();

            var notFoundIds = idSet.Except(foundIds);

            return notFoundIds
                .Select(id => new SuggestObject
                {
                    Id = id,
                    Name = "(Not found)"
                })
                .ToList();
        }
        public async Task<List<SuggestObject>> GetSymtomSuggestionsAsync(string normalizedQuery, int maxResults)
        {
            if (string.IsNullOrEmpty(normalizedQuery))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(normalizedQuery));
            }

            return await _context.TechnicalSymptoms
                .AsNoTracking()
                .Where(i => i.SymptomCode != null && i.SymptomCode.Contains(normalizedQuery))
                .Select(i => new SuggestObject
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .Take(maxResults)
                .ToListAsync();
        }
        public async Task<List<TechnicalSymtomDTO>> GetSymtomsByIssueIdsAsync(List<Guid> issueIds)
        {
            var symtoms = await _context.IssueTechnicalSymptoms
                .Where(its => issueIds.Contains(its.IssueId) && !its.TechnicalSymptom.IsDeleted)
                .Select(its => new TechnicalSymtomDTO
                {
                    Id = its.TechnicalSymptom.Id,
                    Name = its.TechnicalSymptom.Name
                })
                .Distinct() // Loại bỏ trùng lặp
                .ToListAsync();

            return symtoms;
        }
    }
}
