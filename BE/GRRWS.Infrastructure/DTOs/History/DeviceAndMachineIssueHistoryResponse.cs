using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class DeviceAndMachineIssueHistoryResponse
    {
        public List<DeviceIssueHistoryResponse> DeviceHistory { get; set; }
        public List<MachineIssueHistoryResponse> MachineHistory { get; set; }
    }
}
