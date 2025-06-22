using GRRWS.Application.Common.Result;

namespace GRRWS.Application.Interfaces
{
    public interface IExpoPushService
    {
        Task<Result> SendPushNotificationAsync(string pushToken, string title, string body, object data = null);
        Task<Result> SendPushNotificationsAsync(List<string> pushTokens, string title, string body, object data = null);
        Task<Result> GetUserPushTokensAsync(Guid userId);
        Task<Result> RegisterPushTokenAsync(Guid userId, string token, string platform);
        Task<Result> DeactivateTokenAsync(string token);
        Task<Result> GetTokensByRoleAsync(int role);
    }
}