using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {

            builder.HasIndex(e => e.ErrorCode)
                   .HasDatabaseName("IX_Errors_ErrorCode");

            builder.HasData(
                new Error
                {
                    Id = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"),
                    ErrorCode = "HONG_BAN_DAP",
                    Name = "Hỏng Bàn Đạp",
                    Description = "Bàn đạp không phản hồi hoặc mất tín hiệu.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 20,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"),
                    ErrorCode = "DAYCUROA_TRUOT",
                    Name = "Dây Curoa Trượt",
                    Description = "Dây curoa lỏng hoặc mòn, gây mất chuyển động.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 15,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a333-0003-0003-0003-000000000003"),
                    ErrorCode = "MAY_CHAY_LUON_LUOT",
                    Name = "Máy Chạy Luôn Lượt",
                    Description = "Bo điều khiển bị lỗi, không kiểm soát được tốc độ.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,

                    OccurrenceCount = 5,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"),
                    ErrorCode = "CHAY_MOTOR",
                    Name = "Cháy Motor",
                    Description = "Động cơ chính bị cháy do quá tải hoặc ngắn mạch.",
                    EstimatedRepairTime = TimeSpan.FromHours(3),
                    IsCommon = false,
                    OccurrenceCount = 3,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"),
                    ErrorCode = "KHOA_KIM_HONG",
                    Name = "Khóa Kim Hỏng",
                    Description = "Cơ chế giữ kim bị lệch hoặc gãy.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(40),
                    IsCommon = true,
                    OccurrenceCount = 18,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1a666-0006-0006-0006-000000000006"),
                    ErrorCode = "GIOANG_DAU_BI_RO",
                    Name = "Gioăng Dầu Bị Rò",
                    Description = "Dầu rò ra ngoài do gioăng hoặc phớt bị mòn.",

                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 10,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a777-0007-0007-0007-000000000007"),
                    ErrorCode = "CAM_BIEN_LECH",
                    Name = "Cảm Biến Lệch",
                    Description = "Cảm biến vị trí bị lệch dẫn đến dừng máy không đúng lúc.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"),
                    ErrorCode = "LOI_MACH_DIEU_KHIEN",
                    Name = "Lỗi Mạch Điều Khiển",
                    Description = "Bo mạch điều khiển bị chập, không phản hồi.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 4,

                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1a999-0009-0009-0009-000000000009"),
                    ErrorCode = "CHONG_TROI_KHONG_HOAT_DONG",
                    Name = "Chống Trôi Không Hoạt Động",
                    Description = "Cơ chế chống trôi vải không ăn khớp.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(50),
                    IsCommon = true,
                    OccurrenceCount = 12,

                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"),
                    ErrorCode = "CHOT_VAI_KET",
                    Name = "Chốt Vải Kẹt",
                    Description = "Chốt vải bị kẹt, gây gián đoạn chu trình may.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    IsCommon = true,
                    OccurrenceCount = 22,
                    Severity = "Low",

                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1addd-0011-0011-0011-000000000011"),
                    ErrorCode = "VONG_BAC_MON",
                    Name = "Vòng Bạc Mòn",
                    Description = "Vòng bạc trục bị mòn dẫn đến rung lắc hoặc tiếng ồn lớn.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 14,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1aeee-0012-0012-0012-000000000012"),
                    ErrorCode = "DAO_CAT_KHONG_SAC",
                    Name = "Dao Cắt Không Sắc",
                    Description = "Dao cắt không bén, gây xơ vải hoặc rách mép.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    IsCommon = true,
                    OccurrenceCount = 25,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"),
                    ErrorCode = "CAM_BIEN_VAI_KHONG_NHAN",
                    Name = "Cảm Biến Vải Không Nhận",
                    Description = "Cảm biến không phát hiện được vải khi đưa vào.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"),
                    ErrorCode = "KIM_LOI_TAM",
                    Name = "Kim Lỗi Tâm",
                    Description = "Kim không đúng trục tâm, đâm lệch lỗ.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 18,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a123-0015-0015-0015-000000000015"),
                    ErrorCode = "LOI_QUAT_GIO",
                    Name = "Lỗi Quạt Gió",
                    Description = "Quạt tản nhiệt không hoạt động gây quá nhiệt.",

                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 7,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {

                    Id = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"),
                    ErrorCode = "TRUC_CHINH_LAC",
                    Name = "Trục Chính Lệch",
                    Description = "Trục chính không quay đồng tâm gây rung.",

                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow

                },
                new Error
                {
                    Id = Guid.Parse("e1d1a125-0017-0017-0017-000000000017"),
                    ErrorCode = "DUI_DEN_CHAY",
                    Name = "Đuôi Đèn Cháy",
                    Description = "Đèn máy không sáng do đuôi đèn bị hỏng.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(20),
                    IsCommon = true,
                    OccurrenceCount = 15,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"),
                    ErrorCode = "MAT_BO_NHO_LUU_THONG_SO",
                    Name = "Mất Bộ Nhớ Lưu Thông Số",
                    Description = "Bộ điều khiển không lưu lại các thiết lập máy.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = false,
                    OccurrenceCount = 2,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a127-0019-0019-0019-000000000019"),
                    ErrorCode = "CAM_BIEN_AP_LUC_LOI",
                    Name = "Cảm Biến Áp Lực Lỗi",
                    Description = "Áp lực chân vịt không ổn định do cảm biến sai số.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 5,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a128-0020-0020-0020-000000000020"),
                    ErrorCode = "ROONG_KHONG_DU_SIEU",
                    Name = "Rong Không Đủ Siêu",
                    Description = "Vải bị kéo không đều do lỗi bộ cấp vải.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 10,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"),
                    ErrorCode = "MO_TROI_CHI",
                    Name = "Mỏ Trói Chỉ Bị Lỏng",
                    Description = "Bộ phận giữ chỉ không đủ lực siết, gây bung chỉ khi may.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    IsCommon = true,
                    OccurrenceCount = 13,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"),
                    ErrorCode = "BANH_RANG_MON",
                    Name = "Bánh Răng Mòn",
                    Description = "Bánh răng truyền động bị mòn, phát ra tiếng kêu hoặc trượt.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 17,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a131-0023-0023-0023-000000000023"),
                    ErrorCode = "CAM_TAY_KHONG_AN_KHOP",
                    Name = "Cần Tay Không Ăn Khớp",
                    Description = "Bộ phận điều khiển bằng tay không ăn khớp với cơ cấu truyền động.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 3,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a132-0024-0024-0024-000000000024"),
                    ErrorCode = "KIM_CHAM_VAI",
                    Name = "Kim Chạm Vải",
                    Description = "Kim va vào mặt vải hoặc phụ liệu, có thể gây hỏng bề mặt.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    IsCommon = true,
                    OccurrenceCount = 22,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a133-0025-0025-0025-000000000025"),
                    ErrorCode = "DAY_KHOI_DONG_LOI",
                    Name = "Dây Khởi Động Lỗi",
                    Description = "Dây nối từ nút khởi động đến động cơ bị hở hoặc đứt.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"),
                    ErrorCode = "BULONG_LONG",
                    Name = "Bu Lông Lỏng",
                    Description = "Một số bu lông cố định các cụm máy bị lỏng gây rung lắc.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(40),
                    IsCommon = true,
                    OccurrenceCount = 19,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a135-0027-0027-0027-000000000027"),
                    ErrorCode = "MACH_DEN_LOI",
                    Name = "Mạch Đèn Lỗi",
                    Description = "Hỏng mạch điện đèn chiếu sáng, gây mất tầm nhìn khu vực may.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    IsCommon = true,
                    OccurrenceCount = 12,
                    Severity = "Low",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a136-0028-0028-0028-000000000028"),
                    ErrorCode = "DAU_BO_NHIEU",
                    Name = "Dầu Bôi Trơn Nhiều",
                    Description = "Dầu bôi trơn ra quá nhiều gây loang vải hoặc trơn trượt bộ truyền.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 11,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a137-0029-0029-0029-000000000029"),
                    ErrorCode = "QUAT_THONG_GIO_YEU",
                    Name = "Quạt Thông Gió Yếu",
                    Description = "Quạt thông gió hoạt động yếu, không đủ làm mát cho mô tơ.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a138-0030-0030-0030-000000000030"),
                    ErrorCode = "CUA_KIM_LECH",
                    Name = "Cửa Kim Lệch",
                    Description = "Cửa kim không thẳng hàng với trục kim gây lệch đường may.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 20,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow

                },
                new Error
                {
                    Id = Guid.Parse("e1d1a139-0031-0031-0031-000000000031"),
                    ErrorCode = "ONG_DAN_CHI_HONG",
                    Name = "Ống Dẫn Chỉ Hỏng",
                    Description = "Ống dẫn chỉ bị mòn hoặc gãy, gây rối hoặc đứt chỉ.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    IsCommon = true,
                    OccurrenceCount = 16,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a140-0032-0032-0032-000000000032"),
                    ErrorCode = "SUOT_CHI_KHONG_QUAY",
                    Name = "Suốt Chỉ Không Quay",
                    Description = "Suốt chỉ dưới không quay, ngăn chỉ dưới tham gia may.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 14,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a141-0033-0033-0033-000000000033"),
                    ErrorCode = "CHAN_VIT_KHONG_DONG_BO",
                    Name = "Chân Vịt Không Đồng Bộ",
                    Description = "Chân vịt không đồng bộ với kim, gây rách hoặc lệch vải.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 12,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a142-0034-0034-0034-000000000034"),
                    ErrorCode = "DAY_DIEN_NGUON_HO",
                    Name = "Dây Điện Nguồn Hở",
                    Description = "Dây nguồn bị hở, gây mất điện hoặc chập điện.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 5,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a143-0035-0035-0035-000000000035"),
                    ErrorCode = "BANG_TAI_HONG",
                    Name = "Băng Tải Hỏng",
                    Description = "Băng tải truyền động bị lỏng hoặc đứt, dừng chuyển động vải.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 8,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a144-0036-0036-0036-000000000036"),
                    ErrorCode = "TRUC_KIM_GAY",
                    Name = "Trục Kim Gãy",
                    Description = "Trục kim bị gãy, ngăn kim di chuyển lên xuống.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 3,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a145-0037-0037-0037-000000000037"),
                    ErrorCode = "CAM_BIEN_NHIET_DO_LOI",
                    Name = "Cảm Biến Nhiệt Độ Lỗi",
                    Description = "Cảm biến nhiệt độ không hoạt động, không cảnh báo quá nhiệt.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a146-0038-0038-0038-000000000038"),
                    ErrorCode = "MO_HOP_SO_KHONG_KHOP",
                    Name = "Mô Hộp Số Không Khớp",
                    Description = "Hộp số truyền động bị lệch, gây tiếng ồn hoặc mất lực.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 7,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"),
                    ErrorCode = "RANH_CUA_MON",
                    Name = "Răng Cưa Mòn",
                    Description = "Răng cưa kéo vải bị mòn, gây trượt hoặc không kéo vải.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 15,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a148-0040-0040-0040-000000000040"),
                    ErrorCode = "CAP_NGUON_YEU",
                    Name = "Cáp Nguồn Yếu",
                    Description = "Cáp nguồn bị mòn, cung cấp điện không ổn định.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 10,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"),
                    ErrorCode = "MO_CANG_CHI_LOI",
                    Name = "Mô Căng Chỉ Lỗi",
                    Description = "Bộ căng chỉ bị kẹt hoặc lỏng, gây chỉ không đều.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    IsCommon = true,
                    OccurrenceCount = 18,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a150-0042-0042-0042-000000000042"),
                    ErrorCode = "BO_NHO_DIEU_KHIEN_HONG",
                    Name = "Bộ Nhớ Điều Khiển Hỏng",
                    Description = "Bộ nhớ lưu chương trình may bị lỗi, gây mất cài đặt.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a151-0043-0043-0043-000000000043"),
                    ErrorCode = "TRUC_CHUYEN_DONG_KET",
                    Name = "Trục Chuyển Động Kẹt",
                    Description = "Trục chuyển động bị kẹt do bẩn hoặc thiếu dầu bôi trơn.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 13,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a152-0044-0044-0044-000000000044"),
                    ErrorCode = "CAM_BIEN_VAN_TOC_LOI",
                    Name = "Cảm Biến Vận Tốc Lỗi",
                    Description = "Cảm biến vận tốc không chính xác, gây tốc độ máy không ổn định.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 5,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a153-0045-0045-0045-000000000045"),
                    ErrorCode = "PHAO_CHI_HONG",
                    Name = "Pháo Chỉ Hỏng",
                    Description = "Pháo chỉ dưới bị mòn hoặc hỏng, gây lỗi đường may dưới.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 12,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a154-0046-0046-0046-000000000046"),
                    ErrorCode = "BO_CAU_CHI_LOI",
                    Name = "Bộ Câu Chỉ Lỗi",
                    Description = "Bộ câu chỉ không hoạt động đúng, không kéo chỉ dưới lên.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 11,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a155-0047-0047-0047-000000000047"),
                    ErrorCode = "ONG_DAU_BI_TAT",
                    Name = "Ống Dầu Bị Tắc",
                    Description = "Ống dẫn dầu bôi trơn bị tắc, gây thiếu dầu cho bộ truyền.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 9,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a156-0048-0048-0048-000000000048"),
                    ErrorCode = "BO_TRUYEN_DONG_YEU",
                    Name = "Bộ Truyền Động Yếu",
                    Description = "Bộ truyền động mất lực do mòn hoặc hỏng bánh răng.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a157-0049-0049-0049-000000000049"),
                    ErrorCode = "CHAN_VIT_KHONG_NANG",
                    Name = "Chân Vịt Không Nâng",
                    Description = "Cơ cấu nâng chân vịt bị kẹt, không di chuyển được.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 10,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a158-0050-0050-0050-000000000050"),
                    ErrorCode = "CAM_BIEN_KIM_LOI",
                    Name = "Cảm Biến Kim Lỗi",
                    Description = "Cảm biến kim không nhận diện vị trí, gây dừng máy bất thường.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a159-0051-0051-0051-000000000051"),
                    ErrorCode = "MO_DAN_VAI_HONG",
                    Name = "Mô Dẫn Vải Hỏng",
                    Description = "Mô dẫn vải bị mòn, không kéo vải đều khi may.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 13,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a160-0052-0052-0052-000000000052"),
                    ErrorCode = "BO_DIEU_KHIEN_QUA_NHIET",
                    Name = "Bộ Điều Khiển Quá Nhiệt",
                    Description = "Bo mạch điều khiển quá nóng, gây gián đoạn hoạt động.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 3,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a161-0053-0053-0053-000000000053"),
                    ErrorCode = "TRUC_CANG_CHI_GAY",
                    Name = "Trục Căng Chỉ Gãy",
                    Description = "Trục căng chỉ bị gãy, không điều chỉnh được độ căng chỉ.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = false,
                    OccurrenceCount = 5,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a162-0054-0054-0054-000000000054"),
                    ErrorCode = "RANH_CUA_KHONG_DONG_BO",
                    Name = "Răng Cưa Không Đồng Bộ",
                    Description = "Răng cưa kéo vải không đồng bộ với kim, gây lỗi đường may.",
                    EstimatedRepairTime = TimeSpan.FromHours(1.5),
                    IsCommon = true,
                    OccurrenceCount = 11,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"),
                    ErrorCode = "BO_NGUON_HONG",
                    Name = "Bộ Nguồn Hỏng",
                    Description = "Bộ nguồn cung cấp điện bị hỏng, không cấp điện cho máy.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 4,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a164-0056-0056-0056-000000000056"),
                    ErrorCode = "DAY_CUROA_GAY",
                    Name = "Dây Curoa Gãy",
                    Description = "Dây curoa truyền động bị gãy, dừng hoàn toàn chuyển động.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = true,
                    OccurrenceCount = 9,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a165-0057-0057-0057-000000000057"),
                    ErrorCode = "CAM_BIEN_CHI_LOI",
                    Name = "Cảm Biến Chỉ Lỗi",
                    Description = "Cảm biến chỉ không nhận diện được chỉ, gây lỗi may.",
                    EstimatedRepairTime = TimeSpan.FromHours(1),
                    IsCommon = false,
                    OccurrenceCount = 6,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a166-0058-0058-0058-000000000058"),
                    ErrorCode = "BO_KHOP_NOI_HONG",
                    Name = "Bộ Khớp Nối Hỏng",
                    Description = "Bộ khớp nối truyền động bị mòn hoặc gãy, gây mất lực.",
                    EstimatedRepairTime = TimeSpan.FromHours(2),
                    IsCommon = false,
                    OccurrenceCount = 7,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a167-0059-0059-0059-000000000059"),
                    ErrorCode = "LO_XO_CANG_CHI_YEU",
                    Name = "Lò Xo Căng Chỉ Yếu",
                    Description = "Lò xo căng chỉ mất độ đàn hồi, gây chỉ lỏng khi may.",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    IsCommon = true,
                    OccurrenceCount = 14,
                    Severity = "Medium",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Error
                {
                    Id = Guid.Parse("e1d1a168-0060-0060-0060-000000000060"),
                    ErrorCode = "MO_DONG_CO_MON",
                    Name = "Mô Động Cơ Mòn",
                    Description = "Mô động cơ bị mòn, giảm công suất và gây rung lắc.",
                    EstimatedRepairTime = TimeSpan.FromHours(2.5),
                    IsCommon = false,
                    OccurrenceCount = 5,
                    Severity = "High",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
