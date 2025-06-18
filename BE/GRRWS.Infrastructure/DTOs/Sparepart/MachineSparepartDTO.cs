using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class MachineSparepartDTO
    {
        public Guid SparepartId { get; set; }
        public string SparepartName { get; set; }
        public Guid MachineId { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public Guid? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Category { get; set; }
    }
}
