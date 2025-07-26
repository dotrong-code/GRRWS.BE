using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.ErrorDetail;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorDetailController : ControllerBase
    {
        private readonly IErrorDetailService _errorDetailService;

        public ErrorDetailController(IErrorDetailService errorDetailService)
        {
            _errorDetailService = errorDetailService;
        }

        [HttpPost]
        public async Task<IResult> CreateErrorDetail([FromBody] CreateErrorDetailRequest request)
        {
            var result = await _errorDetailService.CreateErrorDetailAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "ErrorDetail created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _errorDetailService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error detail")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("request/{requestId}")]
        public async Task<IResult> GetByRequestId(Guid requestId)
        {
            var result = await _errorDetailService.GetByRequestIdAsync(requestId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error details for request")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _errorDetailService.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "ErrorDetail deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("update-task/{id}")]
        public async Task<IResult> UpdateErrorTask(Guid id, [FromBody] UpdateErrorTaskRequest request)
        {
            var result = await _errorDetailService.UpdateErrorTaskAsync(id, request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Error task updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}