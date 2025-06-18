using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.ErrorFixProgress;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorFixProgressController : ControllerBase
    {
        private readonly IErrorFixProgressService _errorFixProgressService;

        public ErrorFixProgressController(IErrorFixProgressService errorFixProgressService)
        {
            _errorFixProgressService = errorFixProgressService;
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _errorFixProgressService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error fix progress")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-error-detail/{errorDetailId}")]
        public async Task<IResult> GetAllByErrorDetailId(Guid errorDetailId)
        {
            var result = await _errorFixProgressService.GetAllByErrorDetailIdAsync(errorDetailId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error fix progress by error detail ID")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("update-completed")]
        public async Task<IResult> UpdateIsCompleted([FromBody] UpdateIsCompletedRequest request)
        {
            var result = await _errorFixProgressService.UpdateIsCompletedAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Error fix progress updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _errorFixProgressService.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Error fix progress deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}