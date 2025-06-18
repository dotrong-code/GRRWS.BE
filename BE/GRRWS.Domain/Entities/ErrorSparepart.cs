namespace GRRWS.Domain.Entities
{
    public class ErrorSparepart
    {
        public Guid ErrorGuidelineId { get; set; }
        public Guid SparepartId { get; set; }
        public int? QuantityNeeded { get; set; }

        public ErrorGuideline ErrorGuideline { get; set; }
        public Sparepart Sparepart { get; set; }
    }
}
