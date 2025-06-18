namespace GRRWS.Domain.Entities
{
    public class WarrantyClaimDocument : BaseEntity
    {
        public string? DocumentType { get; set; } // Receipt, Photos, Report, Certificate
        public string? DocumentName { get; set; }
        public string? DocumentUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? UploadDate { get; set; }
        
        // Foreign keys
        public Guid WarrantyClaimId { get; set; }
        public Guid UploadedByUserId { get; set; }
        
        // Navigation
        public WarrantyClaim WarrantyClaim { get; set; }
        public User UploadedByUser { get; set; }
    }
}