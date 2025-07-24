namespace GRRWS.Domain.Enum
{
    public enum Status
    {
        Pending = 0, // Khi mới tạo yêu cầu, chưa có hành động nào
        Approved = 1, // Chưa dùng đến, có thể dùng trong tương lai
        Rejected = 2, // Xử lý yêu cầu tại chỗ, không cần thực hiện thêm hành động nào hoặc từ chối yêu cầu
        InProgress = 3, // Khi yêu cầu đang được xử lý hoặc thực hiện (sau khi tạo report)
        Completed = 4, // Work on the request has been completed
        Cancelled = 5, // Request has been cancelled
        OnHold = 6, // Work on the request is temporarily paused
        Suggested = 7, // Trạng thái task đã được hệ thống gợi ý mechanic (dùng cho khi tạo task)
        WaitingForConfirmation = 8, // Trạng thái task đang được chờ stockkeepr xác nhận (dùng cho khi tạo task)
        Delayed = 9, // Trạng thái yêu cầu bị trì hoãn do chưa có phụ tùng thay thế hoặc do nguyên nhân khác
        WaitingForInstallation = 10, // Trạng thái yêu cầu đang chờ cài đặt thiết bị mới
    }
}
//[EnumDataType(typeof(Status))]