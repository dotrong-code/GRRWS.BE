using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class MechanicVerifyInstallationRequest
    {
        public Guid DeviceId { get; set; } // DeviceId from scanned QR code
        public string? reason { get; set; } 
        public string? deviceCondition { get; set; } 
    }
}
