namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class RequestTakeSparePartUsageDto
    {
        public Guid Id { get; set; }
        public string RequestCode { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public Guid? RequestedById { get; set; }
        public string? Status { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public Guid? ConfirmedById { get; set; }
        public string? Notes { get; set; }
        public List<SparePartUsageDto> SparePartUsages { get; set; } = new List<SparePartUsageDto>();
    }
}
