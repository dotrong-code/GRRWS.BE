using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class RequestIssueConfiguration : IEntityTypeConfiguration<RequestIssue>
    {
        public void Configure(EntityTypeBuilder<RequestIssue> builder)
        {
            // Configure index for RequestId and IssueId
            builder.HasIndex(ri => new { ri.RequestId, ri.IssueId })
                   .HasDatabaseName("IX_RequestIssues_RequestId_IssueId")
                   .IsUnique();

            // Sample data (10 records)
            builder.HasData(
                new RequestIssue
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111222"),
                    Status = "Open",
                    RequestId = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111") // MAY_NONG
                },
                new RequestIssue
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222333"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("22222222-2222-2222-2222-222222222233"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222") // KIM_GAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333444"),
                    Status = "Open",
                    RequestId = Guid.Parse("33333333-3333-3333-3333-333333333344"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333") // MAY_KHONG_CHAY
                },
                new RequestIssue
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444555"),
                    Status = "Closed",
                    RequestId = Guid.Parse("44444444-4444-4444-4444-444444444455"),
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444") // CHAY_DAU
                },
                new RequestIssue
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555666"),
                    Status = "Open",
                    RequestId = Guid.Parse("55555555-5555-5555-5555-555555555566"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555") // KEU_TO
                },
                new RequestIssue
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666777"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("66666666-6666-6666-6666-666666666677"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888") // DUT_CHI
                },
                new RequestIssue
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777888"),
                    Status = "Open",
                    RequestId = Guid.Parse("77777777-7777-7777-7777-777777777788"),
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999") // KHONG_CUON_CHI
                },
                new RequestIssue
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888999"),
                    Status = "InProgress",
                    RequestId = Guid.Parse("88888888-8888-8888-8888-888888888899"),
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") // MAY_CHAY_CHAM
                },
                new RequestIssue
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999101010"),
                    Status = "Open",
                    RequestId = Guid.Parse("99999999-9999-9999-9999-999999991010"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") // CHI_KHONG_DEU
                },
                new RequestIssue
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaabbb"),
                    Status = "Closed",
                    RequestId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaabb"),
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd") // BAN_DAP_KHONG_HOAT_DONG
                }
            );
        }
    }
}
