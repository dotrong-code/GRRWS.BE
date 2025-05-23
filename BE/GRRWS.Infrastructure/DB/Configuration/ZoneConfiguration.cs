using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasData(
                // Zones for Khu Sản Xuất Chính (6 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    ZoneName = "Dây Chuyền May A",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    ZoneName = "Dây Chuyền May B",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    ZoneName = "Dây Chuyền May C",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    ZoneName = "Khu Cắt May",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"),
                    ZoneName = "Khu Chuẩn Bị Vải",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"),
                    ZoneName = "Dây Chuyền May D",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Khu Sản Xuất Chính
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    ZoneName = "Khu May Nặng A",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    ZoneName = "Khu May Nặng B",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    ZoneName = "Khu Cắt Gọt và Đóng Gói",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zones for Khu Kiểm Tra Chất Lượng (2 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    ZoneName = "Khu Kiểm Tra 1",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"), // Khu Kiểm Tra Chất Lượng
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    ZoneName = "Khu Kiểm Tra 2",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"), // Khu Kiểm Tra Chất Lượng
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zones for Khu Cắt Vải (2 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    ZoneName = "Khu Cắt Vải Tự Động",
                    AreaId = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004"), // Khu Cắt Vải
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0013-0013-0013-000000000013"),
                    ZoneName = "Khu May Nặng C",
                    AreaId = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zone for Khu Thêu (1 zone)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0014-0014-0014-000000000014"),
                    ZoneName = "Khu May Nặng D",
                    AreaId = Guid.Parse("b1c2d3e4-0005-0005-0005-000000000005"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zone for Khu Lưu Kho Thiết Bị (1 zone)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0015-0015-0015-000000000015"),
                    ZoneName = "Khu Lưu Trữ Máy May",
                    AreaId = Guid.Parse("b1c2d3e4-0006-0006-0006-000000000006"), // Khu Lưu Kho Thiết Bị
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}