using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class SparePartUsageConfiguration : IEntityTypeConfiguration<SparePartUsage>
    {
        public void Configure(EntityTypeBuilder<SparePartUsage> builder)
        {
            builder.HasData(

                // ---------------- RequestTakeSparePartUsageId 1 (Guideline 1) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000001"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000002"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000026"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 2 (Guideline 2) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000003"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000004"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000022"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 3 (Guideline 3) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000005"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000003"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000029"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 4 (Guideline 4) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000006"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000004"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000007"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000004"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000019"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 5 (Guideline 5) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000008"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000005"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    QuantityUsed = 5,
                    IsTakenFromStock = true
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000009"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000005"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                    QuantityUsed = 1,
                    IsTakenFromStock = true
                },

                // ---------------- RequestTakeSparePartUsageId 6 (Guideline 6) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000010"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000006"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000011"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000006"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000011"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 7 (Guideline 7) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000012"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000007"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                    QuantityUsed = 2,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 8 (Guideline 8) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000013"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000008"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000015"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000014"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000008"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000017"),
                    QuantityUsed = 4,
                    IsTakenFromStock = false
                },

                // ---------------- RequestTakeSparePartUsageId 9 (Guideline 9) -----------------
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000015"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000009"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                },
                new SparePartUsage
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000016"),
                    RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000009"),
                    SparePartId = Guid.Parse("10000000-0000-0000-0000-000000000012"),
                    QuantityUsed = 1,
                    IsTakenFromStock = false
                }
            );
        }
    }
}
