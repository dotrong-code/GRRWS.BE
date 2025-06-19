using GRRWS.Domain.Entities;

namespace GRRWS.Application.Interfaces
{
    public interface IExpoPushService
    {
        Task<bool> SendPushNotificationAsync(string pushToken, string title, string body, object data = null);
        Task<bool> SendPushNotificationsAsync(List<string> pushTokens, string title, string body, object data = null);
        Task<List<string>> GetUserPushTokensAsync(Guid userId);
        Task RegisterPushTokenAsync(Guid userId, string token, string platform);
        Task DeactivateTokenAsync(string token);
        Task<List<string>> GetTokensByRoleAsync(int role);
    }
}