
namespace GRRWS.Domain.Entities
{
    public class PushToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Platform { get; set; } // "ios" or "android"
        public bool IsActive { get; set; } = true;
        public DateTime LastUsed { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        public virtual User User { get; set; }
    }
}