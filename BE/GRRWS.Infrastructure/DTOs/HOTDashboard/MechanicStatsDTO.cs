namespace GRRWS.Infrastructure.DTOs.HOTDashboard
{
    public class MechanicStatsDTO
    {
        public int Available { get; set; }
        public int InTask { get; set; }
        public int Total => Available + InTask;
    }
}
