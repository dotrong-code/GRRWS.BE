using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Position : BaseEntity
    {
        public int Index { get; set; }
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
        public Device Device { get; set; }
    }
}
