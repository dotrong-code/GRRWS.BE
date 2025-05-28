using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class RequestIssue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Status { get; set; } // e.g., Pending, InProgress, Approved, Rejected
        public string? RejectionReason { get; set; } // e.g., NotSerious, IncorrectReport
        public string? RejectionDetails { get; set; } // Nội dung mô tả lý do từ chối

        // Foreign key 
        public bool IsRejected { get; set; } = false;
        public Guid RequestId { get; set; }
        public Guid IssueId { get; set; }
        // Navigation
        public Request Request { get; set; }
        public Issue Issue { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
