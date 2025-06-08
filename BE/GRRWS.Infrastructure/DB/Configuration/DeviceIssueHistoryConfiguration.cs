using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceIssueHistoryConfiguration : IEntityTypeConfiguration<DeviceIssueHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceIssueHistory> builder)
        {
            builder.HasKey(dih => new { dih.DeviceId, dih.IssueId });

            builder.HasData(
                // DEV001: Juki DDL-8700 Unit 1 (6 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ đứt do căng chỉ không đều, đã điều chỉnh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy khi may vải dày, thay kim phù hợp."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 3, 25, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Đường chỉ lỏng, điều chỉnh bộ căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 2, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn do áp suất chân vịt cao, đã điều chỉnh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    LastOccurredDate = new DateTime(2025, 1, 10, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy rung do bu-lông lỏng, đã siết lại."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"), // TIENG_KEN_KET
                    LastOccurredDate = new DateTime(2024, 12, 20, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Tiếng kèn kẹt từ bánh răng, đã tra dầu."
                },
                // DEV002: Juki DDL-8700 Unit 2 (5 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 12, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt do ống chỉ kẹt, đã thay ống mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 4, 18, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt dưới chân vịt, đã vệ sinh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Máy kêu to do bạc đạn mòn, cần thay."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"), // CHAN_VIT_NANG
                    LastOccurredDate = new DateTime(2025, 2, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chân vịt nặng, đã tra dầu và điều chỉnh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    LastOccurredDate = new DateTime(2024, 11, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ không kéo lên, kiểm tra cơ cấu suốt chỉ."
                },
                // DEV003: Juki DDL-8700 Unit 3 (5 issues, InRepair)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy không chạy, lỗi động cơ, đang sửa chữa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"), // MUI_KHET
                    LastOccurredDate = new DateTime(2025, 4, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mùi khét từ mô-tơ, đang kiểm tra."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // MAY_CHOP_TAT
                    LastOccurredDate = new DateTime(2025, 3, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy chạy ngắt quãng, kiểm tra nguồn điện."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt liên tục, đã thay chỉ mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 1, 10, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy do may vải dày, thay kim mới."
                },
                // DEV004: Juki DDL-8700 Unit 4 (3 issues, newer device)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 10, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy ngừng, lỗi dây điện, đã sửa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ không đều, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Vải nhăn, điều chỉnh áp suất chân vịt."
                },
                // DEV005: Juki DDL-8700 Unit 5 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 14, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt do bụi kẹt, đã vệ sinh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 10, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy, thay kim đúng loại."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    LastOccurredDate = new DateTime(2025, 3, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải không di chuyển, kiểm tra răng cưa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("70707070-7070-7070-7070-707070707070"), // TIENG_LACH_CACH
                    LastOccurredDate = new DateTime(2025, 2, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng lạch cạch, tra dầu cơ cấu kim."
                },
                // DEV007: Juki DDL-8700 Unit 7 (5 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim gãy do sai loại kim, đã thay."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, kiểm tra ống chỉ và thay mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ lỏng, điều chỉnh bộ căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    LastOccurredDate = new DateTime(2025, 2, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy nóng khi may vải dày, kiểm tra mô-tơ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"), // CHAN_VIT_NANG
                    LastOccurredDate = new DateTime(2025, 1, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chân vịt nặng, đã vệ sinh và tra dầu."
                },
                // DEV008: Juki DDL-8700 Unit 8 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Tiếng ồn lớn, kiểm tra bánh răng."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 12, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, thay chỉ chất lượng cao."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 3, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn, điều chỉnh áp suất."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    LastOccurredDate = new DateTime(2025, 2, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ không kéo lên, kiểm tra suốt chỉ."
                },
                // DEV009: Juki DDL-8700 Unit 9 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 13, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, vệ sinh ống chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy, thay kim mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 3, 25, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không đều, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("70707070-7070-7070-7070-707070707070"), // TIENG_LACH_CACH
                    LastOccurredDate = new DateTime(2025, 2, 10, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng lạch cạch, tra dầu."
                },
                // DEV010: Juki DDL-8700 Unit 10 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 14, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, kiểm tra ống chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn, điều chỉnh chân vịt."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 3, 20, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt, vệ sinh răng cưa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // BAN_DAP_KHONG_HOAT_DONG
                    LastOccurredDate = new DateTime(2025, 2, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bàn đạp không phản hồi, kiểm tra dây."
                },
                // DEV011: Juki DDL-9000C Unit 1 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đèn báo lỗi, kiểm tra mạch điện tử."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không đều, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    LastOccurredDate = new DateTime(2025, 3, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy tự dừng, kiểm tra bo mạch."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"), // DUONG_MAY_LOI
                    LastOccurredDate = new DateTime(2025, 2, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đường may lệch, căn chỉnh lại kim."
                },
                // DEV012: Juki DDL-9000C Unit 2 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 5, 10, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ lỏng, điều chỉnh bộ căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, kiểm tra chất lượng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 3, 20, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đèn báo lỗi, kiểm tra cảm biến."
                },
                // DEV013: Juki DDL-9000C Unit 3 (4 issues, InRepair)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 10, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không đều, siết bu-lông căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 5, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy không chạy, lỗi bo mạch, đang sửa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    LastOccurredDate = new DateTime(2025, 3, 25, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy tự dừng, kiểm tra hệ thống điện."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 2, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, thay chỉ mới."
                },
                // DEV014: Juki DDL-9000C Unit 4 (2 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 12, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ đứt, kiểm tra căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ lỏng, điều chỉnh bộ căng chỉ."
                },
                // DEV015: Juki DDL-9000C Unit 5 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 15, 12, 10, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ dưới lỏng, thay cảm biến."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, kiểm tra suốt chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 3, 25, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đèn báo lỗi, kiểm tra bo mạch."
                },
                // DEV016: Juki DDL-9000C Unit 6 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 5, 14, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không đều, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 10, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, kiểm tra chất lượng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn, điều chỉnh áp suất chân vịt."
                },
                // DEV017: Juki DDL-9000C Unit 7 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 13, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, thay chỉ mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ lỏng, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"), // DUONG_MAY_LOI
                    LastOccurredDate = new DateTime(2025, 3, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đường may lệch, căn chỉnh kim."
                },
                // DEV018: Brother B957 Unit 1 (5 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 15, 12, 10, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ dưới không đẹp, thay cảm biến."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ đứt liên tục, kiểm tra căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 4, 20, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối dưới, vệ sinh cơ chế suốt."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_KHONG_LEN_CHI
                    LastOccurredDate = new DateTime(2025, 3, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không kéo lên, kiểm tra cơ cấu."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt, vệ sinh răng cưa."
                },
                // DEV019: Brother B957 Unit 2 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 14, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ dưới lỏng, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, kiểm tra chất lượng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 3, 20, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ rối, vệ sinh cơ chế suốt."
                },
                // DEV020: Brother B735 Unit 1 (4 issues, InRepair)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 5, 20, 15, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn, điều chỉnh áp suất chân vịt."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    IssueId = Guid.Parse("9c9c9c9c-9c9c-9c9c-9c9c-9c9c9c9c9c9c"), // CHI_LUNG_TREN_VAI
                    LastOccurredDate = new DateTime(2025, 5, 10, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối trên vải, kiểm tra căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt liên tục, thay chỉ mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // MAY_CHAY_CHAM
                    LastOccurredDate = new DateTime(2025, 3, 20, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy chạy chậm, kiểm tra mô-tơ."
                },
                // DEV021: Singer 4452 Unit 1 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 5, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim gãy khi may jeans, thay kim."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    LastOccurredDate = new DateTime(2025, 4, 20, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy nóng, kiểm tra mô-tơ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 3, 25, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Máy kêu to, tra dầu bánh răng."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 2, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt, vệ sinh răng cưa."
                },
                // DEV022: Singer 4452 Unit 2 (4 issues, InRepair)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 18, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy không chạy, lỗi mô-tơ, đang sửa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy, thay kim mới."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    LastOccurredDate = new DateTime(2025, 3, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy nóng, kiểm tra hệ thống tản nhiệt."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Máy kêu to, kiểm tra bánh răng."
                },
                // DEV023: Juki MO-6714S Unit 1 (5 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ dưới lỏng, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ đứt liên tục, kiểm tra căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 4, 20, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối dưới, vệ sinh cơ chế suốt."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    LastOccurredDate = new DateTime(2025, 3, 15, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ không kéo lên, kiểm tra cơ cấu."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải nhăn, điều chỉnh áp suất."
                },
                // DEV024: Juki MO-6714S Unit 2 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 14, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ dưới không đều, điều chỉnh."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ đứt, kiểm tra ống dẫn chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối, vệ sinh cơ chế."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ không kéo lên, kiểm tra."
                },
                // DEV025: Brother S-7200C Unit 1 (3 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đèn báo lỗi, kiểm tra cảm biến."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ lỏng, điều chỉnh căng chỉ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 3, 25, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ đứt, kiểm tra chất lượng chỉ."
                },
                // DEV027: Singer 4423 Unit 1 (4 issues)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 5, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim gãy khi may vải dày, thay kim."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    LastOccurredDate = new DateTime(2025, 4, 20, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy nóng, kiểm tra mô-tơ."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 3, 25, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Máy kêu to, tra dầu bánh răng."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 2, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt, vệ sinh răng cưa."
                },
                // DEV028: Singer 4423 Unit 2 (4 issues, InRepair)
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0028-0028-0028-000000000028"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 18, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy không chạy, lỗi mô-tơ, đang sửa."
                },
                new DeviceIssueHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0028-0028-0028-000000000028"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim gãy, thay kim mới."

                }
            );
        }
    }
}