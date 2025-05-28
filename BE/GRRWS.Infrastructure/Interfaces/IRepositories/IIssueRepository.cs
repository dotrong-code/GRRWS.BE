using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IIssueRepository
    {
        Task<List<SuggestObject>> GetIssueSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<SuggestObject>> GetNotFoundIssueDisplayNamesAsync(IEnumerable<Guid> issueIds);
        Task<Issue> GetByIdAsync(Guid id); 
        Task UpdateAsync(Issue issue);
    }
}
