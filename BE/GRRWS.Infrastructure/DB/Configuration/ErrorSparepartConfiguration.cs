//using GRRWS.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace GRRWS.Infrastructure.DB.Configuration
//{
//    public class ErrorSparepartConfiguration : IEntityTypeConfiguration<ErrorSparepart>
//    {
//        public void Configure(EntityTypeBuilder<ErrorSparepart> builder)
//        {
//            builder.HasKey(es => new { es.ErrorId, es.SparepartId });

//            builder.HasData(
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), // HONG_BAN_DAP
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000003"), // Bàn Đạp Máy
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"), // DAYCUROA_TRUOT
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000002"), // Dây Curoa
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000007"), // Mô Tơ Máy May
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000009"), // Trụ Gắn Kim
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a999-0009-0009-0009-000000000009"), // CHONG_TROI_KHONG_HOAT_DONG
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000017"), // Đế Cao Su Chống Rung
//                    QuantityNeeded = 3
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"), // TRUC_CHINH_LAC
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000013"), // Trục Kim Máy May
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a133-0025-0025-0025-000000000025"), // DAY_KHOI_DONG_LOI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000019"), // Dây Điện Động Cơ
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a127-0019-0019-0019-000000000019"), // CAM_BIEN_AP_LUC_LOI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000015"), // Cảm Biến Tốc Độ
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020"), // ROONG_KHONG_DU_SIEU
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000024"), // Bộ Điều Khiển Chân Vịt
//                    QuantityNeeded = 2
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024"), // KIM_CHAM_VAI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000001"), // Kim May Công Nghiệp
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028"), // DAU_BO_NHIEU
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000020"), // Bộ Gương Chắn Dầu
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a137-0029-0029-0029-000000000029"), // QUAT_THONG_GIO_YEU
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000007"), // Mô Tơ Máy May
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // KHOA_KIM_HONG
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000001"), // Kim May Công Nghiệp
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"), // BANH_RANG_MON
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000006"), // Bánh Răng Phụ
//                    QuantityNeeded = 2
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"), // BANH_RANG_MON
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000012"), // Bánh Răng Truyền Động
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"), // TRUC_CHINH_LAC
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000027"), // Đế Máy May
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a127-0019-0019-0019-000000000019"), // CAM_BIEN_AP_LUC_LOI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000028"), // Bộ Điều Khiển Áp Suất
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024"), // KIM_CHAM_VAI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000021"), // Kim May Dày
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"), // MAT_BO_NHO_LUU_THONG_SO
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000029"), // Bộ Điều Khiển Điện Tử
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"), // MAT_BO_NHO_LUU_THONG_SO
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000014"), // Bộ Lưu Thông Số
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027"), // MACH_DEN_LOI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000008"), // Đèn LED Gắn Máy
//                    QuantityNeeded = 2
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027"), // MACH_DEN_LOI
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000030"), // Công Tắc Đèn
//                    QuantityNeeded = 1
//                },
//                new ErrorSparepart
//                {
//                    ErrorId = Guid.Parse("e1d1a131-0023-0023-0023-000000000023"), // CAM_TAY_KHONG_AN_KHOP
//                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000026"), // Trục Quay Bàn Đạp
//                    QuantityNeeded = 1
//                }
//            );
//        }
//    }
//}
