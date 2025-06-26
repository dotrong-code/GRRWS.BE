using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class Top3MechanicDTO
    {
        public Guid MechanicId { get; set; }
        public string MechanicName { get; set; }
        public int CompletedTaskCount { get; set; }
    }
}
