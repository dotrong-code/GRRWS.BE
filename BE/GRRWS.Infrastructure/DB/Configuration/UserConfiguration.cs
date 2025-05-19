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
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 1 // HOD
                },
                new User
                {
                    Id = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Email = "hot@gmail.com",
                    FullName = "Head of Team",
                    UserName = "Head of Team",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 2 // HOT
                },
                new User
                {
                    Id = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    Email = "staff@gmail.com",
                    FullName = "Staff Member",
                    UserName = "Staff Member",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 3 // Staff
                },
                new User
                {
                    Id = Guid.Parse("54444444-4444-4444-4444-444444444444"),
                    Email = "sk@gmail.com",
                    FullName = "Support Staff",
                    UserName = "Support Staff",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 4 // SK
                },
                new User
                {
                    Id = Guid.Parse("65555555-5555-5555-5555-555555555555"),
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    UserName = "Administrator",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 5 // Admin
                }
                );
        }
    }
}
