using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Area : BaseEntity
    {
        public string? AreaName { get; set; }
        public ICollection<Zone>? Zones { get; set; }
    }
}
