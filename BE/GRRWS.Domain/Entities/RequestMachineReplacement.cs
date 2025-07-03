using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class RequestMachineReplacement : BaseEntity
    {
        public string RequestCode { get; set; } // Mã yêu cầu thay thế (unique)
        public DateTime RequestDate { get; set; } = DateTime.UtcNow; // Ngày tạo yêu cầu
        public Guid RequestedById { get; set; } // Người yêu cầu
        public Guid? AssigneeId { get; set; } // Người thực hiện (kỹ thuật viên)
        public Guid OldDeviceId { get; set; } // Device cũ cần thay thế
        public Guid? NewDeviceId { get; set; } // Device mới thay    (nullable nếu chưa chọn)
        public Guid? MachineId { get; set; } // Model (Machine) của Device cần thay thế
        public Guid? ErrorDetailId { get; set; } // In RequestMachineReplacement
        public string? Reason { get; set; } // Lý do thay thế
        public MachineReplacementStatus Status { get; set; } // Trạng thái (Pending, Approved, Rejected, Completed)
        public DateTime? ApprovedDate { get; set; } // Ngày duyệt
        public Guid? ApprovedById { get; set; } // Người duyệt
        public string? Notes { get; set; } // Ghi chú
        public DateTime? CompletedDate { get; set; } // Ngày hoàn thành
        public bool AssigneeConfirm { get; set; } = false; // Thiết bị đã được lấy chưa (true nếu đã lấy, false nếu chưa)
        public bool StokkKeeperConfirm { get; set; } = false; // Xác nhận của thủ kho (true nếu đã xác nhận, false nếu chưa)
        public Guid? TaskId { get; set; } // Optional task liên quan đến yêu cầu này
        // Navigation properties
        public User RequestedBy { get; set; }
        public User? Assignee { get; set; }
        public User? ApprovedBy { get; set; }
        public Device OldDevice { get; set; }
        public Device? NewDevice { get; set; }
        public Machine Machine { get; set; }
        public Tasks? Task { get; set; } // Optional task liên quan đến yêu cầu này
        public ErrorDetail? ErrorDetail { get; set; }
    }
}