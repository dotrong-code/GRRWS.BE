namespace GRRWS.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string? IssueKey { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool IsCommon { get; set; }
        public int? OccurrenceCount { get; set; }

        // Foreign key for WarrantyDetail (1-n relationship)
        public Guid? WarrantyDetailId { get; set; }

        // Navigation
        public WarrantyDetail? WarrantyDetail { get; set; }
        public ICollection<RequestIssue>? RequestIssues { get; set; }
        public ICollection<IssueError>? IssueErrors { get; set; }
    }
}
