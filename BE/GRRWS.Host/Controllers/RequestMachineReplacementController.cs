using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestMachineReplacement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestMachineReplacementController : ControllerBase
    {
        private readonly IRequestMachineReplacementService _requestMachineReplacementService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestMachineReplacementController(IRequestMachineReplacementService requestMachineReplacementService, IHttpContextAccessor httpContextAccessor)
        {
            _requestMachineReplacementService = requestMachineReplacementService;
            _httpContextAccessor = httpContextAccessor;
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
        [Authorize]
        [HttpPut("confirm-taken-device/{requestMachineId}")]
        public async Task<IResult> ConfirmTakenDevice(Guid requestMachineId)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _requestMachineReplacementService.ConfirmTakenDevice(requestMachineId, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device confirmed as taken successfully")
                : ResultExtensions.ToProblemDetails(result);

        }
        [Authorize]
        [HttpPut("confirm-had-device/{requestMachineId}")]
        public async Task<IResult> ConfirmHadDevice(Guid requestMachineId)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _requestMachineReplacementService.ConfirmHadDevice(requestMachineId, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device confirmed as had successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IResult> UpdateRequestMachineReplacement([FromBody] UpdateRMR updateRMR)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _requestMachineReplacementService.UpdateRequestMachineReplacement(updateRMR);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Request Machine Replacement updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}