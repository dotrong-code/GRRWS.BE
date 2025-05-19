using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Device
{
    public class UpdateDeviceRequest
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public bool IsUnderWarranty { get; set; }
        public string? Specifications { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string? Supplier { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? PositionId { get; set; }
    }
}
