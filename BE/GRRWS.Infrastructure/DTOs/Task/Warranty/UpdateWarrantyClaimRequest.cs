using GRRWS.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class UpdateWarrantyClaimRequest
    {
        public Guid WarrantyClaimId { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string? Resolution { get; set; }
        public string? WarrantyNotes { get; set; }
        public decimal? ClaimAmount { get; set; }
        public WarrantyClaimStatus? ClaimStatus { get; set; }
        public List<IFormFile> DocumentFiles { get; set; } = new();
        public string? DocumentDescription { get; set; }
    }
}
