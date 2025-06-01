namespace GRRWS.Domain.Entities
{
    public class Request : BaseEntity
    {
        //Area-Zone-Position-Guid(5 char)
        //NVT-A52-1-ABCDE
        public string? RequestTitle { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // e.g., Pending, Inprogress,Approved, Denied
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }

        public bool IsRejected { get; set; } = false;
        public string? RejectionReason { get; set; }
        public string? RejectionDetails { get; set; }

        // Foreign key 
        public Guid DeviceId { get; set; }
        public Guid RequestedById { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? SerderId { get; set; }

        // Navigation properties
        public Device Device { get; set; }
        public User Sender { get; set; }
        public Report? Report { get; set; }
        public ICollection<RequestIssue>? RequestIssues { get; set; }

    }
}
