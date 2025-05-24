using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class DeviceAndMachineErrorHistoryResponse
    {
        public List<DeviceErrorHistoryResponse> DeviceHistory { get; set; }
        public List<MachineErrorHistoryResponse> MachineHistory { get; set; }
    }
}
