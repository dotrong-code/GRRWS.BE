using GRRWS.Domain.Common;
using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class TaskConfirmation : BaseEntity
    {
        public Guid TaskId { get; set; } // Foreign key to Tasks
        public Guid? SignerId { get; set; } // User who signed (Mechanic, HOD, or HOT), nullable
        public Guid DeviceId { get; set; }
        public string SignerRole { get; set; } // Role of the signer (e.g., "Mechanic", "HOD", "HOT")
        public string? SignatureBase64 { get; set; } // Signature stored as base64 string, nullable
        public string? DeviceName { get; set; }
        public string? DeviceCode { get; set; }
        public string? DeviceCondition { get; set; } // Device condition at time of signing
        public string ConfirmationType { get; set; } // Type of confirmation (e.g., "Uninstall", "Install", "WarrantySubmission")
        public string? Notes { get; set; } // Additional notes (optional)

        // Navigation properties
        public Tasks Task { get; set; }
        public User? Signer { get; set; } // Nullable navigation property
    }

}