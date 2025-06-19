using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class ErrorGuideline : BaseEntity
    {
        public Guid ErrorId { get; set; }
        public string? Title { get; set; } // Unique
        public TimeSpan? EstimatedRepairTime { get; set; }
        public Priority? Priority { get; set; }
        public Error? Error { get; set; }

        public ICollection<ErrorDetail>? ErrorDetails { get; set; }
        public ICollection<ErrorSparepart>? ErrorSpareparts { get; set; }
        public ICollection<ErrorFixStep>? ErrorFixSteps { get; set; } // Navigation property for fix steps

    }
}
