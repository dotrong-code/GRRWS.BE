using System;
using System.Collections.Generic;
using GRRWS.Infrastructure.DTOs.SparePartUsage;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class RequestTakeSparePartUsageDto
    {
        public Guid Id { get; set; }
        public string RequestCode { get; set; }
        public DateTime RequestDate { get; set; }
        public Guid? RequestedById { get; set; }
        public Guid? AssigneeId { get; set; }
        public string? AssigneeName { get; set; }
        public string? Status { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public Guid? ConfirmedById { get; set; }
        public string Notes { get; set; }
        public List<SparePartUsageDto> SparePartUsages { get; set; }
    }
}