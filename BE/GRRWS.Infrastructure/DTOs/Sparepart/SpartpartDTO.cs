using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class SparepartDto
    {
        public string SparepartCode { get; set; }
        public string SparepartName { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public int StockQuantity { get; set; }
    }
}
