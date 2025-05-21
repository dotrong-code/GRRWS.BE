
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
                    Id = Guid.Parse("f1a2b3c4-0001-0001-0001-800000000001"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0002-0002-1002-000000000002"), // Juki DDL-8700 Unit 4
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0002-0002-0002-800000000002"),
                    Status = "Completed",
                    RequestId = Guid.Parse("a1f2e3d4-0003-0003-1003-000000000003"), // Juki DDL-8700 Unit 7
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0003-0003-0003-800000000003"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0005-0005-1005-000000000005"), // Brother B957 Unit 1
                    IssueId = Guid.Parse("34343434-3434-3434-3434-343434343434"), // CHI_DUOI_LOI
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0004-0004-0004-800000000004"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0006-0006-1006-000000000006"), // Singer 4452 Unit 1
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0005-0005-0005-800000000005"),
                    Status = "Completed",
                    RequestId = Guid.Parse("a1f2e3d4-0008-0008-1008-000000000008"), // Juki DDL-9000C Unit 2
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0006-0006-0006-800000000006"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"), // Juki DDL-8700 Unit 3
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0007-0007-0007-800000000007"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0011-0011-1011-000000000011"), // Brother B957 Unit 2
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0008-0008-0008-800000000008"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"), // Singer 4452 Unit 3
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // VAI_BI_NHAN
                },
                new RequestIssue
                {
                    Id = Guid.Parse("f1a2b3c4-0009-0009-0009-800000000009"),
                    Status = "Completed",
                    RequestId = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"), // Brother B957 Unit 3
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO

                }
            );
        }
    }

}

