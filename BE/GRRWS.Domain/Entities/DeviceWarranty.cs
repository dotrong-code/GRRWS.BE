using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class DeviceWarranty : BaseEntity
    {
        public string? Status { get; set; }
        
       

        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        // Foreign key 
        public Guid DeviceId { get; set; }
        // Navigation
        public Device Device { get; set; }
    }
}
