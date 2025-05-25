using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GRRWS.Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IHttpContextAccessor _contextAccessor;
        public RequestController(IRequestService requestService, IHttpContextAccessor contextAccessor)
        {
            _requestService = requestService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _requestService.GetAllAsync();
            return result.IsSuccess
    ? ResultExtensions.ToSuccessDetails(result, "Successfully")
    : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("deviceId")]
        public async Task<IResult> GetRequestByDeviceIdAsync(Guid id)
        {
            var result = await _requestService.GetRequestByDeviceIdAsync(id);
            return result.IsSuccess
    ? ResultExtensions.ToSuccessDetails(result, "Successfully")
    : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("userId")]
        public async Task<IResult> GetRequestByUserIdAsync(Guid userId)
        {
            var result = await _requestService.GetRequestByUserIdAsync(userId);
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

        [Authorize]
        [HttpPost("api/Request")]
        public async Task<IResult> Create([FromBody] CreateRequestDTO dto)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _requestService.CreateAsync(dto, c.UserId);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("api/Request/custom")]
        
        
        public async Task<IResult> CreateRequest([FromForm] CreateRequestDTO dto)
        {
            // Assume userId is obtained from authentication (e.g., JWT)
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
           

            var result = await _requestService.CreateAsync(dto, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Request created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("test-create")]
        [Consumes("multipart/form-data")]
        public async Task<IResult> TestCreateRequest([FromForm] TestCreateRequestDTO dto)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _requestService.CreateTestAsync(dto, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Request created successfully for testing")
                : ResultExtensions.ToProblemDetails(result);
        }



        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateRequestDTO dto)
        {
            var result = await _requestService.UpdateAsync(dto, id);
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
        [HttpDelete("Cancel")]
        public async Task<IResult> CancelRequest([FromBody] CancelRequestDTO dto)
        {
            var result = await _requestService.CancelRequestAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{requestId}/issues")]
        public async Task<IResult> GetIssuesByRequestId(Guid requestId)
        {
            var result = await _requestService.GetIssuesByRequestIdAsync(requestId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved issues")
                : ResultExtensions.ToProblemDetails(result);
        }


        //[HttpPost("create-request")]
        //public async Task<IResult> CreateRequest([FromBody] CreateRequest request)
        //{
        //    CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
        //    var result = await _requestService.CreateRequestAsync(request, c.UserId);
        //    return result.IsSuccess
        //        ? ResultExtensions.ToSuccessDetails(result, "Successfully created request")
        //        : ResultExtensions.ToProblemDetails(result);
        //}


        [HttpGet("get-summary")]
        public async Task<IResult> GetSummary()
        {
            var result = await _requestService.GetRequestSummary();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved summary")
                : ResultExtensions.ToProblemDetails(result);
        }

    }
}
