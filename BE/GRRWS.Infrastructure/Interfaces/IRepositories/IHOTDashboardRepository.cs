using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.HOTDashboard;

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
    }
}
