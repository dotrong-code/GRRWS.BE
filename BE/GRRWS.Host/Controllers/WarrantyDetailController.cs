using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.WarrantyDetail;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyDetailController : ControllerBase
    {
        private readonly IWarrantyDetailService _warrantyDetailService;

        public WarrantyDetailController(IWarrantyDetailService warrantyDetailService)
        {
            _warrantyDetailService = warrantyDetailService;
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateWarrantyDetailDTO dto)
        {
            var result = await _warrantyDetailService.CreateAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "WarrantyDetail created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _warrantyDetailService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "WarrantyDetail retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _warrantyDetailService.GetAllAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "WarrantyDetails retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateWarrantyDetailDTO dto)
        {
            var result = await _warrantyDetailService.UpdateAsync(id, dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "WarrantyDetail updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _warrantyDetailService.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "WarrantyDetail deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
