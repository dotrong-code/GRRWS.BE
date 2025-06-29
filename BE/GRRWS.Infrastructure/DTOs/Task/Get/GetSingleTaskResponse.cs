namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetSingleTaskResponse
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? AssigneeName { get; set; }
        public Guid? AssigneeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid RequestId { get; set; }
        public bool IsUninstallDevice { get; set; } // Indicates if the device needs to be uninstalled
    }
}