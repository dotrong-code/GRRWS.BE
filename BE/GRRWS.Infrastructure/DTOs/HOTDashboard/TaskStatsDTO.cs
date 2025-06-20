namespace GRRWS.Infrastructure.DTOs.HOTDashboard
{
    public class TaskStatsDTO
    {
        public int Pending { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
        public int Total => Pending + InProgress + Completed;
    }
}
