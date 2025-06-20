using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.HOTDashboard;

public class DashboardStatsDTO
{
    public RequestStatsDTO RequestStats { get; set; } = new();
    public TaskStatsDTO TaskStats { get; set; } = new();
    public MechanicStatsDTO MechanicStats { get; set; } = new();
    public List<ChartDataDTO> RequestChart { get; set; } = new();
    public List<ChartDataDTO> TaskChart { get; set; } = new();
    public List<ChartDataDTO> MechanicChart { get; set; } = new();
}