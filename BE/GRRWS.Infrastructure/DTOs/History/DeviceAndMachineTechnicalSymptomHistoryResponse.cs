using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.History
{
    public class DeviceAndMachineTechnicalSymptomHistoryResponse
    {
        public List<DeviceTechnicalSymptomHistoryResponse> DeviceHistory { get; set; } = new List<DeviceTechnicalSymptomHistoryResponse>();
        public List<MachineTechnicalSymptomHistoryResponse> MachineHistory { get; set; } = new List<MachineTechnicalSymptomHistoryResponse>();
    }
}
