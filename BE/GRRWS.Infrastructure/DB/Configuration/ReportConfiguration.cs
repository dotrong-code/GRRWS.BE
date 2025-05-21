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
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0001-0001-0001-300000000001"),
                    RequestId = Guid.Parse("a1f2e3d4-0002-0002-1002-000000000002"), // Juki DDL-8700 Unit 4
                    Priority = 3, // High
                    Location = "Khu Vực: Main Production Floor, Khu: Sewing Line A, Vị trí: 3",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0002-0002-0002-300000000002"),
                    RequestId = Guid.Parse("a1f2e3d4-0003-0003-1003-000000000003"), // Juki DDL-8700 Unit 7
                    Priority = 2, // Medium
                    Location = "Khu Vực: Main Production Floor, Khu: Sewing Line B, Vị trí: 1",
                    Status = "Completed",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1)
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0003-0003-0003-300000000003"),
                    RequestId = Guid.Parse("a1f2e3d4-0005-0005-1005-000000000005"), // Brother B957 Unit 1
                    Priority = 3, // High
                    Location = "Khu Vực: Finishing Department, Khu: Overlock Section, Vị trí: 1",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0004-0004-0004-300000000004"),
                    RequestId = Guid.Parse("a1f2e3d4-0006-0006-1006-000000000006"), // Singer 4452 Unit 1
                    Priority = 3, // High
                    Location = "Khu Vực: Finishing Department, Khu: Heavy Duty Stitching Zone, Vị trí: 1",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0005-0005-0005-300000000005"),
                    RequestId = Guid.Parse("a1f2e3d4-0008-0008-1008-000000000008"), // Juki DDL-9000C Unit 2
                    Priority = 2, // Medium
                    Location = "Khu Vực: Main Production Floor, Khu: Sewing Line C, Vị trí: 3",
                    Status = "Completed",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2)
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"),
                    RequestId = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"), // Juki DDL-8700 Unit 3
                    Priority = 2, // Medium
                    Location = "Khu Vực: Main Production Floor, Khu: Sewing Line A, Vị trí: 6",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0007-0007-0007-300000000007"),
                    RequestId = Guid.Parse("a1f2e3d4-0011-0011-1011-000000000011"), // Brother B957 Unit 2
                    Priority = 1, // Low
                    Location = "Khu Vực: Finishing Department, Khu: Overlock Section, Vị trí: 2",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"),
                    RequestId = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"), // Singer 4452 Unit 3
                    Priority = 2, // Medium
                    Location = "Khu Vực: Finishing Department, Khu: Heavy Duty Stitching Zone, Vị trí: 3",
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Report
                {
                    Id = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"),
                    RequestId = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"), // Brother B957 Unit 3
                    Priority = 1, // Low
                    Location = "Khu Vực: Finishing Department, Khu: Overlock Section, Vị trí: 3",
                    Status = "Completed",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2)
                }
            );
        }
    }
}