using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class Tasks : BaseEntity
    {
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskType { get; set; }
        public Priority Priority { get; set; } // 1 = Low, 2 = Medium, 3 = High
        public Status Status { get; set; } // e.g., Pending, InProgress, Completed
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; } // Deadline
        public DateTime? EndTime { get; set; }
        public Guid AssigneeId { get; set; }
        // Warranty appointment report fields
        public DateTime? DeviceReturnTime { get; set; } // Time device is returned
        public string? DeviceCondition { get; set; } // Condition after repair
        public string? ReportNotes { get; set; } // Additional report details
                                                 // Navigation
        public Guid? WarrantyClaimId { get; set; }
        public User Assignee { get; set; }
        public WarrantyClaim? WarrantyClaim { get; set; }
        public ICollection<ErrorDetail> ErrorDetails { get; set; }
        public ICollection<RepairSparepart>? RepairSpareparts { get; set; }
        public ICollection<TechnicalSymptomReport>? TechnicalSymptomReports { get; set; }
        public ICollection<MechanicShift>? MechanicShifts { get; set; }
    }
}
