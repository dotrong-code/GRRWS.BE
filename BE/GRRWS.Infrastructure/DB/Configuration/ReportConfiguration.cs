using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasData(
                new Report { Id = Guid.Parse("e1f2a3b4-0001-0001-0001-300000000001"), Location = "Xưởng A - Khu 1", RequestId = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009") },
                new Report { Id = Guid.Parse("e1f2a3b4-0002-0002-0002-300000000002"), Location = "Xưởng B - Khu 3", RequestId = Guid.Parse("a1f2e3d4-0010-0010-1010-000000000010") },
                new Report { Id = Guid.Parse("e1f2a3b4-0003-0003-0003-300000000003"), Location = "Xưởng C - Khu 5", RequestId = Guid.Parse("a1f2e3d4-0012-0012-1012-000000000012") },
                new Report { Id = Guid.Parse("e1f2a3b4-0004-0004-0004-300000000004"), Location = "Xưởng A - Khu 2", RequestId = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013") },
                new Report { Id = Guid.Parse("e1f2a3b4-0005-0005-0005-300000000005"), Location = "Xưởng B - Khu 6", RequestId = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015") },
                new Report { Id = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"), Location = "Xưởng C - Khu 4", RequestId = Guid.Parse("a1f2e3d4-0016-0016-1016-000000000016") },
                new Report { Id = Guid.Parse("e1f2a3b4-0007-0007-0007-300000000007"), Location = "Xưởng A - Khu 1", RequestId = Guid.Parse("a1f2e3d4-0018-0018-1018-000000000018") },
                new Report { Id = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"), Location = "Xưởng B - Khu 2", RequestId = Guid.Parse("a1f2e3d4-0020-0020-1020-000000000020") },
                new Report { Id = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"), Location = "Xưởng C - Khu 5", RequestId = Guid.Parse("a1f2e3d4-0022-0022-1022-000000000022") },
                new Report { Id = Guid.Parse("e1f2a3b4-0010-0010-0010-300000000010"), Location = "Xưởng Tổng - Kiểm định cuối" }
            );
        }
    }
}
