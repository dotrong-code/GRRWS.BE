using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(GRRWSContext context, ILogger<NotificationRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(Guid userId, int skip = 0, int take = 50)
        {
            try
            {
                return await _context.Notifications
                    .AsNoTracking()
                    .Where(n => n.ReceiverId == userId && n.Enabled == true)
                    .OrderByDescending(n => n.CreatedDate)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notifications for user {UserId}", userId);
                throw;
            }
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            try
            {
                return await _context.Notifications
                    .AsNoTracking()
                    .CountAsync(n => n.ReceiverId == userId && !n.IsRead && n.Enabled == true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count for user {UserId}", userId);
                throw;
            }
        }

        public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
        {
            try
            {
                var notification = await _context.Notifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId && n.ReceiverId == userId);

                if (notification != null)
                {
                    notification.IsRead = true;
                    notification.ReadAt = DateTime.UtcNow;
                    _context.Notifications.Update(notification);
                    _logger.LogDebug("Notification {NotificationId} marked as read for user {UserId}", notificationId, userId);
                }
                else
                {
                    _logger.LogWarning("Notification {NotificationId} not found for user {UserId}", notificationId, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                throw;
            }
        }

        public async Task AddAsync(Notification notification)
        {
            try
            {
                await _context.Notifications.AddAsync(notification);
                _logger.LogDebug("Notification added for receiver {ReceiverId}", notification.ReceiverId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding notification");
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving notification changes");
                throw;
            }
        }
    }
}