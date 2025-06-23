using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Dashboard;
using GRRWS.Infrastructure.DTOs.HOTDashboard;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IHOTDashboardRepository
    {
        Task<RequestStatsDTO> GetRequestStatsAsync();
        Task<TaskStatsDTO> GetTaskStatsAsync();
        Task<MechanicStatsDTO> GetMechanicStatsAsync();
        Task<List<ChartDataDTO>> GetRequestChartDataAsync();
        Task<List<ChartDataDTO>> GetTaskChartDataAsync();
        Task<List<ChartDataDTO>> GetMechanicChartDataAsync();
        Task<List<RequestDTO>> GetRequestsContainReportAsync();
        Task<ReportStatisticsDTO> GetReportStatisticsAsync();
        Task<TaskStatisticsDTO> GetTaskStatisticsAsync();
        Task<DeviceStatisticsDTO> GetDeviceStatisticsAsync();
        Task<TotalUserByRoleDTO> GetTotalUserByRoleAsync();
        Task<TaskByWeekAndMonthDTO> GetTaskCompletionCountByWeekAndMonthAsync();
    }
}
