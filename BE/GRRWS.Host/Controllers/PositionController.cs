using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }
        

        [HttpPost]
        public async Task<IResult> CreatePosition([FromBody] CreatePositionRequest request)
        {
            var result = await _positionService.CreatePositionAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Position created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetPositionById(Guid id)
        {
            var result = await _positionService.GetPositionByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Position retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("search")]
        public async Task<IResult> GetAllPositions(
            [FromQuery] Guid? zoneId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _positionService.GetAllPositionsAsync(zoneId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Positions retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut]
        public async Task<IResult> UpdatePosition([FromBody] UpdatePositionRequest request)
        {
            var result = await _positionService.UpdatePositionAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Position updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeletePosition(Guid id)
        {
            var result = await _positionService.DeletePositionAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Position deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("import")]
        public async Task<IResult> ImportPosition(IFormFile file)
        {
            var result = await _positionService.ImportPositionsAsync(file);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Positions imported successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("by-area/{areaId}")]
        public async Task<IResult> GetPositionsByAreaId(Guid areaId)
        {
            var result = await _positionService.GetPositionsByAreaIdAsync(areaId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Positions retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("{positionId}/requests")]
        public async Task<IResult> GetRequestsByPositionId(Guid positionId)
        {
            var result = await _positionService.GetRequestsByPositionIdAsync(positionId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Requests retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("{positionId}/task-confirmations")]
        public async Task<IResult> GetTaskConfirmationsByPositionId(Guid positionId)
        {
            var result = await _positionService.GetTaskConfirmationsByPositionIdAsync(positionId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task confirmations retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
