using GRRWS.Infrastructure.DTOs.Sparepart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class SparePartUsageDto
    {
        public Guid Id { get; set; }
        public Guid? RequestTakeSparePartUsageId { get; set; }
        public Guid SparePartId { get; set; }
        public int QuantityUsed { get; set; }
        public bool IsTakenFromStock { get; set; }
        public List<SparepartDto>? Spareparts { get; set; } = new List<SparepartDto>();
    }
}
