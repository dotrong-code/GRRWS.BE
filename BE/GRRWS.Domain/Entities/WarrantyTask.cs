using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class WarrantyTask : BaseEntity
    {
        public string? TaskCode { get; set; } // Unique
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Provider { get; set; }
        public string? Status { get; set; } // Assigned, InProgress, Accepted, Rejected, Completed
        public string? ErrorDescription { get; set; }
        public string? ConfirmationResult { get; set; } // Accepted, Rejected, Pending
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
        public string? AppointmentDocumentUrl { get; set; }

        // Foreign keys
        public Guid DeviceId { get; set; }
        public Guid? AssignedStaffId { get; set; }

        // Navigation
        public Device Device { get; set; }
        public User? AssignedStaff { get; set; }
        public ICollection<DeviceWarranty> RelatedWarranties { get; set; } = new List<DeviceWarranty>();
    }
}
