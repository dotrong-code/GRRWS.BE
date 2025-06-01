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
    public class IssueErrorRepository : GenericRepository<IssueError>, IIssueErrorRepository
    {
        public IssueErrorRepository(GRRWSContext context) : base(context) { }

        public async Task CreateRangeAsync(IEnumerable<IssueError> issueErrors)
        {
            await _context.AddRangeAsync(issueErrors);
        }

        public async Task<List<IssueError>> GetByIssueIdAsync(Guid issueId)
        {
            return await _context.IssueErrors
                .Where(ie => ie.IssueId == issueId)
                .Include(ie => ie.Issue)
                .Include(ie => ie.Error)
                .ToListAsync();
        }

        public async Task<List<IssueError>> GetByErrorIdAsync(Guid errorId)
        {
            return await _context.IssueErrors
                .Where(ie => ie.ErrorId == errorId)
                .Include(ie => ie.Issue)
                .Include(ie => ie.Error)
                .ToListAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<IssueError> issueErrors)
        {
            _context.IssueErrors.RemoveRange(issueErrors);
            await Task.CompletedTask;
        }
    }
}
