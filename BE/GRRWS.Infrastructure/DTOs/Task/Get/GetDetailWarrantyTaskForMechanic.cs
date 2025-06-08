namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetDetailWarrantyTaskForMechanic : GetTaskDetailBase
    {
        public string? ClaimNumber { get; set; } // Warranty claim number
        public string? WarrantyProvider { get; set; } // Name of the warranty provider
        public string? WarrantyCode { get; set; } // Name of the device under warranty
        public string? ContractNumber { get; set; } // Contract number for the warranty
        public string? ClaimStatus { get; set; } // Status of the warranty claim
        public DateTime? StartDate { get; set; } // Time device is returned
        public DateTime? ExpectedReturnDate { get; set; } // Expected date to return the device
        public DateTime? ActualReturnDate { get; set; }
        public string? Location { get; set; } // Location of the warranty claim 
        public string? Resolution { get; set; }
        public string? IssueDescription { get; set; }
        public string? WarrantyNotes { get; set; }
        public decimal? ClaimAmount { get; set; }
        public string? HotNumber { get; set; } // Head of technical's phone number
    }
}
