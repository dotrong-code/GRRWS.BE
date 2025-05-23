using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                    AreaName = "Main Production Floor",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0002-0002-0002-000000000002"),
                    AreaName = "Finishing Department",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Area
                {
                    Id = Guid.Parse("b1c2d3e4-0003-0003-0003-000000000003"),
                    AreaName = "Quality Control Area",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}

