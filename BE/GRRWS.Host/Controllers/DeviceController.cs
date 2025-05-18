using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Device;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        
        [HttpPost]
        public async Task<IResult> CreateDevice([FromBody] CreateDeviceRequest request)
        {
            var result = await _deviceService.CreateDeviceAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetDeviceById(Guid id)
        {
            var result = await _deviceService.GetDeviceByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("search")]
        public async Task<IResult> GetAllDevices(
            [FromQuery] string? deviceName = null,
            [FromQuery] string? deviceCode = null,
            [FromQuery] string? status = null,
            [FromQuery] Guid? positionId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _deviceService.GetAllDevicesAsync(deviceName, deviceCode, status, positionId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Devices retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut]
        public async Task<IResult> UpdateDevice([FromBody] UpdateDeviceRequest request)
        {
            var result = await _deviceService.UpdateDeviceAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteDevice(Guid id)
        {
            var result = await _deviceService.DeleteDeviceAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("positions")]
        public async Task<IResult> GetPositionsForDeviceCreation()
        {
            var result = await _deviceService.GetPositionsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Positions retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("positions/{positionId}/zone")]
        public async Task<IResult> GetZoneByPosition(Guid positionId)
        {
            var result = await _deviceService.GetZoneByPositionAsync(positionId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zone retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("zones/{zoneId}/area")]
        public async Task<IResult> GetAreaByZone(Guid zoneId)
        {
            var result = await _deviceService.GetAreaByZoneAsync(zoneId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Area retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
