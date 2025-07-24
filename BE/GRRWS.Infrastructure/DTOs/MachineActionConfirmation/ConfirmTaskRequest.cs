using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.MachineActionConfirmation
{
    public class ConfirmTaskRequest
    {
        public Guid TaskId { get; set; }
        public string SignatureBase64 { get; set; }
        public string DeviceCondition { get; set; }
        public Guid? VerificationToken { get; set; } // Token from QR code for HOD confirmation
    }
}
