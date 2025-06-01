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
                // Juki DDL-8700 (Machine 1)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 8,
                    Notes = "Chỉ đứt do sử dụng chỉ kém chất lượng, đã thay chỉ mới."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 15, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Chỉ không đều do căng chỉ chưa đúng, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim gãy khi may vải dày, thay kim kích cỡ lớn hơn."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 2, 10, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Tiếng ồn lớn do bu-lông lỏng, đã siết chặt."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2024, 12, 5, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải kẹt do xơ vải tích tụ, đã vệ sinh máy."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"), // MUI_KHET
                    LastOccurredDate = new DateTime(2024, 11, 15, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mùi khét do động cơ quá nóng, kiểm tra quạt làm mát."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 1, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Chỉ rối do lỗi luồn chỉ, đã luồn lại đúng cách."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    LastOccurredDate = new DateTime(2025, 4, 1, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy rung do đặt trên bề mặt không bằng phẳng, đã điều chỉnh."
                },
                // Juki DDL-9000C (Machine 2)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Đèn báo lỗi sáng, kiểm tra mạch điện tử."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // MAY_CHOP_TAT
                    LastOccurredDate = new DateTime(2025, 4, 12, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Máy chạy không ổn định, kiểm tra nguồn điện và cảm biến."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 3, 25, 13, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi cắt chỉ tự động, đã hiệu chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // DEN_KHONG_SANG
                    LastOccurredDate = new DateTime(2025, 2, 15, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Đèn chiếu sáng hỏng, đã thay bóng mới."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("80808080-8080-8080-8080-808080808080"), // MAY_BI_DO
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy không phản hồi, kiểm tra công tắc nguồn."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 7,
                    Notes = "Chỉ đứt do cảm biến căng chỉ lỗi, đang sửa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), // MAY_BOC_KHOI
                    LastOccurredDate = new DateTime(2024, 11, 20, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Khói nhẹ từ bảng mạch, ngưng sử dụng và kiểm tra."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ không đều, điều chỉnh hệ thống căng chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    LastOccurredDate = new DateTime(2025, 3, 10, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy tự dừng, kiểm tra cảm biến an toàn."
                },
                // Brother B957 (Machine 3)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 5, 8, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Vải kẹt do răng cưa bẩn, đã vệ sinh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 4, 18, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ dưới không đều, điều chỉnh căng chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    LastOccurredDate = new DateTime(2025, 3, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Vải không di chuyển, kiểm tra răng cưa bị kẹt."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHĂN
                    LastOccurredDate = new DateTime(2025, 2, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Vải nhăn do áp suất chân vịt quá cao, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"), // CHI_TREN_LOI
                    LastOccurredDate = new DateTime(2024, 12, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ trên không đều, kiểm tra cơ cấu chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 12, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 7,
                    Notes = "Chỉ đứt do luồn chỉ sai, đã sửa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"), // TIENG_KEN_KET
                    LastOccurredDate = new DateTime(2025, 1, 10, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng kèn kẹt do thiếu dầu bôi trơn, đã bôi dầu."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 4, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi căng chỉ, đã điều chỉnh."
                },
                // Singer 4452 (Machine 4)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 10, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy ngừng hoạt động, phát hiện lỗi dây điện."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"), // RACH_VAI
                    LastOccurredDate = new DateTime(2025, 4, 5, 14, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Vải rách do áp suất chân vịt quá mạnh, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"), // CHAN_VIT_NANG
                    LastOccurredDate = new DateTime(2025, 3, 22, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chân vịt nặng, bôi trơn và điều chỉnh lò xo."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 2, 25, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Kim gãy do may denim dày, thay kim nặng."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    LastOccurredDate = new DateTime(2024, 12, 20, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy rung do bu-lông lỏng, đã siết chặt."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("60606060-6060-6060-6060-606060606060"), // KIM_DAM_VAO_VAI
                    LastOccurredDate = new DateTime(2025, 1, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Kim đâm sâu, để lại lỗ lớn, thay kim phù hợp."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("6f6f6f6f-6f6f-6f6f-6f6f-6f6f6f6f6f6f"), // MAY_GIAT_DIEN
                    LastOccurredDate = new DateTime(2024, 11, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy gây giật điện, kiểm tra dây nối đất."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    IssueId = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"), // TIENG_KHUC_KHAC
                    LastOccurredDate = new DateTime(2025, 4, 10, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng khục khặc do bộ truyền động mòn, cần bảo trì."
                },
                // Juki MO-6714S (Machine 5)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 12, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 9,
                    Notes = "Chỉ đứt do căng chỉ không đều, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 4, 20, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Vải kẹt do tích tụ xơ vải, đã vệ sinh máy."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 3, 18, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Chỉ không đều, kiểm tra và cân chỉnh cơ cấu chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHĂN
                    LastOccurredDate = new DateTime(2025, 2, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Vải nhăn do tốc độ cấp liệu không đồng bộ, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"), // CHI_TREN_LOI
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ trên lỏng, điều chỉnh căng chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    LastOccurredDate = new DateTime(2025, 1, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải không di chuyển, kiểm tra răng cưa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"), // TIENG_KEN_KET
                    LastOccurredDate = new DateTime(2025, 4, 5, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng kèn kẹt do thiếu dầu, đã bôi trơn."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 5, 1, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi luồn chỉ, đã sửa."
                },
                // Brother S-7200C (Machine 6)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    LastOccurredDate = new DateTime(2025, 5, 14, 9, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Đèn báo lỗi do cảm biến cắt chỉ, đang sửa chữa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 4, 25, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng ồn do bu-lông lỏng, đã siết chặt."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 3, 30, 14, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi cắt chỉ tự động, đã hiệu chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // MAY_CHOP_TAT
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy chạy không ổn định, kiểm tra bảng mạch."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // DEN_KHONG_SANG
                    LastOccurredDate = new DateTime(2024, 12, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Đèn chiếu sáng hỏng, đã thay bóng."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Chỉ đứt do cảm biến căng chỉ, đã sửa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"), // MUI_KHET
                    LastOccurredDate = new DateTime(2024, 11, 25, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mùi khét từ động cơ, kiểm tra hệ thống làm mát."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    LastOccurredDate = new DateTime(2025, 4, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy tự dừng, kiểm tra cảm biến an toàn."
                },
                // Singer 4423 (Machine 7)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Kim gãy do may denim dày, thay kim nặng."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    LastOccurredDate = new DateTime(2025, 5, 16, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy không chạy, kiểm tra mô-tơ bị hỏng."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"), // RACH_VAI
                    LastOccurredDate = new DateTime(2025, 4, 8, 13, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Vải rách do kim không phù hợp, đã thay kim."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"), // CHAN_VIT_NANG
                    LastOccurredDate = new DateTime(2025, 2, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chân vịt nặng, bôi trơn lò xo."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy rung do đặt trên sàn không ổn định, đã cố định."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("60606060-6060-6060-6060-606060606060"), // KIM_DAM_VAO_VAI
                    LastOccurredDate = new DateTime(2025, 1, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Kim đâm sâu, làm hỏng vải, thay kim phù hợp."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"), // TIENG_KHUC_KHAC
                    LastOccurredDate = new DateTime(2025, 4, 5, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng khục khặc do bộ truyền động, cần bảo trì."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    IssueId = Guid.Parse("6f6f6f6f-6f6f-6f6f-6f6f-6f6f6f6f6f6f"), // MAY_GIAT_DIEN
                    LastOccurredDate = new DateTime(2024, 11, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Máy gây giật điện, kiểm tra dây nối đất."
                },
                // Juki LH-3568S (Machine 8)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    LastOccurredDate = new DateTime(2025, 4, 10, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Chỉ không đều do hai kim không đồng bộ, đã căn chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    LastOccurredDate = new DateTime(2025, 3, 28, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim gãy khi may jeans, thay kim đôi."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    LastOccurredDate = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Tiếng ồn lớn do bánh răng mòn, cần thay thế."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi luồn chỉ đôi, đã sửa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("7a7a7a7a-7a7a-7a7a-7a7a-7a7a7a7a7a7a"), // KIM_DI_SAI
                    LastOccurredDate = new DateTime(2025, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Kim lệch do căn chỉnh kim đôi sai, đã hiệu chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2024, 12, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ dưới không đều, điều chỉnh cơ cấu chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"), // TIENG_KHUC_KHAC
                    LastOccurredDate = new DateTime(2025, 1, 25, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng khục khặc do bộ truyền động, cần bảo trì."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    LastOccurredDate = new DateTime(2025, 4, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Máy rung do bu-lông lỏng, đã siết chặt."
                },
                // Brother B735 (Machine 9)
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    LastOccurredDate = new DateTime(2025, 5, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Vải kẹt do xơ vải tích tụ, đã vệ sinh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 9, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 7,
                    Notes = "Chỉ đứt do căng chỉ quá chặt, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    LastOccurredDate = new DateTime(2025, 3, 25, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chỉ dưới không đều, kiểm tra cơ cấu chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHĂN
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Vải nhăn do áp suất chân vịt, đã điều chỉnh."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"), // CHI_TREN_LOI
                    LastOccurredDate = new DateTime(2024, 12, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ trên lỏng, điều chỉnh căng chỉ."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    LastOccurredDate = new DateTime(2025, 1, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vải không di chuyển, kiểm tra răng cưa."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"), // TIENG_KEN_KET
                    LastOccurredDate = new DateTime(2025, 4, 5, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Tiếng kèn kẹt do thiếu dầu, đã bôi trơn."
                },
                new MachineIssueHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Chỉ rối do lỗi luồn chỉ, đã sửa."
                }
            );
        }
    }
}