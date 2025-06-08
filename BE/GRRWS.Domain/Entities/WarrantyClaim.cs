using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class WarrantyClaim : BaseEntity
    {
        public string? ClaimNumber { get; set; }
        public Status ClaimStatus { get; set; } // Submitted, Approved, InProgress, Completed, Rejected
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string? Resolution { get; set; }
        public string? IssueDescription { get; set; }
        public string? WarrantyNotes { get; set; }
        public string? ContractNumber { get; set; }
        public decimal? ClaimAmount { get; set; }
        // Foreign keys
        public Guid DeviceWarrantyId { get; set; }
        public Guid? SubmittedByTaskId { get; set; } // Task that submitted the claim
        public Guid? ReturnTaskId { get; set; } // Task for picking up the device
        public Guid CreatedByUserId { get; set; }

        // Navigation
        public DeviceWarranty DeviceWarranty { get; set; }
        public Tasks? SubmittedByTask { get; set; }
        public Tasks? ReturnTask { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<WarrantyClaimDocument>? Documents { get; set; }
    }
}