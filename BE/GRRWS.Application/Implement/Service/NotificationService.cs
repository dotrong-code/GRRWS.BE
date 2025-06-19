using GRRWS.Application.Common.Result;
using GRRWS.Application.Interfaces;
using GRRWS.Infrastructure.Hubs;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using GRRWS.Infrastructure.DTOs.Notification;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Common.Message;

namespace GRRWS.Application.Implement.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<RequestHub> _hubContext;
        private readonly IExpoPushService _expoPushService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IHubContext<RequestHub> hubContext,
            IExpoPushService expoPushService,
            IUnitOfWork unitOfWork,
            ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _expoPushService = expoPushService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> SendNotificationAsync(NotificationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Title))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Title"));

                if (string.IsNullOrEmpty(request.Body))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Body"));

                Notification notification = null;

                if (request.SaveToDatabase)
                {
                    notification = await SaveNotificationToDatabase(request);
                }

                if (request.Channel == NotificationChannel.SignalR || request.Channel == NotificationChannel.Both)
                {
                    await SendSignalRNotification(request, notification);
                }

                if (request.Channel == NotificationChannel.Push || request.Channel == NotificationChannel.Both)
                {
                    await SendPushNotification(request);
                }

                _logger.LogInformation("Notification sent successfully: {Title}", request.Title);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification: {Title}", request.Title);
                return Result.Failure(NotificationErrorMessage.NotificationSendFailed());
            }
        }

        public async Task<Result> SendToUserAsync(Guid userId, string title, string body, NotificationType type = NotificationType.General, object data = null)
        {
            var request = new NotificationRequest
            {
                SenderId = Guid.Empty,
                ReceiverId = userId,
                Title = title,
                Body = body,
                Type = type,
                Data = data
            };

            return await SendNotificationAsync(request);
        }

        public async Task<Result> SendToRoleAsync(int role, string title, string body, NotificationType type = NotificationType.General, object data = null)
        {
            var request = new NotificationRequest
            {
                SenderId = Guid.Empty,
                Role = role,
                Title = title,
                Body = body,
                Type = type,
                Data = data
            };

            return await SendNotificationAsync(request);
        }

        public async Task<Result> SendToAllAsync(string title, string body, NotificationType type = NotificationType.General, object data = null)
        {
            var request = new NotificationRequest
            {
                SenderId = Guid.Empty,
                Title = title,
                Body = body,
                Type = type,
                Data = data
            };

            return await SendNotificationAsync(request);
        }

        public async Task<Result> GetUserNotificationsAsync(Guid userId, int skip = 0, int take = 50)
        {
            try
            {
                if (take > 100)
                    return Result.Failure(NotificationErrorMessage.InvalidPagination());

                var notifications = await _unitOfWork.NotificationRepository.GetUserNotificationsAsync(userId, skip, take);
                var unreadCount = await _unitOfWork.NotificationRepository.GetUnreadCountAsync(userId);

                var result = new
                {
                    notifications,
                    unreadCount,
                    hasMore = notifications.Count == take,
                    pagination = new
                    {
                        skip,
                        take,
                        total = notifications.Count + skip
                    }
                };

                return Result.SuccessWithObject(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notifications for user {UserId}", userId);
                return Result.Failure(NotificationErrorMessage.NotificationRetrieveFailed());
            }
        }

        public async Task<Result> MarkAsReadAsync(Guid notificationId, Guid userId)
        {
            try
            {
                await _unitOfWork.NotificationRepository.MarkAsReadAsync(notificationId, userId);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Notification {NotificationId} marked as read by user {UserId}", notificationId, userId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                return Result.Failure(NotificationErrorMessage.NotificationMarkReadFailed());
            }
        }

        public async Task<Result> GetUnreadCountAsync(Guid userId)
        {
            try
            {
                var count = await _unitOfWork.NotificationRepository.GetUnreadCountAsync(userId);
                return Result.SuccessWithObject(new { unreadCount = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread count for user {UserId}", userId);
                return Result.Failure(NotificationErrorMessage.NotificationUnreadCountFailed());
            }
        }

        public async Task<Result> RegisterPushTokenAsync(Guid userId, string token, string platform)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Token"));

                if (string.IsNullOrEmpty(platform))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Platform"));

                var validPlatforms = new[] { "ios", "android" };
                if (!validPlatforms.Contains(platform.ToLower()))
                    return Result.Failure(NotificationErrorMessage.InvalidPlatform());

                await _expoPushService.RegisterPushTokenAsync(userId, token, platform.ToLower());
                _logger.LogInformation("Push token registered successfully for user {UserId}", userId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering push token");
                return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
            }
        }

        private async Task<Notification> SaveNotificationToDatabase(NotificationRequest request)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId ?? Guid.Empty,
                Title = request.Title,
                Body = request.Body,
                Type = request.Type,
                Channel = request.Channel,
                Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null,
                CreatedDate = DateTime.UtcNow,
                Enabled = true
            };

            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
            return notification;
        }

        private async Task SendSignalRNotification(NotificationRequest request, Notification notification)
        {
            var signalRMessage = new
            {
                id = notification?.Id,
                title = request.Title,
                body = request.Body,
                type = request.Type.ToString(),
                data = request.Data,
                timestamp = DateTime.UtcNow
            };

            if (request.ReceiverId.HasValue && request.ReceiverId != Guid.Empty)
            {
                await _hubContext.Clients.Group($"user:{request.ReceiverId}")
                    .SendAsync("NotificationReceived", signalRMessage);
            }
            else if (request.Role.HasValue)
            {
                var roleName = ((Role)request.Role.Value).ToString();
                await _hubContext.Clients.Group($"role:{roleName}")
                    .SendAsync("NotificationReceived", signalRMessage);
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("NotificationReceived", signalRMessage);
            }

            _logger.LogDebug("SignalR notification sent successfully");
        }

        private async Task SendPushNotification(NotificationRequest request)
        {
            if (request.ReceiverId.HasValue && request.ReceiverId != Guid.Empty)
            {
                var pushTokens = await _expoPushService.GetUserPushTokensAsync(request.ReceiverId.Value);
                if (pushTokens.Any())
                {
                    await _expoPushService.SendPushNotificationsAsync(pushTokens, request.Title, request.Body, request.Data);
                }
            }
            else if (request.Role.HasValue)
            {
                var pushTokens = await _expoPushService.GetTokensByRoleAsync(request.Role.Value);
                if (pushTokens.Any())
                {
                    await _expoPushService.SendPushNotificationsAsync(pushTokens, request.Title, request.Body, request.Data);
                }
            }

            _logger.LogDebug("Push notification sent successfully");
        }
    }
}