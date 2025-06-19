using GRRWS.Domain.Entities;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUserNotificationsAsync(Guid userId, int skip = 0, int take = 50);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task MarkAsReadAsync(Guid notificationId, Guid userId);
        Task AddAsync(Notification notification);
        Task<int> SaveChangesAsync();
    }
}