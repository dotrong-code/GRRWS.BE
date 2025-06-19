
namespace GRRWS.Infrastructure.DTOs.Notification
{
    public class RegisterPushTokenRequest
    {
        public string Token { get; set; }
        public string Platform { get; set; } // "ios" or "android"
    }
}
