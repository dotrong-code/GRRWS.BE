using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class IssueRepository : GenericRepository<Domain.Entities.Issue>, IIssueRepository
    {
        public IssueRepository(GRRWSContext context) : base(context)
        {
        }

        public async Task<List<SuggestObject>> GetIssueSuggestionsAsync(string normalizedQuery, int maxResults)
        {
            if (string.IsNullOrEmpty(normalizedQuery))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(normalizedQuery));
            }

            return await _context.Issues
                .AsNoTracking()
                .Where(i => i.IssueKey != null && i.IssueKey.Contains(normalizedQuery)) // Null check added
                .Select(i => new SuggestObject
                {
                    Id = i.Id,
                    Name = i.DisplayName // Assuming `Name` is the display name
                })
                .Take(maxResults)
                .ToListAsync();
        }

        public async Task<List<SuggestObject>> GetNotFoundIssueDisplayNamesAsync(IEnumerable<Guid> issueIds)
        {
            var idSet = issueIds.ToHashSet();

            // Get all existing issues with their IDs and DisplayNames
            var existingIssues = await _context.Issues
                .AsNoTracking()
                .Where(i => idSet.Contains(i.Id) && !i.IsDeleted)
                .Select(i => new { i.Id, i.DisplayName })
                .ToListAsync();

            var foundIds = existingIssues.Select(i => i.Id).ToHashSet();

            // Find IDs that are missing
            var notFoundIds = idSet.Except(foundIds);

            // Return SuggestObject for each not found ID (DisplayName can be a placeholder)
            return notFoundIds
                .Select(id => new SuggestObject
                {
                    Id = id,
                    Name = "(Not found)"
                })
                .ToList();
        }
        public async Task<Issue> GetByIdAsync(Guid id)
        {
            return await _context.Issues
                .Include(i => i.RequestIssues)
                .Include(i => i.IssueErrors)
                .Include(i => i.WarrantyDetail)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task UpdateAsync(Issue issue)
        {
            _context.Issues.Update(issue);
            await Task.CompletedTask;
        }

    }

}

