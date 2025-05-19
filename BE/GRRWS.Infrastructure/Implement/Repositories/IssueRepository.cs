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
    }

}

