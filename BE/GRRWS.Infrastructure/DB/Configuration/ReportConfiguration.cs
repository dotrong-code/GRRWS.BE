using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasData(
                //new Report
                //{
                //    Id = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"),
                //    RequestId = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"),
                //    Priority = 2, // Medium
                //    Location = "Khu Vực: Khu Sản Xuất Chính A, Khu: Khu Cắt May, Vị trí: 1",
                //    Status = "InProgress",
                //    CreatedDate = DateTime.UtcNow,
                //    ModifiedDate = DateTime.UtcNow
                //},
                //new Report
                //{
                //    Id = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"),
                //    RequestId = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"),
                //    Priority = 2, // Medium
                //    Location = "Khu Vực: Khu Thêu, Khu: Khu May Nặng D, Vị trí: 2",
                //    Status = "InProgress",
                //    CreatedDate = DateTime.UtcNow,
                //    ModifiedDate = DateTime.UtcNow
                //},
                //new Report
                //{
                //    Id = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"),
                //    RequestId = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"),
                //    Priority = 1, // Low
                //    Location = "Khu Vực: Khu Sản Xuất Chính B, Khu: Khu May Nặng A, Vị trí: 3",
                //    Status = "Completed",
                //    CreatedDate = DateTime.UtcNow,
                //    ModifiedDate = DateTime.UtcNow.AddHours(2)
                //}
            );
        }
    }
}