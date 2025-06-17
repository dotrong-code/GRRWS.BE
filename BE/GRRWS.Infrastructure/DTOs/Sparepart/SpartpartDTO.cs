namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class SparepartDto
    {
        public Guid Id { get; set; }
        public string SparepartCode { get; set; }
        public string SparepartName { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public int StockQuantity { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Unit { get; set; }
        public DateTime? ExpectedAvailabilityDate { get; set; }
        public Guid? SupplierId { get; set; }
        public string? Category { get; set; }
        public string? ImgUrl { get; set; }
    }
}
