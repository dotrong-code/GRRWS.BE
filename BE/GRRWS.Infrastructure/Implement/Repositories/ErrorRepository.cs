using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
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
    }
}
