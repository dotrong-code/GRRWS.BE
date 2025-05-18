using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class DeviceHistory : BaseEntity
    {
        public string? ActionType { get; set; } // Warranty, Repair, Replacement
        public DateTime EventDate { get; set; }
        public string? Description { get; set; }
        public string? Reason { get; set; }
        public string? Provider { get; set; }
        public decimal? Cost { get; set; }
        public string? ComponentCode { get; set; }
        public string? ComponentName { get; set; }
        public string? Status { get; set; } // Completed, Pending, Failed
        public string? DocumentUrl { get; set; }
        public Guid? RelatedTaskId { get; set; }

        // Foreign keys
        public Guid DeviceId { get; set; }

        // Navigation
        public Device Device { get; set; }
    }
}
