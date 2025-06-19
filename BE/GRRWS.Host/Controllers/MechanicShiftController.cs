using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.MechanicShift;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicShiftController : ControllerBase
    {
        private readonly IMechanicShiftService _mechanicShiftService;
        public MechanicShiftController(IMechanicShiftService mechanicShiftService)
        {
            _mechanicShiftService = mechanicShiftService;
        }
        [HttpPost("create-mechanic-shift")]
        public async Task<IResult> CreateMechanicShift([FromBody] MechanicShiftDTO dto)
        {
            var result = await _mechanicShiftService.CreateMechanicShiftAsync(dto.MechanicId, dto.TaskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Mechanic shift created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-all")]
        public async Task<IResult> GetAllMechanicShiftAsync()
        {
            var result = await _mechanicShiftService.GetAllMechanicShiftAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("update-mechanic-shift-status-to-available/{mechanicShiftId}")]
        public async Task<IResult> UpdateMechanicShiftStatusToAvailableAsync(Guid mechanicShiftId)
        {
            var result = await _mechanicShiftService.UpdateMechanicShiftStatusToAvailableAsync(mechanicShiftId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Mechanic shift status updated to available successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
