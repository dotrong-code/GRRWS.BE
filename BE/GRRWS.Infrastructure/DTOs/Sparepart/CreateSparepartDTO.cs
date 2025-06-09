using Microsoft.AspNetCore.Http;
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
        public DateTime? ExpectedAvailabilityDate { get; set; }
        public Guid? SupplierId { get; set; }
        public string? Category { get; set; } // Thêm Category
        public IFormFile? ImageFile { get; set; }
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
        public DateTime? ExpectedAvailabilityDate { get; set; }
        public Guid? SupplierId { get; set; }
        public string? Category { get; set; } // Thêm Category
        public IFormFile? ImageFile { get; set; }
    }




}
