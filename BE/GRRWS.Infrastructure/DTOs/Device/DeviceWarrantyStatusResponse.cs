namespace GRRWS.Infrastructure.DTOs.Device
{
    public class DeviceWarrantyStatusResponse
    {
        public Guid Id { get; set; } // Unique identifier for the warranty status
        public bool IsUnderWarranty { get; set; }
        public string WarrantyStatus { get; set; }
        public string WarrantyCode { get; set; }
        public string WarrantyType { get; set; }
        public string WarrantyReason { get; set; }
        public string Provider { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Notes { get; set; }
        public decimal? Cost { get; set; }
        public string DocumentUrl { get; set; }
        public int? DaysRemaining { get; set; } // New field: Number of days remaining
        public bool LowDayWarning { get; set; } // New field: True if <= 10 days remaining
    }
}
