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
                // HOD Users
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                    Email = "hod@gmail.com",
                    FullName = "Head of Department",
                    UserName = "Head of Department",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001") // Khu Sản Xuất Chính A
                },
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111112"),
                    Email = "hod2@gmail.com",
                    FullName = "Head of Department 2",
                    UserName = "Head of Department 2",
                    PhoneNumber = "09785628661",
                    PasswordHash = "String123!",
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002") // Khu Sản Xuất Chính B
                },
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111113"),
                    Email = "hod3@gmail.com",
                    FullName = "Head of Department 3",
                    UserName = "Head of Department 3",
                    PhoneNumber = "09785628662",
                    PasswordHash = "String123!",
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003") // Khu Kiểm Tra Chất Lượng
                },
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111114"),
                    Email = "hod4@gmail.com",
                    FullName = "Head of Department 4",
                    UserName = "Head of Department 4",
                    PhoneNumber = "09785628663",
                    PasswordHash = "String123!",
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004") // Khu Cắt Vải
                },
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111115"),
                    Email = "hod5@gmail.com",
                    FullName = "Head of Department 5",
                    UserName = "Head of Department 5",
                    PhoneNumber = "09785628664",
                    PasswordHash = "String123!",
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0005-0005-0005-000000000005") // Khu Thêu
                },
                new User
                {
                    Id = Guid.Parse("21111111-1111-1111-1111-111111111116"),
                    Email = "hod6@gmail.com",
                    FullName = "Head of Department 6",
                    UserName = "Head of Department 6",
                    PhoneNumber = "09785628665",
                    PasswordHash = "String123!",
                    Role = 1, // HOD
                    AreaId = Guid.Parse("b1c2d3e4-0006-0006-0006-000000000006") // Khu Lưu Kho Thiết Bị
                },
                // HOT Users
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
                    Id = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    Email = "tech2@gmail.com",
                    FullName = "Head of Tech 2",
                    UserName = "Head of Tech 2",
                    PhoneNumber = "09785628666",
                    PasswordHash = "String123!",
                    Role = 2,
                },
                new User
                {
                    Id = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    Email = "tech3@gmail.com",
                    FullName = "Head of Tech 3",
                    UserName = "Head of Tech 3",
                    PhoneNumber = "09785628667",
                    PasswordHash = "String123!",
                    Role = 2,
                },
                // Staff Users (Role 3)
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
                // Stock Keeper
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
                // Admin
                new User
                {
                    Id = Guid.Parse("65555555-5555-5555-5555-555555555555"),
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    UserName = "Administrator",
                    PhoneNumber = "09785628660",
                    PasswordHash = "String123!", // Ensure password is hashed in production
                    Role = 5 // Admin
                }
            );
        }
    }
}
