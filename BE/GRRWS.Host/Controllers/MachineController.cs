using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Machine;
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
        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _machineService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Machine retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("update")]
        public async Task<IResult> UpdateMachine([FromBody] UpdateMachineRequest updateDTO)
        {
            var result = await _machineService.UpdateMachineAsync(updateDTO);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Machine updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IResult> DeleteMachine(Guid id)
        {
            var result = await _machineService.DeleteMachineAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Machine deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}