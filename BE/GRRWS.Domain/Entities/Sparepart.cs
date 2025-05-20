namespace GRRWS.Domain.Entities
{
    public class Sparepart : BaseEntity
    {
        public string SparepartCode { get; set; } = null!; // Mã linh kiện (unique, bắt buộc)
        public string SparepartName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Specification { get; set; } // Thông số kỹ thuật (nếu có)
        public int StockQuantity { get; set; } = 0; // Số lượng hiện có trong kho
        public bool IsAvailable => StockQuantity > 0;
        public string? Unit { get; set; } // Đơn vị tính: cái, bộ, v.v.
        public decimal? UnitPrice { get; set; } // Giá linh kiện

        // Quan hệ
        public ICollection<RepairSparepart> RepairSpareparts { get; set; }
        public ICollection<ErrorSparepart> ErrorSpareparts { get; set; }
    }
}
