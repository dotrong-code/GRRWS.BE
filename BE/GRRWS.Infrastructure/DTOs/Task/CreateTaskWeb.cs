namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskWeb
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; } // e.g., "Repair", "Maintenance"
        public DateTime StartDate { get; set; } // Date in string format
        public List<Guid> ErrorIds { get; set; } = new List<Guid>(); // List of error IDs associated with the task
        public Guid AssigneeId { get; set; } // User ID of the assignee
        public List<Guid> SparepartIds { get; set; } = new List<Guid>(); // List of spare part IDs associated with the task

    }
}
