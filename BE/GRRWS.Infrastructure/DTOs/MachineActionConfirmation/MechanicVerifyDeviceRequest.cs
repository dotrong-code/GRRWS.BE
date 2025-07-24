using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class MechanicVerifyDeviceRequest
    {
        public Guid DeviceId { get; set; } // DeviceId from scanned QR code
    }
}
