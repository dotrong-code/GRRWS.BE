using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.IssueDTO;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IIssueRepository : IGenericRepository<Issue>
    {
        Task<List<SuggestObject>> GetIssueSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<SuggestObject>> GetNotFoundIssueDisplayNamesAsync(IEnumerable<Guid> issueIds);
        Task<Issue> GetByIdAsync(Guid id); 
        Task UpdateAsync(Issue issue);
        Task<List<IssueDTO>> GetAllIssuesAsync(int pageNumber, int pageSize, string? searchByName);
        Task<bool> UpdateIssueAsync(UpdateIssueDTO updateIssueDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
