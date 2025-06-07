namespace GRRWS.Domain.Entities
{
    public class RequestIssue : BaseEntity
    {
        public Guid RequestId { get; set; }
        public Guid IssueId { get; set; }
        // Navigation
        public Request Request { get; set; }
        public Issue Issue { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
