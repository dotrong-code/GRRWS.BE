using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Position : BaseEntity
    {

        // Foreign key 
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
        // Navigation
        public ICollection<Device>? Devices { get; set; }
    }
}
