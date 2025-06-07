using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceErrorHistoryConfiguration : IEntityTypeConfiguration<DeviceErrorHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceErrorHistory> builder)
        {
            builder.HasKey(deh => new { deh.DeviceId, deh.ErrorId });

            builder.HasData(

                // DEV001: Juki DDL-8700 Unit 1 (Issues: MAY_NONG, KEU_TO)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a145-0037-0037-0037-000000000037"), // CAM_BIEN_NHIET_DO_LOI
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Cảm biến nhiệt độ không hoạt động, gây máy quá nóng. Đã thay mới."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060"), // MO_DONG_CO_MON
                    LastOccurredDate = new DateTime(2025, 5, 8, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Motor mòn gây tiếng ồn lớn. Đã thay mới."
                },
                // DEV002: Juki DDL-9000C Unit 1 (Issues: DEN_BAO_LOI, MAY_CHOP_TAT)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), // LOI_MACH_DIEU_KHIEN
                    LastOccurredDate = new DateTime(2025, 5, 12, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bo mạch điều khiển lỗi, gây máy chạy không ổn định. Đã sửa chữa."
                },
                // DEV003: Juki DDL-8700 Unit 3 (Issues: MAY_KHONG_CHAY, MUI_KHET)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"), // BO_NGUON_HONG
                    LastOccurredDate = new DateTime(2025, 5, 11, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bộ nguồn hỏng, máy không khởi động. Đã thay mới."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 11, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Motor cháy do ngắn mạch, phát mùi khét. Đã thay mới."
                },
                // DEV004: Singer 4452 Unit 1 (Issues: MAY_RUNG_LAC, KEU_TO)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060"), // MO_DONG_CO_MON
                    LastOccurredDate = new DateTime(2025, 5, 12, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Motor mòn gây rung và tiếng ồn. Đã thay mới."
                },
                // DEV005: Juki MO-6714S Unit 1 (Issues: CHI_KHONG_DEU, DEN_BAO_LOI)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 14, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bộ căng chỉ kẹt, gây chỉ không đều. Đã vệ sinh và sửa chữa."
                },
                // DEV006: Brother S-7200C Unit 1 (Issues: MAY_NONG, DEN_BAO_LOI)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052"), // BO_DIEU_KHIEN_QUA_NHIET
                    LastOccurredDate = new DateTime(2025, 5, 10, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bo mạch điều khiển quá nhiệt. Đã kiểm tra và thay quạt tản nhiệt."
                },
                // DEV007: Singer 4423 Unit 1 (Issues: MAY_CHOP_TAT, KEU_TO)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    ErrorId = Guid.Parse("e1d1a148-0040-0040-0040-000000000040"), // CAP_NGUON_YEU
                    LastOccurredDate = new DateTime(2025, 5, 16, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Cáp nguồn yếu, gây máy chạy không ổn định. Đã thay mới."
                },
                // DEV008: Juki LH-3568S Unit 1 (Issues: KIM_GAY, MAY_RUNG_LAC)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0029-0029-0029-000000000029"),
                    ErrorId = Guid.Parse("e1d1a777-0007-0007-0007-000000000007"), // CAM_BIEN_LECH
                    LastOccurredDate = new DateTime(2025, 5, 12, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Cảm biến lệch do kim gãy liên tục. Đã căn chỉnh."
                },
                // DEV009: Brother B735 Unit 1 (Issues: CHI_KHONG_DEU, DEN_BAO_LOI)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    ErrorId = Guid.Parse("e1d1a165-0057-0057-0057-000000000057"), // CAM_BIEN_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Cảm biến chỉ lỗi, gây chỉ không đều. Đã thay mới."
                },
                // DEV010: Juki DDL-8700 Unit 10 (Issues: BAN_DAP_KHONG_HOAT_DONG)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), // HONG_BAN_DAP
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bàn đạp hỏng, không phản hồi. Đã thay mới."
                },
                // DEV011: Juki DDL-9000C Unit 2 (Issues: MAY_KHONG_CHAY, MUI_KHET)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"), // BO_NGUON_HONG
                    LastOccurredDate = new DateTime(2025, 5, 12, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bộ nguồn hỏng, máy không khởi động và có mùi khét. Đã thay mới."
                },
                // DEV013: Juki MO-6714S Unit 2 (Issues: MAY_NONG, KEU_TO)
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0024-0024-0024-000000000024"),
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060"), // MO_DONG_CO_MON
                    LastOccurredDate = new DateTime(2025, 5, 14, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Motor mòn, gây tiếng ồn và quá nhiệt. Đã thay mới."
                },

                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"), // Juki DDL-8700 Unit 1
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"),  // MO_TROI_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Mỏ trói chỉ bị lỏng, đã điều chỉnh lực siết."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"), // Juki DDL-8700 Unit 3
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"),  // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Động cơ cháy do quá tải, cần thay mô tơ mới."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"), // Juki DDL-8700 Unit 4
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"),  // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 10, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mô tơ bị chập điện, đang chờ phụ tùng thay thế."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"), // Juki DDL-8700 Unit 7
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"),  // KIM_LOI_TAM
                    LastOccurredDate = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Kim lệch tâm, đã căn chỉnh lại trục kim."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"), // Juki DDL-8700 Unit 8
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"),  // BANH_RANG_MON
                    LastOccurredDate = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Bánh răng mòn gây tiếng ồn, đã lên kế hoạch thay mới."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"), // Juki DDL-9000C Unit 1
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"),  // LOI_MACH_DIEU_KHIEN
                    LastOccurredDate = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bo mạch điều khiển lỗi, đang kiểm tra để sửa chữa."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"), // Juki DDL-9000C Unit 3
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"),  // BULONG_LONG
                    LastOccurredDate = new DateTime(2025, 4, 10, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Bulong lỏng ở bộ truyền, đã siết chặt lại."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"), // Brother B957 Unit 1
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"),  // CAM_BIEN_VAI_KHONG_NHAN
                    LastOccurredDate = new DateTime(2025, 5, 15, 12, 10, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Cảm biến vải không nhận, đã thay cảm biến mới."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"), // Singer 4452 Unit 1
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"),  // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 18, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mô tơ bị cháy, đang chờ thay thế phụ tùng."
                },
                new DeviceErrorHistory
                {
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"), // Singer 4452 Unit 3
                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020"),  // ROONG_KHONG_DU_SIEU
                    LastOccurredDate = new DateTime(2025, 5, 20, 15, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Bộ cấp vải hoạt động không đều, đã điều chỉnh lại."

                }
            );
        }
    }
}