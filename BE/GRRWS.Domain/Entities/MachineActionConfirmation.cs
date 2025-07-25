using GRRWS.Domain.Common;
using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class MachineActionConfirmation : BaseEntity
    {
        public string ConfirmationCode { get; set; } // Unique code (e.g., "STOCKOUT-20250723-001", "INSTALL-20250723-001")
        public DateTime? StartDate { get; set; } // Date of the action (e.g., request, installation)
        public Guid RequestedById { get; set; } // User who initiated the action       
        public Guid? DeviceId { get; set; } // Device involved (old or new, depending on action)
        public Guid TaskId { get; set; } // Associated task 
        public MachineActionStatus Status { get; set; } // Pending, Approved, InProgress, Completed, Rejected, Cancelled   
        public MachineActionType ActionType { get; set; } // StockOut, StockIn, Installation, WarrantySubmission   
        public DateTime? CompletedDate { get; set; } // Date of completion
        public string? Reason { get; set; } // Reason for Rejected    
        public Guid? VerificationToken { get; set; } // Token for QR code verification (new field)
        public DateTime? TokenExpiration { get; set; } // Token expiration time (optional, for security)

        //For Signer
        public Guid? SignerId { get; set; } // User who signed (e.g., HOD for installation)
        public string? SignerRole { get; set; } // Role of signer (e.g., "HOD", "Mechanic")
        public string? SignatureBase64 { get; set; } // Signature as base64 (nullable)
        
        //For Mechanic
        public Guid? AssigneeId { get; set; } // Mechanic assigned to the action
        public bool MechanicConfirm { get; set; } = false; // Mechanic confirmation

        // For Stockkeeper
        public Guid? ApprovedById { get; set; } // User who approved (e.g., stockkeeper)
        public bool StockkeeperConfirm { get; set; } = false; // Stockkeeper confirmation        
        public DateTime? ApprovedDate { get; set; } // Date of approval
        public Guid? MachineId { get; set; } // Machine model (nullable if not applicable)
        //For HOD
        public string? Notes { get; set; } // Additional notes
        public string? DeviceCondition { get; set; } // Device condition at confirmation




        // Navigation properties
        public User RequestedBy { get; set; }
        public User? Assignee { get; set; }
        public User? Signer { get; set; }
        public User? ApprovedBy { get; set; }
        public Device? Device { get; set; }
        public Machine? Machine { get; set; }
        public Tasks? Task { get; set; }
    }
}
