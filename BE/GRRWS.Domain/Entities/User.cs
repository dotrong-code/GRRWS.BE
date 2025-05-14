using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? StaffID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool? IsRegister { get; set; }
        public int Role { get; set; }//1 Head of department, 2 Head of technical, 3 Staff, 4 Stock keeper
        public string? ResetPasswordToken { get; set; }

        // Navigation
        public ICollection<Area>? ManagedAreas { get; set; }
        public ICollection<Request>? RequestsSents { get; set; }
        public ICollection<Request>? RequestsReceiveds { get; set; }

    }
}
