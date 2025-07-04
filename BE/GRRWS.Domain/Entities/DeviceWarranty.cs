﻿namespace GRRWS.Domain.Entities
{
    public class DeviceWarranty : BaseEntity
    {
        public string? Status { get; set; } // InProgress, Completed
        public string? WarrantyType { get; set; } // Manufacturer, Extended, ThirdParty
        public string? WarrantyReason { get; set; } // NewDevice, AfterWarranty, AfterRepair, AfterReplacement
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? Provider { get; set; }
        public string? Location { get; set; } // Location of the warranty service
        public string? WarrantyCode { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
        public string? DocumentUrl { get; set; }
        public string? SparePartCode { get; set; }
        public string? SparePartName { get; set; }
        // Foreign keys
        public Guid DeviceId { get; set; }
        // Navigation
        public Device Device { get; set; }
        public ICollection<WarrantyClaim>? WarrantyClaims { get; set; }
    }
}
