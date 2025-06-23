using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class DeviceStatisticsDTO
    {
        public int TotalDevices { get; set; }
        public int TotalActiveDevices { get; set; }
        public int TotalInUseDevices { get; set; }
        public int TotalInRepairDevices { get; set; }
        public int TotalInWarrantyDevices { get; set; }
        public int TotalDecommissionedDevices { get; set; }
    }
}
