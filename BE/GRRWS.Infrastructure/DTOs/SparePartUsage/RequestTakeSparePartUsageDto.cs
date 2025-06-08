using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class RequestTakeSparePartUsageDto
    {
        public Guid Id { get; set; }
        public string RequestCode { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public Guid? RequestedById { get; set; }
        public SparePartRequestStatus Status { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public Guid? ConfirmedById { get; set; }
        public string? Notes { get; set; }
        public List<SparePartUsageDto> SparePartUsages { get; set; } = new List<SparePartUsageDto>();
    }
}
