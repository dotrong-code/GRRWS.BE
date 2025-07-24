using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class ConfirmDoneRequest
    {
        public Guid ConfirmationId { get; set; }
        public Guid DeviceId { get; set; }
        public string SignatureBase64 { get; set; }
        public Guid VerificationToken { get; set; } // Token from QR code
    }
}
