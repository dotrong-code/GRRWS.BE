using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ErrorRepository : GenericRepository<Domain.Entities.Error>, IErrorRepository
    {
        public ErrorRepository(GRRWSContext context) : base(context)
        {
        }

        public async Task<List<SuggestObject>> GetErrorSuggestionsAsync(string normalizedQuery, int maxResults)
        {
            if (string.IsNullOrEmpty(normalizedQuery))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(normalizedQuery));
            }

            return await _context.Errors
                .AsNoTracking()
                .Where(i => i.ErrorCode != null && i.ErrorCode.Contains(normalizedQuery)) // Null check added
                .Select(i => new SuggestObject
                {
                    Id = i.Id,
                    Name = i.Name // Assuming `Name` is the display name
                })
                .Take(maxResults)
                .ToListAsync();
        }

        public async Task<List<ErrorSimpleDTO>> GetErrorsByIssueIdsAsync(List<Guid> issueIds)
        {
            var errors = await _context.IssueErrors
                .Where(ie => issueIds.Contains(ie.IssueId) && !ie.Error.IsDeleted)
                .Select(ie => new ErrorSimpleDTO
                {
                    Id = ie.Error.Id,
                    Name = ie.Error.Name
                })
                .Distinct() // Loại bỏ Error trùng lặp
                .ToListAsync();

            return errors;
        }
        public async Task<List<Sparepart>> GetSparepartsByErrorIdAsync(Guid errorId)
        {
            return await _context.ErrorSpareparts
                .Where(es => es.ErrorId == errorId)
                .Include(es => es.Sparepart)
                .Select(es => es.Sparepart)
                .ToListAsync();
        }
        public async Task<List<ErrorSimpleDTO>> GetErrorsByReportIdWithoutTaskAsync(Guid reportId)
        {
            var errors = await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId && ed.TaskId == null && !ed.Error.IsDeleted)
                .Select(ed => new ErrorSimpleDTO
                {
                    Id = ed.Error.Id,
                    Name = ed.Error.Name
                })
                .Distinct()
                .ToListAsync();

            return errors;
        }
        public async Task<List<SuggestObject>> GetNotFoundErrorDisplayNamesAsync(IEnumerable<Guid> errorIds)
        {
            var idSet = errorIds.ToHashSet();

            var existingErrors = await _context.Errors
                .AsNoTracking()
                .Where(i => idSet.Contains(i.Id) && !i.IsDeleted)
                .Select(i => new { i.Id, i.Name })
                .ToListAsync();

            var foundIds = existingErrors.Select(i => i.Id).ToHashSet();

            var notFoundIds = idSet.Except(foundIds);

            return notFoundIds
                .Select(id => new SuggestObject
                {
                    Id = id,
                    Name = "(Not found)"
                })
                .ToList();
        }
    }
}
