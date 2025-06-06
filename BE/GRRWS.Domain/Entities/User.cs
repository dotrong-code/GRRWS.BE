using GRRWS.Domain.Enum;

namespace GRRWS.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        public Role Role { get; set; }
        public string? ResetPasswordToken { get; set; }
        public Guid? FeedbackId { get; set; }
        public Guid? AreaId { get; set; }
        // Navigation
        public Feedback? Feedback { get; set; }
        public ICollection<Request>? Requests { get; set; }
        public ICollection<Tasks>? Tasks { get; set; }
        public Area? Area { get; set; }
    }
}
