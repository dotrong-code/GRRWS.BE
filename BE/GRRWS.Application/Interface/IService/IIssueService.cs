using GRRWS.Application.Common.Result;

namespace GRRWS.Application.Interface.IService
{
    public interface IIssueService
    {
        Task<Result> GetIssueSuggestionsAsync(string query, int maxResults);
        Task<Result> GetIssueByIdAsync(Guid id);
    }
}
