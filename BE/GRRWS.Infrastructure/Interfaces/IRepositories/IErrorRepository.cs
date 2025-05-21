using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorRepository
    {
        Task<List<SuggestObject>> GetErrorSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<ErrorSimpleDTO>> GetErrorsByIssueIdsAsync(List<Guid> issueIds);
    }
}
