namespace GRRWS.Domain.Entities
{
    public class ErrorGuidelineSparePart : BaseEntity
    {
        public Guid ErrorGuidelineId { get; set; } // Foreign key to ErrorGuideline
        public Guid SparePartId { get; set; } // Foreign key to SparePart
        public int Quantity { get; set; } // Quantity of the spare part needed
        public ErrorGuideline? ErrorGuideline { get; set; } // Navigation property to ErrorGuideline
        public Sparepart? Sparepart { get; set; } // Navigation property to SparePart
    }

}
