using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
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

        [HttpGet("{id}/spare-parts")]
        public async Task<IResult> GetSparepartsByErrorId(Guid errorId)
        {
            var result = await _errorService.GetSparepartsByErrorIdAsync(errorId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
