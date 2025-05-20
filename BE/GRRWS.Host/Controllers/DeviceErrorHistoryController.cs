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
        private readonly IDeviceErrorHistoryService _deviceIssueHistoryService;
        public DeviceErrorHistoryController(IDeviceErrorHistoryService deviceIssueHistoryService)
        {
            _deviceIssueHistoryService = deviceIssueHistoryService;
        }
    }
}
