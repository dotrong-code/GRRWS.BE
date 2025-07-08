using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class Request : BaseEntity
    {
        //Area-Zone-Position-Guid(5 char)
        //NVT-A52-1-ABCDE
        public string? RequestTitle { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public bool IsSovled { get; set; } // Indicates if the request has been solved
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public string? RejectionReason { get; set; }
        public string? CompletedDetails { get; set; } // Use when HOT mark it as completed when verifying the request
        // Foreign key 
        public Guid DeviceId { get; set; }
        public Guid RequestedById { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? SerderId { get; set; }
        // Navigation properties
        public Device? Device { get; set; }
        public User? Sender { get; set; }
        public Report? Report { get; set; }
        public ICollection<RequestIssue>? RequestIssues { get; set; }

    }
}
