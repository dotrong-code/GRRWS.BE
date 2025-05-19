using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.DeviceWarranty
{
    public class GetDeviceWarrantyResponse
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? WarrantyType { get; set; }
        public string? WarrantyReason { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? Provider { get; set; }
        public string? WarrantyCode { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
        public string? DocumentUrl { get; set; }
        public string? SparePartCode { get; set; }
        public string? SparePartName { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
