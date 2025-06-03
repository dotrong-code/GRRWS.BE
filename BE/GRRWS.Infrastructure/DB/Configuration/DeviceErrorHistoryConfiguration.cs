using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceErrorHistoryConfiguration : IEntityTypeConfiguration<DeviceErrorHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceErrorHistory> builder)
        {
            builder.HasKey(deh => new { deh.DeviceId, deh.ErrorId });

            builder.HasData(
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