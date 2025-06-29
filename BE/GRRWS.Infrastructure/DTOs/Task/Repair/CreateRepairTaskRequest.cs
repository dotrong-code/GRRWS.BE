namespace GRRWS.Infrastructure.DTOs.Task.Repair
{
    public class CreateRepairTaskRequest
    {
        public Guid RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public Guid? AssigneeId { get; set; }
        public List<Guid> ErrorGuidelineIds { get; set; } = new List<Guid>(); // Required

    }
}
