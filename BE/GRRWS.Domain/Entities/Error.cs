namespace GRRWS.Domain.Entities
{
    public class Error : BaseEntity
    {
        public string? ErrorCode { get; set; } // Unique
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? EstimatedRepairTime { get; set; }
        public bool IsCommon { get; set; }
        public int OccurrenceCount { get; set; }
        public string? Severity { get; set; } // Low, Medium, High


        // Navigation
        public ICollection<IssueError>? IssueErrors { get; set; }
        public ICollection<ErrorDetail>? ErrorDetails { get; set; }
        public ICollection<ErrorSparepart>? ErrorSpareparts { get; set; } 

    }
}
