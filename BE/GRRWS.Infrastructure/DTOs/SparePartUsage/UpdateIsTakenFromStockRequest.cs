using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class UpdateIsTakenFromStockRequest
    {
        public List<Guid> SparePartUsageIds { get; set; }
        public bool IsTakenFromStock { get; set; }
    }
}
