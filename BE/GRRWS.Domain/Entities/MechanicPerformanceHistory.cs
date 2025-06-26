using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class MechanicPerformanceHistory : BaseEntity
    {
        public Guid MechanicId { get; set; }
        public Guid TaskId { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime TaskStartTime { get; set; }
        public DateTime TaskEndTime { get; set; }
        public DateTime? TaskExpectedTime { get; set; }
        public double ActualDurationMinutes { get; set; }
        public double? ExpectedDurationMinutes { get; set; }
        public bool IsOnTime { get; set; }
        public double TimeVarianceMinutes { get; set; } // positive = late, negative = early
        public double QualityScore { get; set; } // 0-100
        public bool RequiredRework { get; set; } = false;
        public string? Notes { get; set; }
        public DateTime RecordedDate { get; set; }
        
        // Navigation properties
        public User? Mechanic { get; set; }
        public Tasks? Task { get; set; }
    }
}