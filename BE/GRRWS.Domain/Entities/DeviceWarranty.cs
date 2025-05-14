using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class DeviceWarranty : BaseEntity
    {
        public string? Status { get; set; } // Pending, InProgress, Completed, Rejected
        public string? WarrantyType { get; set; } // Manufacturer, Extended, ThirdParty
        public string? WarrantyReason { get; set; } // NewDevice, AfterWarranty, AfterRepair, AfterReplacement
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? Provider { get; set; }
        public string? WarrantyCode { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
        public string? DocumentUrl { get; set; }

        // Foreign keys
        public Guid DeviceId { get; set; }
        

        // Navigation
        public Device Device { get; set; }
       
    }
}
