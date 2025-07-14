using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
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
        public async Task<TechnicalSymptom> GetByIdAsync(Guid id)
        {
            return await _context.TechnicalSymptoms
                .Include(e => e.IssueTechnicalSymptoms)
                .Include(i => i.TechnicalSymptomReports)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }
        public async Task<List<TechnicalSymptomViewDTO>> GetAllTechnicalSymptomsAsync(int pageNumber, int pageSize, string? searchByName)
        {
            var query = _context.TechnicalSymptoms
                .Where(i => !i.IsDeleted);

            if (!string.IsNullOrEmpty(searchByName))
            {
                query = query.Where(i => i.Name != null && i.Name.Contains(searchByName));
            }

            var technicalSymptoms = await query
                .Select(i => new TechnicalSymptomViewDTO
                {
                    SymptomCode = i.SymptomCode,
                    Name = i.Name,
                    Description = i.Description,
                    IsCommon = i.IsCommon,
                    OccurrenceCount = i.OccurrenceCount
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return technicalSymptoms;
        }
        public async Task<bool> UpdateTechnicalSymptomAsync(TechnicalSymptomUpdateDTO updateTechnicalSymptomDto)
        {
            var technicalSymptom = await _context.TechnicalSymptoms.FindAsync(updateTechnicalSymptomDto.Id);
            if (technicalSymptom == null)
            {
                return false;
            }

            technicalSymptom.SymptomCode = updateTechnicalSymptomDto.SymptomCode;
            technicalSymptom.Name = updateTechnicalSymptomDto.Name;
            technicalSymptom.Description = updateTechnicalSymptomDto.Description;
            technicalSymptom.IsCommon = updateTechnicalSymptomDto.IsCommon;
            technicalSymptom.OccurrenceCount = updateTechnicalSymptomDto.OccurrenceCount;
            _context.TechnicalSymptoms.Update(technicalSymptom);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var technicalSymptom = await _context.TechnicalSymptoms.FindAsync(id);
            if (technicalSymptom == null)
            {
                return false;
            }
            technicalSymptom.IsDeleted = true;
            _context.TechnicalSymptoms.Update(technicalSymptom);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
