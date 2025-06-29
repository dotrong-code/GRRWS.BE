using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyClaimController : ControllerBase
    {
        private readonly IWarrantyClaimService _warrantyClaimService;

        public WarrantyClaimController(IWarrantyClaimService warrantyClaimService)
        {
            _warrantyClaimService = warrantyClaimService;
        }

        [HttpGet("{warrantyClaimId}")]
        public async Task<IResult> GetWarrantyClaimWithTasks(Guid warrantyClaimId)
        {
            var result = await _warrantyClaimService.GetWarrantyClaimWithTasksAsync(warrantyClaimId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty claim retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet]
        public async Task<IResult> GetWarrantyClaimsWithTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _warrantyClaimService.GetWarrantyClaimsWithTasksAsync(pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty claims retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-task/{taskId}")]
        public async Task<IResult> GetWarrantyClaimByTaskId(Guid taskId, [FromQuery] string taskType)
        {
            var result = await _warrantyClaimService.GetWarrantyClaimByTaskIdAsync(taskId, taskType);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty claim retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}