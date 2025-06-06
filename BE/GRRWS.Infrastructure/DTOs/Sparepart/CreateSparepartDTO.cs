using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class CreateSparepartDTO
    {
        [Required]
        public string SparepartCode { get; set; }
        [Required]
        public string SparepartName { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public int StockQuantity { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? ExpectedAvailabilityDate { get; set; } // Thêm trường này
    }

    public class UpdateSparepartDTO
    {
        [Required]
        public string SparepartName { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public int StockQuantity { get; set; }
        public string? Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? ExpectedAvailabilityDate { get; set; } // Thêm trường này
    }

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
        public DateTime? ExpectedAvailabilityDate { get; set; } // Thêm trường này
    }

    
}
