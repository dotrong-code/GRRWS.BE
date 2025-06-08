using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorFixProgressConfiguration : IEntityTypeConfiguration<ErrorFixProgress>
    {
        public void Configure(EntityTypeBuilder<ErrorFixProgress> builder)
        {
            builder.HasData(
                // ----------------- ErrorDetail 1 -------------------
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000001"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000001"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000001"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000002"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000001"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000002"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000003"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000001"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000003"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000004"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000001"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000004"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000005"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000001"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000005"), IsCompleted = false },

                // ----------------- ErrorDetail 2 -------------------
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000006"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000002"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000006"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000007"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000002"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000007"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000008"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000002"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000008"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000009"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000002"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000009"), IsCompleted = false },

                // ----------------- ErrorDetail 3 -------------------
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000010"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000003"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000010"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000011"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000003"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000011"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000012"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000003"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000012"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000013"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000003"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000013"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000014"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000003"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000014"), IsCompleted = false },

                // ----------------- ErrorDetail 4 -------------------
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000015"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000004"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000015"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000016"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000004"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000016"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000017"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000004"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000017"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000018"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000004"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000018"), IsCompleted = false },

                // ----------------- ErrorDetail 5 -------------------
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000019"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000005"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000019"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000020"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000005"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000020"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000021"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000005"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000021"), IsCompleted = false },
                new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000022"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000005"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000022"), IsCompleted = false },

                // ----------------- ErrorDetail 6 -------------------
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000023"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000006"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000023"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000024"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000006"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000024"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000025"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000006"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000025"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000026"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000006"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000026"), IsCompleted = false },

// ----------------- ErrorDetail 7 -------------------
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000027"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000007"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000027"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000028"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000007"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000028"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000029"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000007"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000029"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000030"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000007"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000030"), IsCompleted = false },

// ----------------- ErrorDetail 8 -------------------
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000031"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000008"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000031"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000032"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000008"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000032"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000033"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000008"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000033"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000034"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000008"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000034"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000035"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000008"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000035"), IsCompleted = false },

// ----------------- ErrorDetail 9 -------------------
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000036"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000009"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000036"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000037"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000009"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000037"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000038"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000009"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000038"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000039"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000009"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000039"), IsCompleted = false },

// ----------------- ErrorDetail 10 -------------------
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000040"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000010"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000040"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000041"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000010"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000041"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000042"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000010"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000042"), IsCompleted = false },
new ErrorFixProgress { Id = Guid.Parse("50000000-0000-0000-0000-000000000043"), ErrorDetailId = Guid.Parse("40000000-0000-0000-0000-000000000010"), ErrorFixStepId = Guid.Parse("30000000-0000-0000-0000-000000000043"), IsCompleted = false }

            );
        }
    }
}
