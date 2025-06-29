using GRRWS.Domain.Enum;

namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class CreateWarrantyReturnTaskRequest
    {
        public Guid WarrantyClaimId { get; set; }
        public Guid? AssigneeId { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsEarlyReturn { get; set; } // Indicates if the device is retrieved earlier than ExpectedReturnDate
        public string? WarrantyNotes { get; set; } // Optional notes for the warranty claim
        
    }
}