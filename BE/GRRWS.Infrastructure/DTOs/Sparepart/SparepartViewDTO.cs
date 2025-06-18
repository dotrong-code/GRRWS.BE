using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class SparepartViewDTO
    {
        public Guid Id { get; set; }
        public string SparepartCode { get; set; }
        public string SparepartName { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? ExpectedAvailabilityDate { get; set; }
        public Guid? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Category { get; set; } // Thêm Category
        public List<Guid>? MachineIds { get; set; }
        public List<string>? MachineNames { get; set; }
        public string? ImgUrl { get; set; }
    }
}
