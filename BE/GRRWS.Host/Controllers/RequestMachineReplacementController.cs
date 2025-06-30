using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestMachineReplacementController : ControllerBase
    {
        private readonly IRequestMachineReplacementService _requestMachineReplacementService;
        public RequestMachineReplacementController(IRequestMachineReplacementService requestMachineReplacementService)
        {
            _requestMachineReplacementService = requestMachineReplacementService;
        }
        [HttpGet("search")]
        public async Task<IResult> GetAllAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? status = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isAscending = true)
        {
            var result = await _requestMachineReplacementService.GetAllAsync(pageNumber, pageSize, status, sortBy, isAscending);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Request Machine Replacements retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

    }
}
