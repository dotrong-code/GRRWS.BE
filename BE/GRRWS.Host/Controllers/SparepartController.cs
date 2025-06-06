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
    }
}
