using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IIssueErrorRepository  : IGenericRepository<IssueError>
    {
        Task CreateRangeAsync(IEnumerable<IssueError> issueErrors);
        Task<List<IssueError>> GetByIssueIdAsync(Guid issueId);
        Task<List<IssueError>> GetByErrorIdAsync(Guid errorId);
        Task DeleteRangeAsync(IEnumerable<IssueError> issueErrors);
        Task<IssueError> GetByIssueAndErrorIdAsync(Guid issueId, Guid errorId);
    }
}
