using GRRWS.Domain.Entities;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<object>> GetUserNotificationsAsync(
    Guid userId, int userRole, int skip = 0, int take = 50,
    string? search = null, string? type = null, bool? isRead = null,
    DateTime? fromDate = null, DateTime? toDate = null);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task MarkAsReadAsync(Guid notificationId, Guid userId);
        Task AddAsync(Notification notification);
        Task AddNotificationReceiversAsync(List<NotificationReceiver> receivers);
        Task<List<Guid>> GetUserIdsByRoleAsync(int role);
        Task<bool> IsNotificationReadByUserAsync(Guid notificationId, Guid userId);
    }
}