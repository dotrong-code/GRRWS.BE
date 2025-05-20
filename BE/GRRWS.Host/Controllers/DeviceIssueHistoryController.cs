using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Report;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceIssueHistoryController : ControllerBase
    {
        private readonly IDeviceIssueHistoryService _deviceIssueHistoryService;
        public DeviceIssueHistoryController(IDeviceIssueHistoryService deviceIssueHistoryService)
        {
            _deviceIssueHistoryService = deviceIssueHistoryService;
        }
        [HttpGet("{deviceId}")]
        public async Task<IResult> etDeviceHistoryByDeviceId(Guid deviceId)
        {
            var result = await _deviceIssueHistoryService.GetDeviceIssueHistoryByDeviceIdAsync(deviceId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
