using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Sparepart;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchSparepartName = null)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize, searchSparepartName);
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
        public async Task<IResult> Create([FromForm] CreateSparepartDTO dto)
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
        public async Task<IResult> GetAvailability([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAvailabilityAsync(pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved sparepart availability")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-machine/{machineId}")]
        public async Task<IResult> GetSparepartsByMachineId(Guid machineId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchSparepartName = null)
        {
            var result = await _service.GetSparepartsByMachineIdAsync(machineId, pageNumber, pageSize, searchSparepartName);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spareparts by machine")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("by-supplier/{supplierId}")]
        public async Task<IResult> GetSparepartsBySupplier(Guid supplierId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchSparepartName = null)
        {
            var result = await _service.GetSparepartsBySupplierAsync(supplierId, pageNumber, pageSize, searchSparepartName);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spareparts by supplier")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("low-stock")]
        public async Task<IResult> GetLowStockSpareparts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchSparepartName = null)
        {
            var result = await _service.GetLowStockSparepartsAsync(pageNumber, pageSize, searchSparepartName);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved low stock spareparts")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("out-of-stock")]
        public async Task<IResult> GetOutOfStockSpareparts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchSparepartName = null)
        {
            var result = await _service.GetOutOfStockSparepartsAsync(pageNumber, pageSize, searchSparepartName);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved out of stock spareparts")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("machines")]
        public async Task<IResult> GetAllMachines([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllMachinesAsync(pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all machines")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("suppliers")]
        public async Task<IResult> GetAllSuppliers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllSuppliersAsync(pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all suppliers")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("update-stock-quantity")]
        public async Task<IResult> UpdateStockQuantity([FromBody] UpdateSparepartStockQuantityRequest dto)
        {
            var result = await _service.UpdateStockQuantityAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, $"Successfully updated stock quantity for sparepart with ID {dto.SparepartId}")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}