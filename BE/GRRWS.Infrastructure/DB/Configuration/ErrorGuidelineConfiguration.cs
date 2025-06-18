using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorGuidelineConfiguration : IEntityTypeConfiguration<ErrorGuideline>
    {
        public void Configure(EntityTypeBuilder<ErrorGuideline> builder)
        {
            builder.HasIndex(e => e.Title).IsUnique();

            builder.HasData(
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), // Hỏng Bàn Đạp
                    Title = "Hướng dẫn sửa chữa bàn đạp không phản hồi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"), // Dây Curoa Trượt
                    Title = "Xử lý dây curoa trượt do mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.High
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ErrorId = Guid.Parse("e1d1a333-0003-0003-0003-000000000003"), // Máy Chạy Luôn Lượt
                    Title = "Sửa lỗi bo điều khiển máy chạy luôn lượt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.High
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // Cháy Motor
                    Title = "Thay thế motor bị cháy do quá tải",
                    EstimatedRepairTime = TimeSpan.FromMinutes(180),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // Khóa Kim Hỏng
                    Title = "Căn chỉnh cơ chế khóa kim",
                    EstimatedRepairTime = TimeSpan.FromMinutes(40),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006"), // Gioăng Dầu Bị Rò
                    Title = "Xử lý gioăng dầu bị rò",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    ErrorId = Guid.Parse("e1d1a777-0007-0007-0007-000000000007"), // Cảm Biến Lệch
                    Title = "Căn chỉnh cảm biến vị trí bị lệch",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000008"),
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), // Lỗi Mạch Điều Khiển
                    Title = "Sửa chữa mạch điều khiển bị lỗi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.High
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000009"),
                    ErrorId = Guid.Parse("e1d1a999-0009-0009-0009-000000000009"), // Chống Trôi Không Hoạt Động
                    Title = "Sửa chữa cơ chế chống trôi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(50),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000010"),
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), // Chốt Vải Kẹt
                    Title = "Giải quyết tình trạng chốt vải kẹt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    Priority = Priority.Low
                }
            );
        }
    }
}
