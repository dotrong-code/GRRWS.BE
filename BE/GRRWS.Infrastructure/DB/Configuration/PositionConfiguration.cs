using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(
                // Khu Sản Xuất Chính A: Dây Chuyền May A (5 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0001-0001-0001-000000000001"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0002-0002-0002-000000000002"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0003-0003-0003-000000000003"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0004-0004-0004-000000000004"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0005-0005-0005-000000000005"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0031-0031-0031-000000000031"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = null, // Dành sẵn cho thiết bị mới
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0032-0032-0032-000000000032"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Dây Chuyền May A
                    DeviceId = null, // Dành sẵn cho thiết bị mới
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính A: Dây Chuyền May B (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0006-0006-0006-000000000006"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Dây Chuyền May B
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0007-0007-0007-000000000007"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Dây Chuyền May B
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0008-0008-0008-000000000008"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Dây Chuyền May B
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0009-0009-0009-000000000009"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Dây Chuyền May B
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính A: Dây Chuyền May C (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0010-0010-0010-000000000010"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Dây Chuyền May C
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0011-0011-0011-000000000011"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Dây Chuyền May C
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0012-0012-0012-000000000012"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Dây Chuyền May C
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0013-0013-0013-000000000013"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Dây Chuyền May C
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính A: Dây Chuyền May D
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0014-0014-0014-000000000014"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"), // Dây Chuyền May D 
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0015-0015-0015-000000000015"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"), // Dây Chuyền May D
                    DeviceId = Guid.Parse("d1e2f3a4-0006-0006-0006-000000000006"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính A: Khu Chuẩn Bị Vải (3 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0016-0016-0016-000000000016"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Khu Chuẩn Bị Vải
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0017-0017-0017-000000000017"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Khu Chuẩn Bị Vải
                    DeviceId = Guid.Parse("d1e2f3a4-0029-0029-0029-000000000029"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0018-0018-0018-000000000018"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Khu Chuẩn Bị Vải
                    DeviceId = Guid.Parse("d1e2f3a4-0028-0028-0028-000000000028"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính B: Khu May Nặng A (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0019-0019-0019-000000000019"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0020-0020-0020-000000000020"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0021-0011-0021-000000000021"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0022-0022-0022-000000000022"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0033-0033-0033-000000000033"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = null, // Dành sẵn cho thiết bị mới
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0034-0034-0034-000000000034"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Khu May Nặng A
                    DeviceId = null, // Dành sẵn cho thiết bị mới
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính B: Khu May Nặng B (3 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0023-0023-0023-000000000023"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Khu May Nặng B
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0024-0024-0024-000000000024"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Khu May Nặng B
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0025-0025-0025-000000000025"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Khu May Nặng B
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính B: Khu May Nặng C (2 positions, formerly Khu Cắt Gọt và Đóng Gói)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0026-0026-0026-000000000026"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0013-0013-0013-000000000013"), // Khu May Nặng C
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0027-0027-0027-000000000027"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0013-0013-0013-000000000013"), // Khu May Nặng C
                    DeviceId = Guid.Parse("d1e2f3a4-0030-0030-0030-000000000030"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính B: Khu May Nặng D (2 positions, formerly Khu Kiểm Tra 1)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0028-0028-0028-000000000028"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0014-0014-0014-000000000014"), // Khu May Nặng D
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0029-0029-0029-000000000029"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0014-0014-0014-000000000014"), // Khu May Nặng D
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0035-0035-0035-000000000035"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0014-0014-0014-000000000014"), // Khu May Nặng D
                    DeviceId = null, // Dành sẵn cho thiết bị mới
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Khu Sản Xuất Chính B: Khu Sản Xuất Nhỏ (1 position, formerly Khu Kiểm Tra 2)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0030-0030-0030-000000000030"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0015-0015-0015-000000000015"), // Khu Sản Xuất Nhỏ
                    DeviceId = Guid.Parse("d1e2f3a4-0026-0026-0026-000000000026"), 
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}