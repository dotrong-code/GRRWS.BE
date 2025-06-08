namespace GRRWS.Infrastructure.DTOs.Task.Replace
{
    public class CreateReplaceTaskRequest
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; } = "Replace"; // Default to Replace
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid NewDevice { get; set; } // Required - New device to be replaced

    }
}
