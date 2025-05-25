using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceWarrantyController : ControllerBase
    {
        private readonly IDeviceWarrantyService _deviceWarrantyService;

        public DeviceWarrantyController(IDeviceWarrantyService deviceWarrantyService)
        {
            _deviceWarrantyService = deviceWarrantyService;
        }

        [HttpPost]
        public async Task<IResult> CreateDeviceWarranty([FromBody] CreateDeviceWarrantyRequest request)
        {
            var result = await _deviceWarrantyService.CreateDeviceWarrantyAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device warranty created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetDeviceWarrantyById(Guid id)
        {
            var result = await _deviceWarrantyService.GetDeviceWarrantyByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device warranty retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("search")]
        public async Task<IResult> GetAllDeviceWarranties(
            [FromQuery] Guid? deviceId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _deviceWarrantyService.GetAllDeviceWarrantiesAsync(deviceId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device warranties retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpPut]
        public async Task<IResult> UpdateDeviceWarranty([FromBody] UpdateDeviceWarrantyRequest request)
        {
            var result = await _deviceWarrantyService.UpdateDeviceWarrantyAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device warranty updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteDeviceWarranty(Guid id)
        {
            var result = await _deviceWarrantyService.DeleteDeviceWarrantyAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Device warranty deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("Warranties/{deviceId}")]
        public async Task<IResult> GetAllWarrantiesByDeviceId(Guid deviceId)
        {
            var result = await _deviceWarrantyService.GetAllWarrantiesByDeviceIdAsync(deviceId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranties retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
