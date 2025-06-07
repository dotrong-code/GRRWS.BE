namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskFromErrorsRequest
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; } = "Repair";
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public List<Guid> ErrorIds { get; set; } = new List<Guid>(); // Required
        public List<Guid>? SparepartIds { get; set; } // Optional - ONLY for errors
    }
}