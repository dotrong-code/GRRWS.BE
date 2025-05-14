using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Machine : BaseEntity
    {
        public string? MachineName { get; set; }
        public string? MachineCode { get; set; }
        

        public ICollection<Device>? Devices { get; set; }
    }
}
