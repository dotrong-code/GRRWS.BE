using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Zone : BaseEntity
    {
        public string? ZoneName { get; set; }

        public Guid AreaId { get; set; }
        public Area Area { get; set; }

        public ICollection<Position>? Positions { get; set; }
    }
}
