using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.MachineActionConfirmation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineActionConfirmationController : ControllerBase
    {
        private readonly IMachineActionConfirmationService _confirmationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MachineActionConfirmationController(IMachineActionConfirmationService confirmationService, IHttpContextAccessor httpContextAccessor)
        {
            _confirmationService = confirmationService;
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
            var result = await _confirmationService.GetAllAsync(pageNumber, pageSize, status, sortBy, isAscending);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Confirmations retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var result = await _confirmationService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Confirmation retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPut("mechanic-verify-device/{confirmationId}")]
        public async Task<IResult> MechanicVerifyDevice(Guid confirmationId, [FromBody] MechanicVerifyDeviceRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 3) // Ensure only mechanics can verify
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify devices.")));

            var result = await _confirmationService.MechanicVerifyDeviceAsync(confirmationId, request.DeviceId, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device verified successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPut("confirm-had-device/{confirmationId}")]
        public async Task<IResult> StockkeeperConfirmHadDevice(Guid confirmationId,Guid deviceId)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _confirmationService.StockkeeperConfirmHadDevice(confirmationId, currentUser.UserId, deviceId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device confirmed as available successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [Authorize]
        [HttpPut("confirm-taken-device")]
        public async Task<IResult> StockkeeperConfirmTakenDevice(ConfirmDoneRequest confirmationRequest)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _confirmationService.StockkeeperConfirmTakenDevice(confirmationRequest, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device confirmed as taken successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [Authorize]
        [HttpPut("done-install-device/{taskId}")]
        public async Task<IResult> MechanicConfirmInstallation(Guid taskId, Guid newDevice)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 3) // Ensure only mechanics can confirm
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can confirm installation.")));

            var result = await _confirmationService.MechanicConfirmInstallation(taskId, currentUser.UserId, newDevice);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Installation confirmed successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPut("mechanic-verify-installation/{confirmationId}")]
        public async Task<IResult> MechanicVerifyInstallation(Guid confirmationId, [FromBody] MechanicVerifyInstallationRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 3) // Ensure only mechanics can verify
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify installations.")));

            var result = await _confirmationService.MechanicVerifyInstallationAsync(confirmationId, request.DeviceId, currentUser.UserId,request.reason,request.deviceCondition);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Installation verified successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPost("confirm-task")]
        public async Task<IResult> HODConfirmTaskInstall([FromBody] ConfirmTaskRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 1) // Ensure only HOD can sign
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only HOD can confirm tasks.")));

            var result = await _confirmationService.HODConfirmTaskInstall(request, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task confirmed successfully")
                : ResultExtensions.ToProblemDetails(result);
        }


        [Authorize]
        [HttpPut("mechanic-verify-stockin/{confirmationId}")]
        public async Task<IResult> MechanicVerifyStockIn(Guid confirmationId, [FromBody] MechanicVerifyStockInRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 3) // Ensure only mechanics can verify
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify stock-in.")));

            var result = await _confirmationService.MechanicVerifyStockInAsync(confirmationId, request.DeviceId, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Stock-in verified successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [Authorize]
        [HttpPut("stockkeeper-confirm-stockin")]
        public async Task<IResult> StockkeeperConfirmStockIn([FromBody] ConfirmDoneRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 4) // Ensure only stockkeepers can confirm
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm stock-in.")));

            var result = await _confirmationService.StockkeeperConfirmStockIn(request, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Stock-in confirmed successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPut("mechanic-verify-warranty-submission/{confirmationId}")]
        public async Task<IResult> MechanicVerifyWarrantySubmission(Guid confirmationId, [FromBody] MechanicVerifyDeviceRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 3) // Ensure only mechanics can verify
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify warranty submissions.")));

            var result = await _confirmationService.MechanicVerifyWarrantySubmissionAsync(confirmationId, request.DeviceId, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty submission verified successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [Authorize]
        [HttpPut("stockkeeper-confirm-warranty-submission")]
        public async Task<IResult> StockkeeperConfirmWarrantySubmission([FromBody] ConfirmDoneRequest request)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (currentUser.Role != 4) // Ensure only stockkeepers can confirm
                return ResultExtensions.ToProblemDetails(Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm warranty submission handover.")));

            var result = await _confirmationService.StockkeeperConfirmWarrantySubmission(request, currentUser.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty submission handover confirmed successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }

    
}