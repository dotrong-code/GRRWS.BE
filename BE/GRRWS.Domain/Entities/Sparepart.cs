using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Sparepart : BaseEntity
    {
        public string? SparepartName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public bool Available { get; set; }
        public ICollection<RepairSparepart> RepairSpareparts { get; set; }
    }
}
