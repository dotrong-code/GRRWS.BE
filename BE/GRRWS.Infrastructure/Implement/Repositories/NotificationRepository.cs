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

        public async Task<List<object>> GetUserNotificationsAsync(Guid userId, int userRole, int skip = 0, int take = 50)
        {
            try
            {
                var notifications = await _context.NotificationReceivers
                    .AsNoTracking()
                    .Where(nr => nr.ReceiverId == userId && nr.Notification.Enabled == true)
                    .Include(nr => nr.Notification)
                        .ThenInclude(n => n.Sender)
                    .OrderByDescending(nr => nr.Notification.CreatedDate)
                    .Skip(skip)
                    .Take(take)
                    .Select(nr => new
                    {
                        Id = nr.Notification.Id,
                        Title = nr.Notification.Title,
                        Body = nr.Notification.Body,
                        Type = nr.Notification.Type,
                        Channel = nr.Notification.Channel,
                        Data = nr.Notification.Data,
                        Priority = nr.Notification.Priority,
                        CreatedDate = nr.Notification.CreatedDate,
                        SenderId = nr.Notification.SenderId,
                        SenderName = nr.Notification.Sender.FullName,
                        IsRead = nr.IsRead,
                        ReadAt = nr.ReadAt,
                        NotificationReceiverId = nr.Id
                    })
                    .ToListAsync();

                return notifications.Cast<object>().ToList();
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
                return await _context.NotificationReceivers
                    .AsNoTracking()
                    .CountAsync(nr => nr.ReceiverId == userId && !nr.IsRead && nr.Notification.Enabled == true);
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
                var notificationReceiver = await _context.NotificationReceivers
                    .FirstOrDefaultAsync(nr => nr.NotificationId == notificationId && nr.ReceiverId == userId);

                if (notificationReceiver != null && !notificationReceiver.IsRead)
                {
                    notificationReceiver.IsRead = true;
                    notificationReceiver.ReadAt = DateTime.UtcNow;
                    notificationReceiver.ModifiedDate = DateTime.UtcNow;
                    
                    _context.NotificationReceivers.Update(notificationReceiver);
                    _logger.LogDebug("Notification {NotificationId} marked as read for user {UserId}", notificationId, userId);
                }
                else
                {
                    _logger.LogWarning("Notification {NotificationId} not found or already read for user {UserId}", notificationId, userId);
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
                _logger.LogDebug("Notification added with ID {NotificationId}", notification.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding notification");
                throw;
            }
        }

        public async Task AddNotificationReceiversAsync(List<NotificationReceiver> receivers)
        {
            try
            {
                await _context.NotificationReceivers.AddRangeAsync(receivers);
                _logger.LogDebug("Added {Count} notification receivers", receivers.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding notification receivers");
                throw;
            }
        }

        public async Task<List<Guid>> GetUserIdsByRoleAsync(int role)
        {
            try
            {
                return await _context.Users
                    .AsNoTracking()
                    .Where(u => u.Role == role && !u.IsDeleted)
                    .Select(u => u.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user IDs by role {Role}", role);
                throw;
            }
        }

        public async Task<bool> IsNotificationReadByUserAsync(Guid notificationId, Guid userId)
        {
            try
            {
                var notificationReceiver = await _context.NotificationReceivers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(nr => nr.NotificationId == notificationId && nr.ReceiverId == userId);

                return notificationReceiver?.IsRead ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if notification is read by user");
                return false;
            }
        }
    }
}