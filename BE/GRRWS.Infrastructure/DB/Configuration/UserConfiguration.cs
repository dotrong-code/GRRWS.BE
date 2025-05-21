using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Email = "hod@gmail.com",
                    FullName = "Head of Department",
                    UserName = "Head of Department",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 1 // HOD
                },
                new User
                {
                    Id = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Email = "hot@gmail.com",
                    FullName = "Head of Team",
                    UserName = "Head of Team",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 2 // HOT
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    Email = "staff@gmail.com",
                    FullName = "Staff Member",
                    UserName = "Staff Member",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 3 // Staff
                },
                new User
                {
                    Id = Guid.Parse("54444444-4444-4444-4444-444444444444"),
                    Email = "sk@gmail.com",
                    FullName = "Support Staff",
                    UserName = "Support Staff",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 4 // SK
                },
                new User
                {
                    Id = Guid.Parse("65555555-5555-5555-5555-555555555555"),
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    UserName = "Administrator",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 5 // Admin
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333334"),
                    Email = "staff2@gmail.com",
                    FullName = "Staff Member 2",
                    UserName = "Staff Member 2",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!",
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333335"),
                    Email = "staff3@gmail.com",
                    FullName = "Staff Member 3",
                    UserName = "Staff Member 3",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333336"),
                    Email = "staff4@gmail.com",
                    FullName = "Staff Member 4",
                    UserName = "Staff Member 4",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!",
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333337"),
                    Email = "staff5@gmail.com",
                    FullName = "Staff Member 5",
                    UserName = "Staff Member 5",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333338"),
                    Email = "staff6@gmail.com",
                    FullName = "Staff Member 6",
                    UserName = "Staff Member 6",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333339"),
                    Email = "staff7@gmail.com",
                    FullName = "Staff Member 7",
                    UserName = "Staff Member 7",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!",
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333340"),
                    Email = "staff8@gmail.com",
                    FullName = "Staff Member 8",
                    UserName = "Staff Member 8",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333341"),
                    Email = "staff9@gmail.com",
                    FullName = "Staff Member 9",
                    UserName = "Staff Member 9",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!",
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333342"),
                    Email = "staff10@gmail.com",
                    FullName = "Staff Member 10",
                    UserName = "Staff Member 10",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 3, // Staff
                },
                new User
                {
                    Id = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    Email = "tech2@gmail.com",
                    FullName = "Head of Tech 2",
                    UserName = "Head of Tech 3",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", 
                    Role = 2, 
                },
                new User
                {
                    Id = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    Email = "tech3@gmail.com",
                    FullName = "Head of Tech 3",
                    UserName = "Head of Tech 3",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!",
                    Role = 2, 
                }
                );
        }
    }
}
