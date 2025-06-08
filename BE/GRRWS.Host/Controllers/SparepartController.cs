using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Sparepart;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SparepartController : ControllerBase
    {
        private readonly ISparepartService _service;

        public SparepartController(ISparepartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spareparts")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved sparepart")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateSparepartDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Sparepart created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateSparepartDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Sparepart updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Sparepart deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("availability")]
        public async Task<IResult> GetAvailability()
        {
            var result = await _service.GetAvailabilityAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved sparepart availability")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-machine/{machineId}")]
        public async Task<IResult> GetSparepartsByMachineId(Guid machineId)
        {
            var result = await _service.GetSparepartsByMachineIdAsync(machineId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spareparts by machine")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-supplier/{supplierId}")]
        public async Task<IResult> GetSparepartsBySupplier(Guid supplierId)
        {
            var result = await _service.GetSparepartsBySupplierAsync(supplierId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spareparts by supplier")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("low-stock")]
        public async Task<IResult> GetLowStockSpareparts()
        {
            var result = await _service.GetLowStockSparepartsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved low stock spareparts")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("out-of-stock")]
        public async Task<IResult> GetOutOfStockSpareparts()
        {
            var result = await _service.GetOutOfStockSparepartsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved out of stock spareparts")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("machines")]
        public async Task<IResult> GetAllMachines()
        {
            var result = await _service.GetAllMachinesAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all machines")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("suppliers")]
        public async Task<IResult> GetAllSuppliers()
        {
            var result = await _service.GetAllSuppliersAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all suppliers")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}