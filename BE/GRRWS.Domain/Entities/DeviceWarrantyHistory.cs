using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class DeviceWarrantyHistory : BaseEntity
    {
        public Guid DeviceId { get; set; }
        public string DeviceDescription { get; set; }
        public bool Status { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string? Provider { get; set; }
        public string? Note { get; set; }
        public Device Device { get; set; }
    }
}
