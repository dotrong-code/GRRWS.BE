namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetTaskDetailBase
    {
        public Guid TaskId { get; set; }
        public Guid DeviceId { get; set; } // Device ID
        public string TaskType { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; } // e.g., Low, Medium, High
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; } // Deadline
        public DateTime? EndTime { get; set; }
        public string? AssigneeName { get; set; }
        public bool IsInstall { get; set; } // True if this is an install task, false if it's an uninstall task
    }
}
