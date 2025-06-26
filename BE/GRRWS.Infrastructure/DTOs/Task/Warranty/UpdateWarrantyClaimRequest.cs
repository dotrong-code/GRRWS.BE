using GRRWS.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task.Warranty
{
    public class UpdateWarrantyClaimRequest
    {
        public Guid WarrantyClaimId { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string? Resolution { get; set; }
        public string? WarrantyNotes { get; set; }
        public decimal? ClaimAmount { get; set; }
        public Status? ClaimStatus { get; set; }
        public List<IFormFile> DocumentFiles { get; set; } = new();
        public string? DocumentDescription { get; set; }
    }
}
