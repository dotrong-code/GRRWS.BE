using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class Tasks : BaseEntity
    {
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public int? OrderIndex { get; set; }
        public TaskType? TaskType { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? AssigneeId { get; set; }

        public bool? IsUninstall { get; set; }
        public DateTime? DeviceReturnTime { get; set; } // Time device is returned
        public string? DeviceCondition { get; set; } // Condition after repair
        public string? ReportNotes { get; set; }

        public Guid? WarrantyClaimId { get; set; }
        public Guid? TaskGroupId { get; set; } // Grouping tasks together
        #region relationships
        public User? Assignee { get; set; }
        public WarrantyClaim? WarrantyClaim { get; set; }
        public TaskGroup? TaskGroup { get; set; } // Group this task belongs to

        public ICollection<ErrorDetail> ErrorDetails { get; set; }
        public ICollection<RepairSparepart>? RepairSpareparts { get; set; }
        public ICollection<TechnicalSymptomReport>? TechnicalSymptomReports { get; set; }
        public ICollection<MechanicShift>? MechanicShifts { get; set; }
        public RequestMachineReplacement? RequestMachineReplacement { get; set; }
        #endregion
    }
}
