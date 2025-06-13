using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class UpdateSparepartStockQuantityRequest
    {
        public Guid SparepartId { get; set; }
        public int StockQuantity { get; set; }
    }
}
