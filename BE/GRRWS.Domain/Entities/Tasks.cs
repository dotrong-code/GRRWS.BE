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
        public DateTime? EndTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public Guid? AssigneeId { get; set; }
        public bool? IsUninstall { get; set; }
        public bool? IsInstall { get; set; }
        public bool? IsSigned { get; set; } = false;
        public string? ReportNotes { get; set; }
        public Guid? WarrantyClaimId { get; set; }
        public Guid? TaskGroupId { get; set; } // Grouping tasks together
        

        #region relationships
        public User? Assignee { get; set; }
        public WarrantyClaim? WarrantyClaim { get; set; }
        public TaskGroup? TaskGroup { get; set; } // Group this task belongs to
       
        public ICollection<MachineActionConfirmation> ActionConfirmations { get; set; }
        public ICollection<ErrorDetail> ErrorDetails { get; set; }
        public ICollection<TechnicalSymptomReport>? TechnicalSymptomReports { get; set; }
        public ICollection<MechanicShift>? MechanicShifts { get; set; }     
        
        #endregion
    }
}