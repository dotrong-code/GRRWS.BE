using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasIndex(a => a.AreaName)
                   .IsUnique()
                   .HasDatabaseName("IX_Areas_AreaName");

            builder.HasData(
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    AreaName = "Khu Sản Xuất Chính A",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"),
                    AreaName = "Khu Sản Xuất Chính B",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"),
                    AreaName = "Khu Kiểm Tra Chất Lượng",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004"),
                    AreaName = "Khu Cắt Vải",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0005-0005-0005-000000000005"),
                    AreaName = "Khu Thêu",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0006-0006-0006-000000000006"),
                    AreaName = "Khu Lưu Kho Thiết Bị",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}