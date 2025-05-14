using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Request : BaseEntity
    {
        public string? RequestTitle { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // e.g., Pending, Approved, Denied

        // Foreign key 
        public Guid? SenderId { get; set; } 
        public Guid? ReceiverId { get; set; } 
        public Guid? DeviceId { get; set; } 
        public Guid? RequestedById { get; set; }


        // Navigation properties
        public Device? Device { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
        public Report? Report { get; set; }
        public ICollection<RequestIssue>? RequestIssues { get; set; }
        
    }
}
