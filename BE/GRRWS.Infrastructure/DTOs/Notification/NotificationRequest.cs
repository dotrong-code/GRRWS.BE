using GRRWS.Domain.Enum;

namespace GRRWS.Infrastructure.DTOs.Notification
{
     public class NotificationRequest
    {
        public Guid SenderId { get; set; }
        public Guid? ReceiverId { get; set; }
        public int? Role { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationType Type { get; set; } = NotificationType.General;
        public NotificationChannel Channel { get; set; } = NotificationChannel.Both;
        public object? Data { get; set; }
        public bool SaveToDatabase { get; set; } = true;
    }
}
