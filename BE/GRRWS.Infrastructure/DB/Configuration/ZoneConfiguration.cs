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
            builder.HasIndex(a => a.ZoneCode)
                   .HasDatabaseName("IX_Areas_ZoneCode");

            builder.HasData(
                // SXA
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    ZoneName = "Dây Chuyền May A",
                    ZoneCode = "A01",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    ZoneName = "Dây Chuyền May B",
                    ZoneCode = "A02",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    ZoneName = "Dây Chuyền May C",
                    ZoneCode = "A03",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    ZoneName = "Khu Cắt May",
                    ZoneCode = "A04",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"),
                    ZoneName = "Khu Chuẩn Bị Vải",
                    ZoneCode = "A05",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },

                // SXB
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    ZoneName = "Khu May Nặng A",
                    ZoneCode = "B01",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    ZoneName = "Khu May Nặng B",
                    ZoneCode = "B02",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    ZoneName = "Khu Cắt Gọt và Đóng Gói",
                    ZoneCode = "B03",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },

                // KKT
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    ZoneName = "Khu Kiểm Tra 1",
                    ZoneCode = "KT1",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    ZoneName = "Khu Kiểm Tra 2",
                    ZoneCode = "KT2",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },

                // KCV
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    ZoneName = "Khu Cắt Vải Tự Động",
                    ZoneCode = "CV1",
                    AreaId = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0013-0013-0013-000000000013"),
                    ZoneName = "Khu May Nặng C",
                    ZoneCode = "CV2",
                    AreaId = Guid.Parse("b1c2d3e4-0004-0004-0004-000000000004"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },

                // KTV
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0014-0014-0014-000000000014"),
                    ZoneName = "Khu May Nặng D",
                    ZoneCode = "TV1",
                    AreaId = Guid.Parse("b1c2d3e4-0005-0005-0005-000000000005"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },

                // KLK
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0015-0015-0015-000000000015"),
                    ZoneName = "Khu Lưu Trữ Máy May",
                    ZoneCode = "LK1",
                    AreaId = Guid.Parse("b1c2d3e4-0006-0006-0006-000000000006"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );

        }
    }
}