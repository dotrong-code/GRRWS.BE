using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Zone;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpPost]
        public async Task<IResult> CreateZone([FromBody] CreateZoneRequest request)
        {
            var result = await _zoneService.CreateZoneAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zone created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetZoneById(Guid id)
        {
            var result = await _zoneService.GetZoneByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zone retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("search")]
        public async Task<IResult> GetAllZones(
            [FromQuery] Guid? areaId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _zoneService.GetAllZonesAsync(areaId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zones retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut]
        public async Task<IResult> UpdateZone([FromBody] UpdateZoneRequest request)
        {
            var result = await _zoneService.UpdateZoneAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zone updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteZone(Guid id)
        {
            var result = await _zoneService.DeleteZoneAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Zone deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
