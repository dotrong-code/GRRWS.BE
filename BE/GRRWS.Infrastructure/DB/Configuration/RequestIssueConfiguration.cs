using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class RequestIssueConfiguration : IEntityTypeConfiguration<RequestIssue>
    {
        public void Configure(EntityTypeBuilder<RequestIssue> builder)
        {
            builder.HasData(
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0006-0006-0006-800000000006"),
                    RequestId = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"), // Juki DDL-8700 Unit 3
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333") // MAY_KHONG_CHAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0008-0008-0008-800000000008"),
                    RequestId = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"), // Singer 4452 Unit 3
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") // VAI_BI_NHAN
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0009-0009-0009-800000000009"),
                    RequestId = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"), // Brother B957 Unit 3
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555") // KEU_TO
                }
            );
        }
    }
}