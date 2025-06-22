using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class NotificationErrorMessage
    {
        public static Error FieldIsEmpty(string fieldName) =>
            Error.Validation("Notification.FieldEmpty", $"{fieldName} is required.");

        public static Error NotificationSendFailed() =>
            Error.Failure("Notification.SendFailed", "Failed to send notification.");

        public static Error NotificationRetrieveFailed() =>
            Error.Failure("Notification.RetrieveFailed", "Failed to retrieve notifications.");

        public static Error NotificationMarkReadFailed() =>
            Error.Failure("Notification.MarkReadFailed", "Failed to mark notification as read.");

        public static Error NotificationUnreadCountFailed() =>
            Error.Failure("Notification.UnreadCountFailed", "Failed to get unread count.");

        public static Error InvalidPagination() =>
            Error.Validation("Notification.InvalidPagination", "Maximum take limit is 100.");

        public static Error PushTokenRegistrationFailed() =>
            Error.Failure("PushToken.RegistrationFailed", "Failed to register push token.");

        public static Error InvalidPlatform() =>
            Error.Validation("PushToken.InvalidPlatform", "Platform must be 'ios' or 'android'.");

        public static Error InvalidReceiver() =>
            Error.Validation("Notification.InvalidReceiver", "One or more receiver IDs are invalid.");
    }
}