using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class MachineErrorHistoryConfiguration : IEntityTypeConfiguration<MachineErrorHistory>
    {
        public void Configure(EntityTypeBuilder<MachineErrorHistory> builder)
        {
            builder.HasKey(meh => new { meh.MachineId, meh.ErrorId });

            builder.HasData(
                // Juki DDL-8700 (Machine 1)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 7,
                    Notes = "Bu lông lỏng do rung lắc, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"), // DAYCUROA_TRUOT
                    LastOccurredDate = new DateTime(2025, 4, 15, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Dây curoa trượt do mòn, đã thay dây mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"), // KIM_LOI_TAM
                    LastOccurredDate = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim lệch tâm, đã căn chỉnh trục kim."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011"), // VONG_BAC_MON
                    LastOccurredDate = new DateTime(2025, 2, 10, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Vòng bạc mòn gây tiếng ồn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
                    LastOccurredDate = new DateTime(2024, 12, 5, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Khóa kim hỏng, đã thay cơ chế giữ kim."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006"), // GIOANG_DAU_BI_RO
                    LastOccurredDate = new DateTime(2024, 11, 15, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Gioăng dầu rò, đã thay gioăng mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 1, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Răng cưa mòn, gây trượt vải, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    ErrorId = Guid.Parse("e1d1a155-0047-0047-0047-000000000047"), // ONG_DAU_BI_TAT
                    LastOccurredDate = new DateTime(2025, 4, 1, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Ống dầu tắc, đã vệ sinh và bôi trơn."
                },
                // Juki DDL-9000C (Machine 2)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), // LOI_MACH_DIEU_KHIEN
                    LastOccurredDate = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Bo mạch điều khiển lỗi, đã gửi sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"), // CAM_BIEN_VAI_KHONG_NHAN
                    LastOccurredDate = new DateTime(2025, 4, 12, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Cảm biến vải không nhận, đã thay cảm biến mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"), // MAT_BO_NHO_LUU_THONG_SO
                    LastOccurredDate = new DateTime(2025, 3, 25, 13, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mất bộ nhớ cài đặt, đã cài lại thông số."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a145-0037-0037-0037-000000000037"), // CAM_BIEN_NHIET_DO_LOI
                    LastOccurredDate = new DateTime(2025, 2, 15, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Cảm biến nhiệt độ lỗi, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052"), // BO_DIEU_KHIEN_QUA_NHIET
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bo mạch quá nhiệt, kiểm tra quạt tản nhiệt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Bu lông lỏng ở bảng điều khiển, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), // HONG_BAN_DAP
                    LastOccurredDate = new DateTime(2024, 11, 20, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Bàn đạp không phản hồi, đã thay bàn đạp mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Bộ căng chỉ lỗi, đã điều chỉnh độ căng."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"), // BO_NGUON_HONG
                    LastOccurredDate = new DateTime(2025, 3, 10, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bộ nguồn hỏng, đã thay mới."
                },
                // Brother B957 (Machine 3)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"), // MO_TROI_CHI
                    LastOccurredDate = new DateTime(2025, 5, 8, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Mỏ trói chỉ lỏng, đã siết lực."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a140-0032-0032-0032-000000000032"), // SUOT_CHI_KHONG_QUAY
                    LastOccurredDate = new DateTime(2025, 4, 18, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Suốt chỉ không quay, đã vệ sinh và thay suốt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 3, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031"), // ONG_DAN_CHI_HONG
                    LastOccurredDate = new DateTime(2025, 2, 20, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Ống dẫn chỉ mòn, đã thay ống mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), // CHOT_VAI_KET
                    LastOccurredDate = new DateTime(2024, 12, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chốt vải kẹt, đã vệ sinh và bôi trơn."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 5, 12, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Bộ căng chỉ kẹt, đã sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045"), // PHAO_CHI_HONG
                    LastOccurredDate = new DateTime(2025, 1, 10, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Pháo chỉ hỏng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    ErrorId = Guid.Parse("e1d1a162-0054-0054-0054-000000000054"), // RANH_CUA_KHONG_DONG_BO
                    LastOccurredDate = new DateTime(2025, 4, 25, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Răng cưa không đồng bộ, đã căn chỉnh."
                },
                // Singer 4452 (Machine 4)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 10, 9, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mô tơ cháy do may denim dày, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"), // TRUC_CHINH_LAC
                    LastOccurredDate = new DateTime(2025, 4, 5, 14, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Trục chính lệch do tải nặng, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"), // BANH_RANG_MON
                    LastOccurredDate = new DateTime(2025, 3, 22, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Bánh răng mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 2, 25, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, gây trượt vải, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a146-0038-0038-0038-000000000038"), // MO_HOP_SO_KHONG_KHOP
                    LastOccurredDate = new DateTime(2024, 12, 20, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Hộp số lệch, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
                    LastOccurredDate = new DateTime(2025, 1, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Khóa kim hỏng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2024, 11, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Bu lông lỏng ở bộ truyền, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    ErrorId = Guid.Parse("e1d1a144-0036-0036-0036-000000000036"), // TRUC_KIM_GAY
                    LastOccurredDate = new DateTime(2025, 4, 10, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Trục kim gãy, đã thay mới."
                },
                // Juki MO-6714S (Machine 5)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031"), // ONG_DAN_CHI_HONG
                    LastOccurredDate = new DateTime(2025, 5, 12, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Ống dẫn chỉ mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), // CHOT_VAI_KET
                    LastOccurredDate = new DateTime(2025, 4, 20, 10, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chốt vải kẹt, đã vệ sinh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 3, 18, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Bộ căng chỉ lỗi, đã sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045"), // PHAO_CHI_HONG
                    LastOccurredDate = new DateTime(2025, 2, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Pháo chỉ hỏng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a162-0054-0054-0054-000000000054"), // RANH_CUA_KHONG_DONG_BO
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Răng cưa không đồng bộ, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a140-0032-0032-0032-000000000032"), // SUOT_CHI_KHONG_QUAY
                    LastOccurredDate = new DateTime(2025, 1, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Suốt chỉ không quay, đã thay suốt mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 4, 5, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"), // MO_TROI_CHI
                    LastOccurredDate = new DateTime(2025, 5, 1, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Mỏ trói chỉ lỏng, đã siết lực."
                },
                // Brother S-7200C (Machine 6)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), // LOI_MACH_DIEU_KHIEN
                    LastOccurredDate = new DateTime(2025, 5, 14, 9, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Bo mạch điều khiển lỗi, đang sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a145-0037-0037-0037-000000000037"), // CAM_BIEN_NHIET_DO_LOI
                    LastOccurredDate = new DateTime(2025, 4, 25, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Cảm biến nhiệt độ lỗi, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052"), // BO_DIEU_KHIEN_QUA_NHIET
                    LastOccurredDate = new DateTime(2025, 3, 30, 14, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bo mạch quá nhiệt, kiểm tra quạt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"), // CAM_BIEN_VAI_KHONG_NHAN
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Cảm biến vải không nhận, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"), // MAT_BO_NHO_LUU_THONG_SO
                    LastOccurredDate = new DateTime(2024, 12, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mất bộ nhớ, đã cài lại thông số."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Bu lông lỏng ở bảng điều khiển, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2024, 11, 25, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Bộ căng chỉ lỗi, đã sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"), // BO_NGUON_HONG
                    LastOccurredDate = new DateTime(2025, 4, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Bộ nguồn hỏng, đã thay mới."
                },
                // Singer 4423 (Machine 7)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"), // KIM_LOI_TAM
                    LastOccurredDate = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Kim lệch tâm, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    LastOccurredDate = new DateTime(2025, 5, 16, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Mô tơ cháy do tải nặng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a146-0038-0038-0038-000000000038"), // MO_HOP_SO_KHONG_KHOP
                    LastOccurredDate = new DateTime(2025, 4, 8, 13, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Hộp số lệch, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
                    LastOccurredDate = new DateTime(2025, 2, 15, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Khóa kim hỏng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2024, 12, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 6,
                    Notes = "Bu lông lỏng, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 1, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"), // TRUC_CHINH_LAC
                    LastOccurredDate = new DateTime(2025, 4, 5, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Trục chính lệch, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    ErrorId = Guid.Parse("e1d1a144-0036-0036-0036-000000000036"), // TRUC_KIM_GAY
                    LastOccurredDate = new DateTime(2024, 11, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 1,
                    Notes = "Trục kim gãy, đã thay mới."
                },
                // Juki LH-3568S (Machine 8)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a138-0030-0030-0030-000000000030"), // CUA_KIM_LECH
                    LastOccurredDate = new DateTime(2025, 4, 10, 16, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Cửa kim lệch, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"), // KIM_LOI_TAM
                    LastOccurredDate = new DateTime(2025, 3, 28, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim lệch tâm, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024"), // KIM_CHAM_VAI
                    LastOccurredDate = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Kim chạm vải, đã điều chỉnh vị trí kim."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011"), // VONG_BAC_MON
                    LastOccurredDate = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Vòng bạc mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // BULONG_LONG
                    LastOccurredDate = new DateTime(2025, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Bu lông lỏng, đã siết chặt."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2024, 12, 15, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Bộ căng chỉ lỗi, đã sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 1, 25, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
                    LastOccurredDate = new DateTime(2025, 4, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Khóa kim hỏng, đã thay mới."
                },
                // Brother B735 (Machine 9)
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), // CHOT_VAI_KET
                    LastOccurredDate = new DateTime(2025, 5, 20, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Chốt vải kẹt, đã vệ sinh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"), // MO_TROI_CHI
                    LastOccurredDate = new DateTime(2025, 4, 15, 9, 30, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Mỏ trói chỉ lỏng, đã siết lực."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a140-0032-0032-0032-000000000032"), // SUOT_CHI_KHONG_QUAY
                    LastOccurredDate = new DateTime(2025, 3, 25, 15, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Suốt chỉ không quay, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // MO_CANG_CHI_LOI
                    LastOccurredDate = new DateTime(2025, 2, 20, 11, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 5,
                    Notes = "Bộ căng chỉ lỗi, đã sửa chữa."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045"), // PHAO_CHI_HONG
                    LastOccurredDate = new DateTime(2024, 12, 15, 13, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Pháo chỉ hỏng, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // RANH_CUA_MON
                    LastOccurredDate = new DateTime(2025, 1, 10, 14, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 3,
                    Notes = "Răng cưa mòn, đã thay mới."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a162-0054-0054-0054-000000000054"), // RANH_CUA_KHONG_DONG_BO
                    LastOccurredDate = new DateTime(2025, 4, 5, 12, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 2,
                    Notes = "Răng cưa không đồng bộ, đã căn chỉnh."
                },
                new MachineErrorHistory
                {
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031"), // ONG_DAN_CHI_HONG
                    LastOccurredDate = new DateTime(2025, 5, 15, 10, 0, 0, DateTimeKind.Utc),
                    OccurrenceCount = 4,
                    Notes = "Ống dẫn chỉ mòn, đã thay mới."
                }
            );
        }
    }
}