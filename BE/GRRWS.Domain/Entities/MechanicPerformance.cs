namespace GRRWS.Domain.Entities
{
    public class MechanicPerformance : BaseEntity
    {
        public Guid MechanicId { get; set; }
        public double? AverageTaskCompletionTime { get; set; } // in minutes
        public double? AveragePerformanceScore { get; set; } // 0-100 scale
        public int TotalTasksCompleted { get; set; } = 0;
        public int TotalTasksOnTime { get; set; } = 0;
        public int TotalTasksLate { get; set; } = 0;
        public double? EfficiencyRating { get; set; } // calculated field
        public DateTime? LastPerformanceUpdate { get; set; }
        
        // Task type specific performance
        public double? AverageRepairTime { get; set; }
        public double? AverageInstallationTime { get; set; }
        public double? AverageUninstallationTime { get; set; }
        public double? AverageWarrantyProcessingTime { get; set; }
        
        // Task type completion counts
        public int RepairTasksCompleted { get; set; } = 0;
        public int InstallationTasksCompleted { get; set; } = 0;
        public int UninstallationTasksCompleted { get; set; } = 0;
        public int WarrantyTasksCompleted { get; set; } = 0;
        
        // Quality metrics
        public int TasksRequiredRework { get; set; } = 0;
        public double? CustomerSatisfactionScore { get; set; } // if available
        public int SafetyIncidents { get; set; } = 0;
        
        // Navigation property
        public User? Mechanic { get; set; }
    }
}