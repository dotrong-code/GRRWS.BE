using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class RepairSparepart : BaseEntity
    {
        public Guid SpareId { get; set; }
        public Guid TaskId { get; set; }
        public Tasks Task { get; set; }
        public Sparepart Sparepart { get; set; }
    }
}
