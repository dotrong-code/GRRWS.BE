namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetTaskForMechanic
    {
        public Guid TaskId { get; set; }
        public Guid TaskGroupId { get; set; }
        public int OrderIndex { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public DateTime? CreateDate { get; set; } // Deadline
    }
}
