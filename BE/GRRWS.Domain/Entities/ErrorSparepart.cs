namespace GRRWS.Domain.Entities
{
    public class ErrorSparepart
    {
        public Guid ErrorId { get; set; }
        public Guid SparepartId { get; set; }
        public int? QuantityNeeded { get; set; }

        public Error Error { get; set; }
        public Sparepart Sparepart { get; set; }
    }
}
