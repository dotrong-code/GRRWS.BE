using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasIndex(s => s.ShiftName)
                   .IsUnique()
                   .HasDatabaseName("IX_Shifts_ShiftName");

            builder.HasData(
                new Shift
                {
                    Id = Guid.Parse("c1d2e3f4-0011-0011-0011-000000000001"),
                    ShiftName = "Ca Sáng",
                    StartTime = TimeSpan.FromHours(8),
                    EndTime = TimeSpan.FromHours(11.5),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Shift
                {
                    Id = Guid.Parse("c1d2e3f4-0022-0022-0022-000000000002"),
                    ShiftName = "Ca Chiều",
                    StartTime = TimeSpan.FromHours(12.5),
                    EndTime = TimeSpan.FromHours(17),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}