using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Dashboard;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Application.Interface.IService
{
    public interface IHOTDashboardService
    {
        Task<Result> GetTechnicalHeadDashboardStatsAsync();
        Task<Result> GetRequestsContainReportAsync();
        Task<Result> GetReportStatisticsAsync();
        Task<Result> GetTaskStatisticsAsync();
        Task<Result> GetDeviceStatisticsAsync();
        Task<Result> GetTotalTaskRequestReportAsync();
        Task<Result> GetTotalUserByRoleAsync();
        Task<Result> GetTaskCompletionCountByWeekAndMonthAsync();
        Task<Result> GetTop5MostErrorDevicesAsync();
    }
}
