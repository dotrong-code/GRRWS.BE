namespace GRRWS.Domain.Enum
{
    public enum NotificationType
    {
        General = 0,
        Request = 1,
        Approval = 2,
        Reminder = 3,
        Alert = 4
    }

    public enum NotificationChannel
    {
        SignalR = 1,
        Push = 2,
        Both = 3
    }
}
