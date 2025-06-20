using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IHOTDashboardService _hotDashboardService;

        public DashboardController(IHOTDashboardService hotDashboardService)
        {
            _hotDashboardService = hotDashboardService;
        }

        [HttpGet("technical-head-stats")]
        public async Task<IResult> GetTechnicalHeadDashboardStats()
        {
            var result = await _hotDashboardService.GetTechnicalHeadDashboardStatsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Dashboard statistics retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
