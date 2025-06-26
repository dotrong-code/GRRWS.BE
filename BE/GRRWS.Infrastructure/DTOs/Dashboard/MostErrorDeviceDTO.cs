using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class MostErrorDeviceDTO
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int ErrorCount { get; set; }
        public int MechanicFixCount { get; set; }
        public int RequestCountByMonth { get; set; }
    }
}
