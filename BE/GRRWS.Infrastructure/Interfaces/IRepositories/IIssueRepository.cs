using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IIssueRepository
    {
        Task<List<SuggestObject>> GetIssueSuggestionsAsync(string normalizedQuery, int maxResults);
    }
}
