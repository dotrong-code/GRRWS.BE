using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpPost("import")]
        public async Task<IResult> ImportMachine(IFormFile file)
        {
            var result = await _machineService.ImportMachinesAsync(file);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Machines imported successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("search")]
        public async Task<IResult> GetAllMachines(
    [FromQuery] string? machineName = null,
    [FromQuery] string? machineCode = null,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var result = await _machineService.GetAllMachinesAsync(machineName, machineCode, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Devices retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

    }
}