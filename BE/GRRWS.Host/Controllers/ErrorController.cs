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
    }
}
