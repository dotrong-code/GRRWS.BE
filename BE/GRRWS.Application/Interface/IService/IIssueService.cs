using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.IssueDTO;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Interface.IService
{
    public interface IIssueService
    {
        Task<Result> GetIssueSuggestionsAsync(string query, int maxResults);
        Task<Result> GetIssueByIdAsync(Guid id);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> ImportIssuesAsync(IFormFile file);
        Task<Result> GetAllIssuesAsync(int pageNumber, int pageSize, string? searchByName);
        Task<Result> UpdateIssueAsync(UpdateIssueDTO updateIssueDTO);
        Task<Result> DeleteIssuesAsync(Guid id);
    }
}
