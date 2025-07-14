namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class CreateWarrantyReturnTaskRequest
    {
        public Guid WarrantyClaimId { get; set; }
        public Guid? AssigneeId { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsEarlyReturn { get; set; } // Indicates if the device is retrieved earlier than ExpectedReturnDate
        public string? WarrantyNotes { get; set; } // Optional notes for the warranty claim
        public bool IsWarrantyFailed { get; set; } = false; // Indicates if the warranty claim has failed
        public bool IsReInstallOldDevice { get; set; } = true; // Indicates if the old device is reinstalled
    }
}