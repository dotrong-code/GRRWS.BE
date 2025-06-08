using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorDetailConfiguration : IEntityTypeConfiguration<ErrorDetail>
    {
        public void Configure(EntityTypeBuilder<ErrorDetail> builder)
        {
            builder.HasData(

                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000001"), ReportId = Guid.Parse("e1f2a3b4-0001-0001-0001-300000000001"), ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000001") },

                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000002"), ReportId = Guid.Parse("e1f2a3b4-0002-0002-0002-300000000002"), ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000002") },

                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000003"), ReportId = Guid.Parse("e1f2a3b4-0003-0003-0003-300000000003"), ErrorId = Guid.Parse("e1d1a333-0003-0003-0003-000000000003"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000003") },

                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000004"), ReportId = Guid.Parse("e1f2a3b4-0004-0004-0004-300000000004"), ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000004") },

                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000005"), ReportId = Guid.Parse("e1f2a3b4-0005-0005-0005-300000000005"), ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), RequestTakeSparePartUsageId = Guid.Parse("60000000-0000-0000-0000-000000000005") },

                // Các ErrorDetail 6 đến 10 chưa có RequestTakeSparePartUsageId (null)
                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000006"), ReportId = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"), ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000006"), RequestTakeSparePartUsageId = null },
                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000007"), ReportId = Guid.Parse("e1f2a3b4-0007-0007-0007-300000000007"), ErrorId = Guid.Parse("e1d1a777-0007-0007-0007-000000000007"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000007"), RequestTakeSparePartUsageId = null },
                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000008"), ReportId = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"), ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), RequestTakeSparePartUsageId = null },
                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000009"), ReportId = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"), ErrorId = Guid.Parse("e1d1a999-0009-0009-0009-000000000009"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000009"), RequestTakeSparePartUsageId = null },
                new ErrorDetail { Id = Guid.Parse("40000000-0000-0000-0000-000000000010"), ReportId = Guid.Parse("e1f2a3b4-0010-0010-0010-300000000010"), ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), ErrorGuideLineId = Guid.Parse("20000000-0000-0000-0000-000000000010"), RequestTakeSparePartUsageId = null }
            );
        }
    }
}
