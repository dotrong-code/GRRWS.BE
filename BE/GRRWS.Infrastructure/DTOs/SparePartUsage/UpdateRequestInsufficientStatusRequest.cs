using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class UpdateRequestInsufficientStatusRequest
    {
        public Guid RequestTakeSparePartUsageId { get; set; }
        public List<Guid> SparePartIds { get; set; } = new List<Guid>();
        public DateTime ExpectedAvailabilityDate { get; set; }
        public string? Notes { get; set; }
    }
}
