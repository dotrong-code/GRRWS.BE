using GRRWS.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class FillInWarrantyTask
    {
        public Guid TaskId { get; set; }
        public DateTime? ExpectedReturnDate { get; set; } // Expected return date from warranty provider
        public string? Resolution { get; set; } // Outcome of the warranty claim
        public string? ContractNumber { get; set; } // Warranty contract number
        public decimal? ClaimAmount { get; set; } // Repair cost, if applicable
        public string? WarrantyNotes { get; set; } // Additional notes
        public WarrantyClaimStatus? ClaimStatus { get; set; } // Status of the claim
        public bool CreateReturnTaskNow { get; set; } // Whether to create a return task immediately
        public DateTime? ReturnTaskStartTime { get; set; } // Start time for the return task, if created
        public List<IFormFile> DocumentFiles { get; set; } = new(); // Photos or documents (e.g., appointment slip)
        public string? DocumentDescription { get; set; } // Description for uploaded documents
    }
}
