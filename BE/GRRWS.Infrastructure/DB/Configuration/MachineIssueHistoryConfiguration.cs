using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class MachineIssueHistoryConfiguration : IEntityTypeConfiguration<MachineIssueHistory>
    {
        public void Configure(EntityTypeBuilder<MachineIssueHistory> builder)
        {
            builder.HasKey(mih => new { mih.MachineId, mih.IssueId });

            builder.HasData(
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"), // Juki DDL-8700 Unit 1
                //    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"),   // DUT_CHI
                //    LastOccurredDate = new DateTime(2025, 4, 15, 10, 30, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 3,
                //    Notes = "Chỉ bị đứt do kẹt ở ống chỉ, đã thay ống chỉ mới."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"), // Juki DDL-8700 Unit 3
                //    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"),   // MAY_KHONG_CHAY
                //    LastOccurredDate = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 2,
                //    Notes = "Máy không chạy do lỗi động cơ, đang chờ sửa chữa."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"), // Juki DDL-8700 Unit 4
                //    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"),   // MAY_KHONG_CHAY
                //    LastOccurredDate = new DateTime(2025, 5, 10, 9, 0, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 1,
                //    Notes = "Máy ngừng hoạt động, kiểm tra phát hiện lỗi dây điện."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"), // Juki DDL-8700 Unit 7
                //    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"),   // KIM_GAY
                //    LastOccurredDate = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 4,
                //    Notes = "Kim gãy do sử dụng sai loại kim, đã thay kim phù hợp."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"), // Juki DDL-8700 Unit 8
                //    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"),   // KEU_TO
                //    LastOccurredDate = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 2,
                //    Notes = "Tiếng ồn lớn do bánh răng mòn, cần thay thế."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0011-0011-0011-000000000011"), // Juki DDL-9000C Unit 1
                //    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),   // DEN_BAO_LOI
                //    LastOccurredDate = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 1,
                //    Notes = "Đèn báo lỗi sáng, kiểm tra mạch điện tử đang được tiến hành."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0013-0013-0013-000000000013"), // Juki DDL-9000C Unit 3
                //    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),   // CHI_KHONG_DEU
                //    LastOccurredDate = new DateTime(2025, 4, 10, 16, 0, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 2,
                //    Notes = "Chỉ không đều do bulong lỏng, đã siết lại."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0015-0015-0015-000000000015"), // Brother B957 Unit 1
                //    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"),   // CHI_DUOI_LOI
                //    LastOccurredDate = new DateTime(2025, 5, 15, 12, 10, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 3,
                //    Notes = "Chỉ dưới không kéo lên, kiểm tra cảm biến và thay mới."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0018-0018-0018-000000000018"), // Singer 4452 Unit 1
                //    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"),   // MAY_KHONG_CHAY
                //    LastOccurredDate = new DateTime(2025, 5, 18, 10, 0, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 1,
                //    Notes = "Máy không chạy, kiểm tra phát hiện lỗi mô tơ."
                //},
                //new MachineIssueHistory
                //{
                //    MachineId = Guid.Parse("a1b2c3d4-0020-0020-0020-000000000020"), // Singer 4452 Unit 3
                //    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),   // VAI_BI_NHAN
                //    LastOccurredDate = new DateTime(2025, 5, 20, 15, 30, 0, DateTimeKind.Utc),
                //    OccurrenceCount = 2,
                //    Notes = "Vải bị nhăn do điều chỉnh áp suất không đúng, đã điều chỉnh lại."
                //}
            );
        }
    }
}