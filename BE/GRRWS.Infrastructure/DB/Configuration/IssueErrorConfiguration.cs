using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class IssueErrorConfiguration : IEntityTypeConfiguration<IssueError>
    {
        public void Configure(EntityTypeBuilder<IssueError> builder)
        {
            // Configure composite key
            builder.HasKey(ie => new { ie.IssueId, ie.ErrorId });

            // Sample data (10 records)
            builder.HasData(
                new IssueError
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    ErrorId = Guid.Parse("55555555-5555-5555-5555-555555555565")  // Overheating
                },
                new IssueError
                {
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    ErrorId = Guid.Parse("22222222-2222-2222-2222-222222222232")  // Needle Breakage
                },
                new IssueError
                {
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    ErrorId = Guid.Parse("77777777-7777-7777-7777-777777777787")  // Electrical Fault
                },
                new IssueError
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // CHAY_DAU
                    ErrorId = Guid.Parse("44444444-4444-4444-4444-444444444454")  // Oil Leak
                },
                new IssueError
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    ErrorId = Guid.Parse("66666666-6666-6666-6666-666666666676")  // Belt Slippage
                },
                new IssueError
                {
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    ErrorId = Guid.Parse("33333333-3333-3333-3333-333333333343")  // Thread Tension Issue
                },
                new IssueError
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // KHONG_CUON_CHI
                    ErrorId = Guid.Parse("88888888-8888-8888-8888-888888888898")  // Bobbin Winding Issue
                },
                new IssueError
                {
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // MAY_CHAY_CHAM
                    ErrorId = Guid.Parse("11111111-1111-1111-1111-111111111121")  // Motor Failure
                },
                new IssueError
                {
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    ErrorId = Guid.Parse("99999999-9999-9999-9999-999999999109")  // Stitch Irregularity
                },
                new IssueError
                {
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // BAN_DAP_KHONG_HOAT_DONG
                    ErrorId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaba")  // Foot Pedal Failure
                }
            );
        }
    }
}
