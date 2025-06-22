using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GRRWS.Application.Interfaces;
using System.Security.Claims;
using GRRWS.Infrastructure.DTOs.Notification;
using GRRWS.Application.Common.Result;
using GRRWS.Domain.Enum;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(
            INotificationService notificationService,
            ILogger<NotificationsController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpPost("register-push-token")]
        public async Task<IResult> RegisterPushToken([FromBody] RegisterPushTokenRequest request)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.RegisterPushTokenAsync(userId, request.Token, request.Platform);
            return result.IsSuccess
                ? result.ToSuccessDetails("Push token registered successfully")
                : result.ToProblemDetails();
        }

        [HttpGet]
        public async Task<IResult> GetNotifications(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        [FromQuery] string? search = null,
        [FromQuery] string? type = null,
        [FromQuery] bool? isRead = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.GetUserNotificationsAsync(
                userId, skip, take, search, type, isRead, fromDate, toDate);
            return result.IsSuccess
                ? result.ToSuccessDetails("Successfully retrieved notifications")
                : result.ToProblemDetails();
        }

        [HttpPut("{notificationId}/mark-read")]
        public async Task<IResult> MarkAsRead(Guid notificationId)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.MarkAsReadAsync(notificationId, userId);
            return result.IsSuccess
                ? result.ToSuccessDetails("Notification marked as read")
                : result.ToProblemDetails();
        }

        [HttpGet("unread-count")]
        public async Task<IResult> GetUnreadCount()
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.GetUnreadCountAsync(userId);
            return result.IsSuccess
                ? result.ToSuccessDetails("Successfully retrieved unread count")
                : result.ToProblemDetails();
        }

        [HttpPost("test-send")]
        [Authorize(Roles = "HOD")]
        public async Task<IResult> TestSendNotification([FromBody] NotificationRequest request)
        {
            var currentUserId = GetCurrentUserId();
            request.SenderId = currentUserId;
            var result = await _notificationService.SendNotificationAsync(request);
            return result.IsSuccess
                ? result.ToSuccessDetails("Test notification sent successfully")
                : result.ToProblemDetails();
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User ID not found in token");
            return userId;
        }
    }
}