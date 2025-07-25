using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class MechanicDeviceVerificationResponse
    {
        public Guid ConfirmationId { get; set; }
        public Guid DeviceId { get; set; }
        public string QRCodeData { get; set; } // Base64 or URL for QR code
        public string Message { get; set; }
    }
}
