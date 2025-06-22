using GRRWS.Application.Common.Result;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Notification;

namespace GRRWS.Application.Interfaces
{
    public interface INotificationService
    {
        Task<Result> SendNotificationAsync(NotificationRequest request);
        Task<Result> SendToUserAsync(Guid userId, string title, string body, NotificationType type = NotificationType.General, object data = null);
        Task<Result> SendToUsersAsync(List<Guid> userIds, string title, string body, NotificationType type = NotificationType.General, object data = null);
        Task<Result> SendToRoleAsync(int role, string title, string body, NotificationType type = NotificationType.General, object data = null);
        Task<Result> SendToAllAsync(string title, string body, NotificationType type = NotificationType.General, object data = null);
        Task<Result> GetUserNotificationsAsync(
        Guid userId, int skip = 0, int take = 50,
        string? search = null, string? type = null, bool? isRead = null,
        DateTime? fromDate = null, DateTime? toDate = null);
        Task<Result> MarkAsReadAsync(Guid notificationId, Guid userId);
        Task<Result> GetUnreadCountAsync(Guid userId);
        Task<Result> RegisterPushTokenAsync(Guid userId, string token, string platform);
    }
}