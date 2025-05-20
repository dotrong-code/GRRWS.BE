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

    }

}
