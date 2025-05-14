using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _requestService.GetAllAsync();
            return result.IsSuccess
    ? ResultExtensions.ToSuccessDetails(result, "Successfully")
    : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _requestService.GetByIdAsync(id);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateRequestDTO dto)
        {
            var result = await _requestService.CreateAsync(dto);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateRequestDTO dto)
        {
            var result = await _requestService.UpdateAsync(dto);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _requestService.DeleteAsync(id);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }
    }
}
