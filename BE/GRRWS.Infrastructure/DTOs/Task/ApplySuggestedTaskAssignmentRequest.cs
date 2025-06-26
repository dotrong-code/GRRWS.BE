namespace GRRWS.Infrastructure.DTOs.Task
{
    public class ApplySuggestedTaskAssignmentRequest
    {
        public Guid TaskId { get; set; }
        public Guid? MechanicId { get; set; } // Optional - if null, auto-assign best available mechanic
    }
}