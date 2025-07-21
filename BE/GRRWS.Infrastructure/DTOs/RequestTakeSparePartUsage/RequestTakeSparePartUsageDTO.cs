using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.RequestTakeSparePartUsage
{
    public class RequestTakeSparePartUsageViewDTO
    {
        public string RequestCode { get; set; }
        public DateTime RequestDate { get; set; }
        public Guid? RequestedById { get; set; }
        public Guid? AssigneeId { get; set; }
        public SparePartRequestStatus Status { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public Guid? ConfirmedById { get; set; }
        public string? Notes { get; set; }
    }
}
