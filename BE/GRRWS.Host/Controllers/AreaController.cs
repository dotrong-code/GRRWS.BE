using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Area;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        
        [HttpPost]
        public async Task<IResult> CreateArea([FromBody] CreateAreaRequest request)
        {
            var result = await _areaService.CreateAreaAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Area created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetAreaById(Guid id)
        {
            var result = await _areaService.GetAreaByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Area retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpGet("search")]
        public async Task<IResult> GetAllAreas(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _areaService.GetAllAreasAsync(pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Areas retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpPut]
        public async Task<IResult> UpdateArea([FromBody] UpdateAreaRequest request)
        {
            var result = await _areaService.UpdateAreaAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Area updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteArea(Guid id)
        {
            var result = await _areaService.DeleteAreaAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Area deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("{areaId}/zones")]
        public async Task<IResult> GetZonesByArea(
            Guid areaId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _areaService.GetZonesByAreaAsync(areaId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zones retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
