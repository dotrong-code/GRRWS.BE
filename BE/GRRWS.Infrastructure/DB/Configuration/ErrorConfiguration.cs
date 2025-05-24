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

                }
            );
        }
    }
}
