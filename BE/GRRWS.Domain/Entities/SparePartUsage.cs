namespace GRRWS.Domain.Entities
{
    public class SparePartUsage : BaseEntity
    {
        public Guid? RequestTakeSparePartUsageId { get; set; } // Liên kết với yêu cầu lấy linh kiện
        public Guid SparePartId { get; set; }
        public int QuantityUsed { get; set; }
        public bool IsTakenFromStock { get; set; } // Indicates if the spare part was taken from stock or not       
        // Navigation properties
        
        public Sparepart SparePart { get; set; }
        public RequestTakeSparePartUsage? RequestTakeSparePartUsage { get; set; }
    }
}