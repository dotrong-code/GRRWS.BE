namespace GRRWS.Domain.Entities
{
    public class ErrorFixStep : BaseEntity
    {
        public Guid ErrorGuidelineId { get; set; }
        public string? StepDescription { get; set; } // Unique
        public int StepOrder { get; set; } // Order of the step in the guideline
        public ErrorGuideline? ErrorGuideline { get; set; } // Navigation property
    }
}
