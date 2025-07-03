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

                // Determine recipient user IDs
                var recipientUserIds = new List<Guid>();

                if (request.ReceiverId.HasValue && request.ReceiverId != Guid.Empty)
                {
                    // Validate specific receiver
                    if (!await _unitOfWork.UserRepository.IdExistsAsync(request.ReceiverId.Value))
                    {
                        return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Invalid receiver ID"));
                    }
                    recipientUserIds.Add(request.ReceiverId.Value);
                }
                else if (request.Role.HasValue)
                {
                    // Get all users with the specified role
                    recipientUserIds = await _unitOfWork.NotificationRepository.GetUserIdsByRoleAsync(request.Role.Value);
                    if (!recipientUserIds.Any())
                    {
                        _logger.LogWarning("No users found with role {Role}", request.Role.Value);
                        return Result.Success(); // Not an error, just no recipients
                    }
                }
                else
                {
                    // Send to all users
                    recipientUserIds = await _unitOfWork.UserRepository.GetAllUserIdsAsync();
                    if (!recipientUserIds.Any())
                    {
                        _logger.LogWarning("No users found to send notification to");
                        return Result.Success();
                    }
                }

                // Save notification and create receivers
                var notification = await SaveNotificationToDatabase(request, recipientUserIds);

                // Send via SignalR if enabled
                if (request.Channel == NotificationChannel.SignalR || request.Channel == NotificationChannel.Both)
                {
                    await SendSignalRNotification(request, notification, recipientUserIds);
                }

                // Send via Push Notification if enabled
                if (request.Channel == NotificationChannel.Push || request.Channel == NotificationChannel.Both)
                {
                    await SendPushNotification(request, recipientUserIds);
                }

                _logger.LogInformation("Notification sent successfully to {RecipientCount} users: {Title}",
                    recipientUserIds.Count, request.Title);

                return Result.SuccessWithObject(new
                {
                    NotificationId = notification.Id,
                    RecipientCount = recipientUserIds.Count,
                    Message = "Notification sent successfully"
                });
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
                Data = data,
                Channel = NotificationChannel.Both,
                SaveToDatabase = true
            };

            return await SendNotificationAsync(request);
        }

        public async Task<Result> SendToUsersAsync(List<Guid> userIds, string title, string body, NotificationType type = NotificationType.General, object data = null)
        {
            try
            {
                if (!userIds.Any())
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("User IDs"));

                // Validate all user IDs exist
                foreach (var userId in userIds)
                {
                    if (!await _unitOfWork.UserRepository.IdExistsAsync(userId))
                    {
                        return Result.Failure(NotificationErrorMessage.FieldIsEmpty($"Invalid user ID: {userId}"));
                    }
                }

                var request = new NotificationRequest
                {
                    SenderId = Guid.Empty,
                    Title = title,
                    Body = body,
                    Type = type,
                    Data = data,
                    Channel = NotificationChannel.Both,
                    SaveToDatabase = true
                };

                var notification = await SaveNotificationToDatabase(request, userIds);

                // Send via SignalR
                await SendSignalRNotification(request, notification, userIds);

                // Send via Push Notification
                await SendPushNotification(request, userIds);

                _logger.LogInformation("Notification sent successfully to {RecipientCount} specific users: {Title}",
                    userIds.Count, title);

                return Result.SuccessWithObject(new
                {
                    NotificationId = notification.Id,
                    RecipientCount = userIds.Count,
                    Message = "Notification sent successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification to specific users: {Title}", title);
                return Result.Failure(NotificationErrorMessage.NotificationSendFailed());
            }
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
                Data = data,
                Channel = NotificationChannel.Both,
                SaveToDatabase = true
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
                Data = data,
                Channel = NotificationChannel.Both,
                SaveToDatabase = true
            };

            return await SendNotificationAsync(request);
        }

        public async Task<Result> GetUserNotificationsAsync(
        Guid userId, int skip = 0, int take = 50,
        string? search = null, string? type = null, bool? isRead = null,
        DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                if (take > 100)
                    return Result.Failure(NotificationErrorMessage.InvalidPagination());
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
                var userRole = user.Role;
                var notifications = await _unitOfWork.NotificationRepository.GetUserNotificationsAsync(
                    userId, userRole, skip, take, search, type, isRead, fromDate, toDate);
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

                return Result.SuccessWithObject(new { Message = "Notification marked as read successfully" });
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

                var result = await _expoPushService.RegisterPushTokenAsync(userId, token, platform.ToLower());
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Push token registered successfully for user {UserId}", userId);
                    return Result.SuccessWithObject(new
                    {
                        message = "Push token registered successfully",
                        userId = userId,
                        platform = platform.ToLower(),
                        registeredAt = TimeHelper.GetHoChiMinhTime()
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering push token");
                return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
            }
        }

        private async Task<Notification> SaveNotificationToDatabase(NotificationRequest request, List<Guid> recipientUserIds)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                Title = request.Title,
                Body = request.Body,
                Type = request.Type,
                Channel = request.Channel,
                Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null,
                Priority = request.Priority,
                CreatedDate = TimeHelper.GetHoChiMinhTime(),
                Enabled = true
            };

            await _unitOfWork.NotificationRepository.AddAsync(notification);

            // Create notification receivers for each recipient
            var notificationReceivers = recipientUserIds.Select(userId => new NotificationReceiver
            {
                Id = Guid.NewGuid(),
                NotificationId = notification.Id,
                ReceiverId = userId,
                IsRead = false,
                CreatedDate = TimeHelper.GetHoChiMinhTime()
            }).ToList();

            await _unitOfWork.NotificationRepository.AddNotificationReceiversAsync(notificationReceivers);
            await _unitOfWork.SaveChangesAsync();

            return notification;
        }

        private async Task SendSignalRNotification(NotificationRequest request, Notification notification, List<Guid> recipientUserIds)
        {
            var signalRMessage = new
            {
                id = notification.Id,
                title = request.Title,
                body = request.Body,
                type = request.Type.ToString(),
                data = request.Data,
                timestamp = TimeHelper.GetHoChiMinhTime()
            };

            foreach (var userId in recipientUserIds)
            {
                await _hubContext.Clients.Group($"user:{userId}")
                    .SendAsync("NotificationReceived", signalRMessage);
            }

            // Also send to role groups if applicable
            if (request.Role.HasValue)
            {
                var roleName = ((Role)request.Role.Value).ToString();
                await _hubContext.Clients.Group($"role:{roleName}")
                    .SendAsync("NotificationReceived", signalRMessage);
            }

            _logger.LogDebug("SignalR notification sent successfully to {RecipientCount} users", recipientUserIds.Count);
        }

        private async Task SendPushNotification(NotificationRequest request, List<Guid> recipientUserIds)
        {
            foreach (var userId in recipientUserIds)
            {
                var tokenResult = await _expoPushService.GetUserPushTokensAsync(userId);
                if (tokenResult.IsSuccess && tokenResult.Object is List<string> tokens && tokens.Any())
                {
                    await _expoPushService.SendPushNotificationsAsync(tokens, request.Title, request.Body, request.Data);
                }
            }

            _logger.LogDebug("Push notification sent successfully to {RecipientCount} users", recipientUserIds.Count);
        }
    }
}