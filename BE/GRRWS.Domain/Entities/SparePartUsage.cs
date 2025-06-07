namespace GRRWS.Domain.Entities
{
    public class SparePartUsage : BaseEntity
    {
        public Guid ErrorDetailId { get; set; }
        public Guid SparePartId { get; set; }
        public int QuantityUsed { get; set; }
        public bool IsTakenFromStock { get; set; } // Indicates if the spare part was taken from stock or not

        // Navigation properties
        public ErrorDetail ErrorDetail { get; set; }
        public Sparepart SparePart { get; set; }
    }
}
