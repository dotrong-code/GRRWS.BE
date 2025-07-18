using System;

namespace GRRWS.Infrastructure.DTOs.User.Create
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}