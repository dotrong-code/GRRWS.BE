namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateSimpleTaskWeb
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; }
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public List<Guid>? SparepartIds { get; set; } // Optional spareparts only
    }
}