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
            // Seed data with 30 position records
            builder.HasData(
                // Main Production Floor: Sewing Line A (5 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0001-0001-0001-000000000001"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Sewing Line A
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"), // Juki DDL-8700 Unit 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0002-0002-0002-000000000002"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Sewing Line A
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"), // Juki DDL-8700 Unit 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0003-0003-0003-000000000003"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Sewing Line A
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"), // Juki DDL-8700 Unit 4
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0004-0004-0004-000000000004"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Sewing Line A
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"), // Juki DDL-8700 Unit 5
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0005-0005-0005-000000000005"),
                    Index = 5,
                    ZoneId = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"), // Sewing Line A
                    DeviceId = null, // Empty position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Main Production Floor: Sewing Line B (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0006-0006-0006-000000000006"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Sewing Line B
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"), // Juki DDL-8700 Unit 7
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0007-0007-0007-000000000007"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Sewing Line B
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"), // Juki DDL-8700 Unit 8
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0008-0008-0008-000000000008"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Sewing Line B
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"), // Juki DDL-9000C Unit 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0009-0009-0009-000000000009"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"), // Sewing Line B
                    DeviceId = null, // Empty position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Main Production Floor: Sewing Line C (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0010-0010-0010-000000000010"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Sewing Line C
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"), // Juki DDL-8700 Unit 9
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0011-0011-0011-000000000011"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Sewing Line C
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"), // Juki DDL-8700 Unit 10
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0012-0012-0012-000000000012"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Sewing Line C
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"), // Juki DDL-9000C Unit 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0013-0013-0013-000000000013"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"), // Sewing Line C
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"), // Juki DDL-9000C Unit 4
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Main Production Floor: Cutting Section (2 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0014-0014-0014-000000000014"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"), // Cutting Section
                    DeviceId = null, // Manual cutting station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0015-0015-0015-000000000015"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"), // Cutting Section
                    DeviceId = null, // Manual cutting station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Main Production Floor: Fabric Preparation Zone (3 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0016-0016-0016-000000000016"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Fabric Preparation Zone
                    DeviceId = null, // Preparation station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0017-0017-0017-000000000017"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Fabric Preparation Zone
                    DeviceId = null, // Preparation station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0018-0018-0018-000000000018"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"), // Fabric Preparation Zone
                    DeviceId = null, // Preparation station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Finishing Department: Overlock Section (4 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0019-0019-0019-000000000019"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Overlock Section
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"), // Brother B957 Unit 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0020-0020-0020-000000000020"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Overlock Section
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"), // Brother B957 Unit 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0021-0021-0021-000000000021"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Overlock Section
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"), // Brother B957 Unit 3
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0022-0022-0022-000000000022"),
                    Index = 4,
                    ZoneId = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"), // Overlock Section
                    DeviceId = null, // Empty position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Finishing Department: Heavy Duty Stitching Zone (3 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0023-0023-0023-000000000023"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Heavy Duty Stitching Zone
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"), // Singer 4452 Unit 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0024-0024-0024-000000000024"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Heavy Duty Stitching Zone
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"), // Singer 4452 Unit 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0025-0025-0025-000000000025"),
                    Index = 3,
                    ZoneId = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"), // Heavy Duty Stitching Zone
                    DeviceId = null, // Empty position (Singer 4452 Unit 3 is InRepair)
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Finishing Department: Trimming and Packing Zone (2 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0026-0026-0026-000000000026"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"), // Trimming and Packing Zone
                    DeviceId = null, // Manual trimming station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0027-0027-0027-000000000027"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"), // Trimming and Packing Zone
                    DeviceId = null, // Manual packing station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Quality Control Area: Inspection Zone 1 (2 positions)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0028-0028-0028-000000000028"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"), // Inspection Zone 1
                    DeviceId = null, // Inspection station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0029-0029-0029-000000000029"),
                    Index = 2,
                    ZoneId = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"), // Inspection Zone 1
                    DeviceId = null, // Inspection station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Quality Control Area: Inspection Zone 2 (1 position)
                new Position
                {
                    Id = Guid.Parse("f1e2d3c4-0030-0030-0030-000000000030"),
                    Index = 1,
                    ZoneId = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"), // Inspection Zone 2
                    DeviceId = null, // Inspection station
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}