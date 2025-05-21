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
                // Zones for Main Production Floor (5 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0001-0001-0001-000000000001"),
                    ZoneName = "Sewing Line A",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Main Production Floor
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0002-0002-0002-000000000002"),
                    ZoneName = "Sewing Line B",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Main Production Floor
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0003-0003-0003-000000000003"),
                    ZoneName = "Sewing Line C",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Main Production Floor
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0004-0004-0004-000000000004"),
                    ZoneName = "Cutting Section",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Main Production Floor
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0005-0005-0005-000000000005"),
                    ZoneName = "Fabric Preparation Zone",
                    AreaId = Guid.Parse("b1c2d3e4-0001-0001-0001-000000000001"), // Main Production Floor
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zones for Finishing Department (3 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0006-0006-0006-000000000006"),
                    ZoneName = "Overlock Section",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), // Finishing Department
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0007-0007-0007-000000000007"),
                    ZoneName = "Heavy Duty Stitching Zone",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), // Finishing Department
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0008-0008-0008-000000000008"),
                    ZoneName = "Trimming and Packing Zone",
                    AreaId = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"), // Finishing Department
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Zones for Quality Control Area (2 zones)
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0009-0009-0009-000000000009"),
                    ZoneName = "Inspection Zone 1",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"), // Quality Control Area
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Zone
                {
                    Id = Guid.Parse("c1d2e3f4-0010-0010-0010-000000000010"),
                    ZoneName = "Inspection Zone 2",
                    AreaId = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"), // Quality Control Area
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}