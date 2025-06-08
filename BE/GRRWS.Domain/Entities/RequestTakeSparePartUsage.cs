using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class RequestTakeSparePartUsage : BaseEntity
    {
        public string RequestCode { get; set; } = null!; // Mã yêu cầu (unique, tự động hoặc nhập tay)
        public DateTime RequestDate { get; set; } = DateTime.UtcNow; // Ngày tạo yêu cầu
        public Guid? RequestedById { get; set; } // ID của người yêu cầu (liên kết với User hoặc Employee)
        public SparePartRequestStatus Status { get; set; } = SparePartRequestStatus.Insufficient; // Trạng thái: Chưa xác nhận, Xác nhận, Chưa đủ, Đã giao, Đã hủy
        public DateTime? ConfirmedDate { get; set; } // Ngày xác nhận (nếu có)
        public Guid? ConfirmedById { get; set; } // ID của người xác nhận (Stock Keeper)
        public string? Notes { get; set; } // Ghi chú (ví dụ: lý do chưa đủ linh kiện)

        // Navigation properties

        public User? RequestedBy { get; set; } // Quan hệ với người yêu cầu
        public User? ConfirmedBy { get; set; } // Quan hệ với người xác nhận


        public ICollection<SparePartUsage> SparePartUsages { get; set; } = new List<SparePartUsage>();
        public ErrorDetail ErrorDetail { get; set; } // Navigation property 1-1 với ErrorDetail
    }
}