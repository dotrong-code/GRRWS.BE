using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Report;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceHistoryController : ControllerBase
    {
        private readonly IDeviceHistoryService _service;

        public DeviceHistoryController(IDeviceHistoryService service)
        {
            _service = service;
        }
        [HttpGet("{deviceId}")]
        public async Task<IResult> etDeviceHistoryByDeviceId(Guid deviceId)
        {
            var result = await _service.GetDeviceHistoryByDeviceIdAsync(deviceId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }

}
