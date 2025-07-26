namespace GRRWS.Domain.Entities
{
    public class ErrorSparepart : BaseEntity
    {
        public Guid ErrorId { get; set; } 
        public Guid SparepartId { get; set; }
        public int? QuantityNeeded { get; set; }

        // Navigation
        public Error Error { get; set; }
        public Sparepart Sparepart { get; set; }
    }
}
