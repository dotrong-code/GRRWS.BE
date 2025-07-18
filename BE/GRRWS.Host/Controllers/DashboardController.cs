﻿using GRRWS.Application.Common.Result;
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
        [HttpGet("get-requests-contain-report")]
        public async Task<IResult> GetRequestsContainReportAsync()
        {
            var result = await _hotDashboardService.GetRequestsContainReportAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get requests contain report successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-report-statistics")]
        public async Task<IResult> GetReportStatisticsAsync()
        {
            var result = await _hotDashboardService.GetReportStatisticsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get report statistics successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-task-statistics")]
        public async Task<IResult> GetTaskStatisticsAsync()
        {
            var result = await _hotDashboardService.GetTaskStatisticsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get task statistics successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-device-statistics")]
        public async Task<IResult> GetDeviceStatisticsAsync()
        {
            var result = await _hotDashboardService.GetDeviceStatisticsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get task statistics successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-total-task-request-report")]
        public async Task<IResult> GetTotalTaskRequestReportAsync()
        {
            var result = await _hotDashboardService.GetTotalTaskRequestReportAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get task statistics successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-total-user-by-role")]
        public async Task<IResult> GetTotalUserByRoleAsync()
        {
            var result = await _hotDashboardService.GetTotalUserByRoleAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get total user by role successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-task-completion-count-by-week-and-month")]
        public async Task<IResult> GetTaskCompletionCountByWeekAndMonthAsync()
        {
            var result = await _hotDashboardService.GetTaskCompletionCountByWeekAndMonthAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get task completion count by week and month successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-top-5-most-error-devices")]
        public async Task<IResult> GetTop5MostErrorDevicesAsync()
        {
            var result = await _hotDashboardService.GetTop5MostErrorDevicesAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get top 5 most error devices successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-top-3-mechanics")]
        public async Task<IResult> GetTop3MechanicsAsync()
        {
            var result = await _hotDashboardService.GetTop3MechanicsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get top 3 mechanics successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("get-monthly-request-count-for-last-6-months")]
        public async Task<IResult> GetMonthlyRequestCountForLast6MonthsAsync()
        {
            var result = await _hotDashboardService.GetMonthlyRequestCountForLast6MonthsAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get monthly request count for last 6 months successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
