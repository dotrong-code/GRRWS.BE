using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IErrorService _errorService;
        public ErrorController(IErrorService errorService)
        {
            _errorService = errorService;
        }
        [HttpGet("suggestions")]
        public async Task<IResult> GetErrorSuggestions(string query, int maxResults)
        {
            var result = await _errorService.GetErrorSuggestionsAsync(query, maxResults);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("recommend")]
        public async Task<IResult> GetRecommendedErrors([FromBody] IssueIdsRequestDTO dto)
        {
            var result = await _errorService.GetRecommendedErrorsAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved recommended errors")
                : ResultExtensions.ToProblemDetails(result);
        }


        [HttpGet("spare-parts")]
        public async Task<IResult> GetSparepartsByErrorId(Guid errorId)
        {
            var result = await _errorService.GetSparepartsByErrorIdAsync(errorId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("report/{reportId}/unassigned")]
        public async Task<IResult> GetErrorsByReportIdWithoutTask(Guid reportId)
        {
            var result = await _errorService.GetErrorsByReportIdWithoutTaskAsync(reportId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved unassigned errors for the report")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("spare-parts/list")]
        public async Task<IResult> GetListOfSparepartByError([FromBody] List<Guid> errorIds)
        {
            var result = await _errorService.GetListOfSparepartByErrorAsync(errorIds);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spare parts for the errors")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
