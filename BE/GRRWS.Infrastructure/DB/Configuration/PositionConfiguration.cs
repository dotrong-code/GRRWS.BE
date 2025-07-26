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
                // SXA: Dây Chuyền May A (A01) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0001-0001-0001-000000000001"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0002-0002-0002-000000000002"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0003-0003-0003-000000000003"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0004-0004-0004-000000000004"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0005-0005-0005-000000000005"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0031-0031-0031-000000000031"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0032-0032-0032-000000000032"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0036-0036-0036-000000000036"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXA: Dây Chuyền May B (A02) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0006-0006-0006-000000000006"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0007-0007-0007-000000000007"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0008-0008-0008-000000000008"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0009-0009-0009-000000000009"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0037-0037-0037-000000000037"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0038-0038-0038-000000000038"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0039-0039-0039-000000000039"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0040-0040-0040-000000000040"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXA: Dây Chuyền May C (A03) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0010-0010-0010-000000000010"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0011-0011-0011-000000000011"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0012-0012-0012-000000000012"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0013-0013-0013-000000000013"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0041-0041-0041-000000000041"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0042-0042-0042-000000000042"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0043-0043-0043-000000000043"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0044-0044-0044-000000000044"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXA: Phân xưởng Cắt May (A04) - 4 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0016-0016-0016-000000000016"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0017-0017-0017-000000000017"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0029-0029-0029-000000000029"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0018-0018-0018-000000000018"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0028-0028-0028-000000000028"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0045-0045-0045-000000000045"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXB: Dây Chuyền May Nặng A (B01) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0019-0019-0019-000000000019"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0020-0020-0020-000000000020"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0021-0021-0021-000000000021"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0022-0022-0022-000000000022"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0046-0046-0046-000000000046"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0047-0047-0047-000000000047"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0048-0048-0048-000000000048"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0049-0049-0049-000000000049"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXB: Dây Chuyền May Nặng B (B02) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0023-0023-0023-000000000023"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0024-0024-0024-000000000024"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0025-0025-0025-000000000025"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0050-0050-0050-000000000050"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0006-0006-0006-000000000006"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0051-0051-0051-000000000051"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0052-0052-0052-000000000052"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0053-0053-0053-000000000053"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0054-0054-0054-000000000054"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXB: Dây Chuyền May Nặng C (B03) - 8 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0026-0026-0026-000000000026"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0027-0027-0027-000000000027"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0030-0030-0030-000000000030"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0055-0055-0055-000000000055"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0056-0056-0056-000000000056"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0057-0057-0057-000000000057"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0058-0058-0058-000000000058"),
                    Index = 6,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0059-0059-0059-000000000059"),
                    Index = 7,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0060-0060-0060-000000000060"),
                    Index = 8,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // SXB: Tổ Cắt Gọt và Đóng Gói (B04) - 4 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0061-0061-0061-000000000061"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    DeviceId = Guid.Parse("d1e2f3a4-0026-0026-0026-000000000026"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0062-0062-0062-000000000062"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0063-0063-0063-000000000063"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0064-0064-0064-000000000064"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // KKT: Bộ phận Kiểm Tra 1 (KT1) - 4 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0028-0028-0028-000000000028"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0065-0065-0065-000000000065"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0066-0066-0066-000000000066"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0067-0067-0067-000000000067"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // KKT: Bộ phận Kiểm Tra 2 (KT2) - 4 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0029-0029-0029-000000000029"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0068-0068-0068-000000000068"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0069-0069-0069-000000000069"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0070-0070-0070-000000000070"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000011"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // KKT: Bộ phận Kiểm Tra 3 (KT3) - 4 Positions
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0030-0030-0030-000000000030"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0071-0071-0071-000000000071"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0072-0072-0072-000000000072"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0073-0073-0073-000000000073"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0012-0012-0012-000000000012"),
                    DeviceId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}