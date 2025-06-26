namespace GRRWS.Infrastructure.DTOs.Mechanic
{
    public class MechanicPerformanceProfile
    {
        public Guid MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public double AverageTaskCompletionTime { get; set; } // in minutes
        public double AveragePerformanceScore { get; set; } // 0-100
        public int TotalTasksCompleted { get; set; }
        public int TotalTasksOnTime { get; set; }
        public int TotalTasksLate { get; set; }
        public double EfficiencyRating { get; set; }
        public double OnTimePercentage { get; set; }
        public List<TaskTypePerformance> TaskTypePerformance { get; set; } = new();
        public List<PerformanceTrendPoint> RecentPerformanceTrend { get; set; } = new();
        public CurrentShiftInfo? CurrentShiftInfo { get; set; }
        public DateTime? LastPerformanceUpdate { get; set; }
        public int TasksRequiredRework { get; set; }
        public double CustomerSatisfactionScore { get; set; }
        public int SafetyIncidents { get; set; }
    }

    public class TaskTypePerformance
    {
        public string TaskType { get; set; } = string.Empty;
        public int TasksCompleted { get; set; }
        public double AverageCompletionTime { get; set; }
        public double OnTimePercentage { get; set; }
    }

    public class PerformanceTrendPoint
    {
        public DateTime Date { get; set; }
        public int TasksCompleted { get; set; }
        public double AverageScore { get; set; }
        public double OnTimePercentage { get; set; }
    }

    public class CurrentShiftInfo
    {
        public string ShiftName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public Guid? CurrentTaskId { get; set; }
    }

    public class MechanicRanking
    {
        public int Rank { get; set; }
        public Guid MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public double PerformanceScore { get; set; }
        public double EfficiencyRating { get; set; }
        public int TasksCompleted { get; set; }
        public double OnTimePercentage { get; set; }
        public double AverageCompletionTime { get; set; }
    }
}