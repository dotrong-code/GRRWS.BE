using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class TaskConfirmationResponseDTO
    {
        public Guid? TaskId { get; set; }
        public Guid? SignerId { get; set; }
        public Guid? DeviceId { get; set; }
        public string? SignerRole { get; set; }
        public string? SignatureBase64 { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceCode { get; set; }
        public string? DeviceCondition { get; set; }
        public string ActionType { get; set; } // Changed from ConfirmationType to ActionType to match MachineActionConfirmation
        public string? Notes { get; set; }
    }
}
