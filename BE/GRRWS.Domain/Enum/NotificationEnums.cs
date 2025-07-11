namespace GRRWS.Domain.Enum
{
    public enum NotificationType
    {
        //  Thông báo chung cho tất cả các vai trò
        General = 0, // Thông báo chung không phân loại

        //  Head of Department (Trưởng bộ phận)
        RequestCreated = 1, // Yêu cầu mới được tạo
        ProgressUpdate = 2, // Cập nhật tiến độ xử lý yêu cầu
        Completed = 3, // Thông báo yêu cầu đã hoàn thành
        FeedbackRequest = 4, // Yêu cầu phản hồi sau khi yêu cầu hoàn thành

        //  System Handler (Điều phối hệ thống)
        StockQuantityChanged = 5, // Thay đổi số lượng tồn kho
        WarrantyStatusUpdate = 6, // Cập nhật trạng thái bảo hành thiết bị
        PartReplacementInitiated = 7, // Bắt đầu quy trình thay thế linh kiện
        SystemSuggestion = 13, // Gợi ý hệ thống về lỗi phổ biến, phụ tùng hoặc nhân sự
        SparePartOutOfStock = 14, // Thông báo linh kiện đã hết
        LowSparePartWarning = 15, // Cảnh báo linh kiện sắp hết

        //  Mechanic (Thợ máy)
        MechanicTaskAssigned = 8, // Thông báo công việc được giao cho thợ máy
        MechanicReportProgress = 16, // Thợ máy báo cáo tiến độ
        MechanicTaskCompleted = 17, // Thợ máy thông báo hoàn thành công việc

        //  Stock Keeper (Quản lý kho)
        StockRequest = 9, // Yêu cầu xuất kho linh kiện

        // Head of Technical (Trưởng bộ phận kỹ thuật)
        WarrantyCollectionReminder = 10, // Nhắc nhở thu hồi thiết bị từ trung tâm bảo hành
        WarrantyDelayUpdate = 11, // Cập nhật tình trạng trễ của thiết bị bảo hành
        EquipmentReturnedIssue = 12, // Thông báo thiết bị trả về từ bảo hành bị lỗi,,,,,
        TaskCompleted = 18, // Thông báo công việc đã hoàn thành
    }


    public enum NotificationChannel
    {
        SignalR = 1,
        Push = 2,
        Both = 3
    }
}
