namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskDTO
    {
        public Guid ReportId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public Guid AssigneeId { get; set; }
        public DateTime? DeviceReturnTime { get; set; }
        public string DeviceCondition { get; set; }
        public string ReportNotes { get; set; }
        public Guid CreatedBy { get; set; }
        public List<Guid> ErrorIds { get; set; } // For linking to ErrorDetails
        public List<Guid> SparepartIds { get; set; } // For linking to RepairSpareparts
    }
}
