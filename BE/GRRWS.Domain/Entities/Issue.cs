namespace GRRWS.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string? IssueKey { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int? OccurrenceCount { get; set; }
        // Navigation
        public ICollection<RequestIssue>? RequestIssues { get; set; }
        public ICollection<IssueError>? IssueErrors { get; set; }
    }
}
