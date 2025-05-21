using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GRRWS.Domain.Entities;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            // Configure index for ErrorCode
            builder.HasIndex(e => e.ErrorCode)
                   .HasDatabaseName("IX_Errors_ErrorCode")
                   .IsUnique();

            // Sample data (10 records)
            builder.HasData(
                new Error
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                    ErrorCode = "ERR001",
                    Name = "Motor Failure",
                    Description = "Motor stops working or runs irregularly.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = true,
                    OccurrenceCount = 5,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222232"),
                    ErrorCode = "ERR002",
                    Name = "Needle Breakage",
                    Description = "Needle breaks during operation.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 8,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333343"),
                    ErrorCode = "ERR003",
                    Name = "Thread Tension Issue",
                    Description = "Incorrect thread tension causing uneven stitches.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 10,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444454"),
                    ErrorCode = "ERR004",
                    Name = "Oil Leak",
                    Description = "Oil leakage from machine components.",
                    EstimatedRepairTime = TimeSpan.FromHours(3),
                    IsCommon = false,
                    OccurrenceCount = 2,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555565"),
                    ErrorCode = "ERR005",
                    Name = "Overheating",
                    Description = "Machine overheats after prolonged use.",
                    EstimatedRepairTime = TimeSpan.FromHours(2.5),
                    IsCommon = true,
                    OccurrenceCount = 6,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666676"),
                    ErrorCode = "ERR006",
                    Name = "Belt Slippage",
                    Description = "Drive belt slips or breaks.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = false,
                    OccurrenceCount = 3,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777787"),
                    ErrorCode = "ERR007",
                    Name = "Electrical Fault",
                    Description = "Electrical issues causing machine failure.",
                    EstimatedRepairTime = TimeSpan.FromHours(3),
                    IsCommon = false,
                    OccurrenceCount = 1,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888898"),
                    ErrorCode = "ERR008",
                    Name = "Bobbin Winding Issue",
                    Description = "Bobbin fails to wind properly.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 7,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999109"),
                    ErrorCode = "ERR009",
                    Name = "Stitch Irregularity",
                    Description = "Machine produces irregular stitches.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 9,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaba"),
                    ErrorCode = "ERR010",
                    Name = "Foot Pedal Failure",
                    Description = "Foot pedal does not respond.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
