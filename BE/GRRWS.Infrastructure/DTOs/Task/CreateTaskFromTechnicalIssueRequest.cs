namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateTaskFromTechnicalIssueRequest
    {
        public Guid RequestId { get; set; }
        public string TaskType { get; set; } = "Warranty";
        public DateTime StartDate { get; set; }
        public Guid AssigneeId { get; set; }
        public List<Guid> TechnicalIssueIds { get; set; } = new List<Guid>(); // Required
        // NO SparepartIds - warranty tasks don't use spareparts
    }
}