using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class TaskConfirmationDTO
    {
        public Guid TaskId { get; set; }
        public Guid? SignerId { get; set; } // Nullable, may be pre-filled for HOD
        public Guid DeviceId { get; set; }
        public string SignerRole { get; set; } // "Mechanic", "HOD", "HOT"
        public string? SignatureBase64 { get; set; } // Nullable
        public string? DeviceCondition { get; set; }
        public string ConfirmationType { get; set; } // "Uninstall", "Install", "WarrantySubmission"
        public string? Notes { get; set; }

    }

}
