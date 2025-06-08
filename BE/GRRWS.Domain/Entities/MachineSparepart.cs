using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class MachineSparepart : BaseEntity
    {
        public Guid MachineId { get; set; }
        public Machine Machine { get; set; } = null!;

        public Guid SparepartId { get; set; }
        public Sparepart Sparepart { get; set; } = null!;

        
    }
}
