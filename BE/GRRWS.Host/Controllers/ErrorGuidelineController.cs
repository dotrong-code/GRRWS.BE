using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.ErrorDetail;
using GRRWS.Infrastructure.DTOs.ErrorGuideline;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorGuidelineController : ControllerBase
    {
        private readonly IErrorGuidelineService _errorGuidelineService;

        public ErrorGuidelineController(IErrorGuidelineService errorGuidelineService)
        {
            _errorGuidelineService = errorGuidelineService;
        }

        [HttpPost]
        public async Task<IResult> CreateErrorGuideline([FromBody] CreateErrorGuidelineRequest request)
        {
            var result = await _errorGuidelineService.CreateErrorGuidelineAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "ErrorGuideline created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _errorGuidelineService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error guideline")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-error/{errorId}")]
        public async Task<IResult> GetAllByErrorId(Guid errorId)
        {
            var result = await _errorGuidelineService.GetAllByErrorIdAsync(errorId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error guidelines by error ID")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateErrorGuideline(Guid id, [FromBody] UpdateErrorGuidelineRequests request)
        {
            var result = await _errorGuidelineService.UpdateErrorGuidelineAsync(id, request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "ErrorGuideline updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _errorGuidelineService.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "ErrorGuideline deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}