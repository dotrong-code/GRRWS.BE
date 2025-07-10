using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorService
    {
        Task<Result> GetAllErrorsAsync(int pageNumber, int pageSize, string? searchByName);
        Task<Result> UpdateErrorAsync(UpdateErrorDTO updateErrorDTO);
        Task<Result> DeleteErrorsAsync(Guid id);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetErrorSuggestionsAsync(string query, int maxResults);

        Task<Result> GetRecommendedErrorsAsync(IssueIdsRequestDTO dto);

        Task<Result> GetSparepartsByErrorIdAsync(Guid errorId);
        Task<Result> GetErrorsByReportIdWithoutTaskAsync(Guid reportId);
        Task<Result> GetListOfSparepartByErrorAsync(List<Guid> errorIds);
        Task<Result> ImportErrorsAsync(IFormFile file);
    }
}
