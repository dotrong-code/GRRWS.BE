using GRRWS.Domain.Common;

namespace GRRWS.Infrastructure.DTOs.RequestMachineReplacement
{
    public class GetAll
    {
        public string Title { get; set; } // Tiêu đề yêu cầu
        public string Description { get; set; }
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; } = TimeHelper.GetHoChiMinhTime();
        public Guid? AssigneeId { get; set; } // Người thực hiện (kỹ thuật viên)
        public string? AssigneeName { get; set; }
        public Guid OldDeviceId { get; set; } // Device cũ cần thay thế
        public Guid? NewDeviceId { get; set; } // Device mới thay    (nullable nếu chưa chọn)
        public Guid? MachineId { get; set; } // Model (Machine) của Device cần thay thế
        public string Status { get; set; } // Trạng thái (Pending, Approved, Rejected, Completed)
        public string RequestType { get; set; } // Loại yêu cầu (Thay thế, Bảo hành, v.v.)
        public bool AssigneeConfirm { get; set; } = false; // Thiết bị đã được lấy chưa (true nếu đã lấy, false nếu chưa)
        public bool StokkKeeperConfirm { get; set; } = false; // Xác nhận của thủ kho (true nếu đã xác nhận, false nếu chưa)
    }
}
