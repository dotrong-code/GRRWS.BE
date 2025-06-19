using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Import
{
    public class ImportDeviceDto
    {
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Status { get; set; } // Chuỗi để parse sang DeviceStatus
        public bool IsUnderWarranty { get; set; }
        public bool? InUsed { get; set; }
        public string? Specifications { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string? Supplier { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? PositionId { get; set; }
    }
}
