using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Report;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceErrorHistoryController : ControllerBase
    {
        private readonly IDeviceErrorHistoryService _deviceErrorHistoryService;
        public DeviceErrorHistoryController(IDeviceErrorHistoryService deviceErrorHistoryService)
        {
            _deviceErrorHistoryService = deviceErrorHistoryService;
        }
        [HttpGet("{deviceId}")]
        public async Task<IResult> etDeviceHistoryByDeviceId(Guid deviceId)
        {
            var result = await _deviceErrorHistoryService.GetDeviceErrorHistoryByDeviceIdAsync(deviceId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
