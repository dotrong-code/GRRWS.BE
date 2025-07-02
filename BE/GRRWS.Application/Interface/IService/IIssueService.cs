using GRRWS.Application.Common.Result;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Interface.IService
{
    public interface IIssueService
    {
        Task<Result> GetIssueSuggestionsAsync(string query, int maxResults);
        Task<Result> GetIssueByIdAsync(Guid id);
        Task<Result> ImportIssuesAsync(IFormFile file);
    }
}
