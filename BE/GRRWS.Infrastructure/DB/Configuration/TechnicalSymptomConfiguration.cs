using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class TechnicalSymptomConfiguration : IEntityTypeConfiguration<TechnicalSymptom>
    {
        public void Configure(EntityTypeBuilder<TechnicalSymptom> builder)
        {
            // Configure index for SymptomCode
            builder.HasIndex(ts => ts.SymptomCode)
                   .HasDatabaseName("IX_TechnicalSymptoms_SymptomCode");

            // Seed data for TechnicalSymptom (20 records)
            builder.HasData(
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a1a1a1a1-1111-1111-1111-111111111111"),
                    SymptomCode = "MAY_TOA_NHIET_QUA_MUC",
                    Name = "Máy Tỏa Nhiệt Quá Mức",
                    Description = "Máy tỏa nhiệt cao bất thường từ thân máy, có thể do quá tải hoặc ma sát cơ học.",
                    IsCommon = true,
                    OccurrenceCount = 25,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a2a2a2a2-2222-2222-2222-222222222222"),
                    SymptomCode = "KIM_GAY_THUONG_XUYEN",
                    Name = "Kim Gãy Thường Xuyên",
                    Description = "Kim may gãy liên tục, có thể do lệch tâm hoặc áp lực chân vịt không đều.",
                    IsCommon = true,
                    OccurrenceCount = 20,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a3a3a3a3-3333-3333-3333-333333333333"),
                    SymptomCode = "MAY_KHONG_NHAN_DIEN",
                    Name = "Máy Không Nhận Điện",
                    Description = "Máy không khởi động, có thể do mất kết nối nguồn hoặc công tắc bị lỗi.",
                    IsCommon = true,
                    OccurrenceCount = 15,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a4a4a4a4-4444-4444-4444-444444444444"),
                    SymptomCode = "RO_RI_DAU_BOI_TRON",
                    Name = "Rò Rỉ Dầu Bôi Trơn",
                    Description = "Dầu bôi trơn rò rỉ ra ngoài từ các khe máy, gây bẩn bề mặt.",
                    IsCommon = true,
                    OccurrenceCount = 18,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a5a5a5a5-5555-5555-5555-555555555555"),
                    SymptomCode = "TIENG_ON_CO_HOC",
                    Name = "Tiếng Ồn Cơ Học",
                    Description = "Máy phát ra tiếng ồn lớn, có thể do các bộ phận lỏng lẻo hoặc ma sát bất thường.",
                    IsCommon = true,
                    OccurrenceCount = 22,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a6a6a6a6-6666-6666-6666-666666666666"),
                    SymptomCode = "HU_HONG_VAI",
                    Name = "Hư Hỏng Vải",
                    Description = "Vải bị rách hoặc xước do kim hoặc chân vịt không đồng bộ.",
                    IsCommon = true,
                    OccurrenceCount = 17,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a7a7a7a7-7777-7777-7777-777777777777"),
                    SymptomCode = "DUONG_MAY_KHONG_ON_DINH",
                    Name = "Đường May Không Ổn Định",
                    Description = "Đường may không đều, có thể do căng chỉ không phù hợp hoặc kim lệch.",
                    IsCommon = true,
                    OccurrenceCount = 19,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a8a8a8a8-8888-8888-8888-888888888888"),
                    SymptomCode = "CHI_MAY_DUT_LIEN_TUC",
                    Name = "Chỉ May Đứt Liên Tục",
                    Description = "Chỉ may thường xuyên bị đứt, có thể do độ căng chỉ hoặc kim không phù hợp.",
                    IsCommon = true,
                    OccurrenceCount = 20,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("a9a9a9a9-9999-9999-9999-999999999999"),
                    SymptomCode = "MAY_HOAT_DONG_GIAN_DOAN",
                    Name = "Máy Hoạt Động Gián Đoạn",
                    Description = "Máy chạy không ổn định, tự dừng rồi khởi động lại, có thể do nguồn điện hoặc cảm biến.",
                    IsCommon = true,
                    OccurrenceCount = 14,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b1b1b1b1-1111-1111-1111-111111111112"),
                    SymptomCode = "TOC_DO_MAY_CHAM",
                    Name = "Tốc Độ Máy Chậm",
                    Description = "Máy hoạt động chậm hơn bình thường, có thể do ma sát cơ học hoặc động cơ yếu.",
                    IsCommon = true,
                    OccurrenceCount = 16,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b2b2b2b2-2222-2222-2222-222222222223"),
                    SymptomCode = "CANG_CHI_KHONG_DEU",
                    Name = "Căng Chỉ Không Đều",
                    Description = "Chỉ may có độ căng không đồng đều, dẫn đến đường may lỏng hoặc chặt.",
                    IsCommon = true,
                    OccurrenceCount = 18,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b3b3b3b3-3333-3333-3333-333333333334"),
                    SymptomCode = "DEN_BAO_LOI_KICH_HOAT",
                    Name = "Đèn Báo Lỗi Kích Hoạt",
                    Description = "Đèn báo lỗi sáng, có thể do cảm biến hoặc bo mạch báo hiệu vấn đề.",
                    IsCommon = true,
                    OccurrenceCount = 15,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b4b4b4b4-4444-4444-4444-444444444445"),
                    SymptomCode = "BAN_DAP_KHONG_PHAN_HOI",
                    Name = "Bàn Đạp Không Phản Hồi",
                    Description = "Bàn đạp không điều khiển được máy, có thể do mất kết nối hoặc hỏng cơ cấu.",
                    IsCommon = true,
                    OccurrenceCount = 12,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b5b5b5b5-5555-5555-5555-555555555556"),
                    SymptomCode = "DEN_CHIEU_SANG_HONG",
                    Name = "Đèn Chiếu Sáng Hỏng",
                    Description = "Đèn chiếu sáng trên máy không hoạt động, có thể do bóng đèn hoặc mạch điện.",
                    IsCommon = true,
                    OccurrenceCount = 10,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b6b6b6b6-6666-6666-6666-666666666667"),
                    SymptomCode = "KIM_KHONG_DI_CHUYEN",
                    Name = "Kim Không Di Chuyển",
                    Description = "Kim may không chuyển động, có thể do cơ cấu truyền động hoặc kẹt kim.",
                    IsCommon = true,
                    OccurrenceCount = 13,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b7b7b7b7-7777-7777-7777-777777777778"),
                    SymptomCode = "MAY_RUNG_LAC_MANH",
                    Name = "Máy Rung Lắc Mạnh",
                    Description = "Máy rung lắc bất thường, có thể do các bộ phận lỏng hoặc lệch trục.",
                    IsCommon = true,
                    OccurrenceCount = 20,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b8b8b8b8-8888-8888-8888-888888888889"),
                    SymptomCode = "MUI_DAU_BOI_TRON",
                    Name = "Mùi Dầu Bôi Trơn Mạnh",
                    Description = "Máy phát ra mùi dầu bôi trơn nồng, có thể do rò rỉ hoặc dầu thừa.",
                    IsCommon = true,
                    OccurrenceCount = 14,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("b9b9b9b9-9999-9999-9999-999999999990"),
                    SymptomCode = "VAI_KHONG_TIEN",
                    Name = "Vải Không Tiến",
                    Description = "Vải không được kéo qua máy, có thể do chân vịt hoặc răng cưa bị kẹt.",
                    IsCommon = true,
                    OccurrenceCount = 16,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("c1c1c1c1-1111-1111-1111-111111111113"),
                    SymptomCode = "MAY_TU_NGUNG",
                    Name = "Máy Tự Ngừng",
                    Description = "Máy tự động dừng đột ngột, có thể do cảm biến hoặc mạch điều khiển.",
                    IsCommon = true,
                    OccurrenceCount = 11,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("c2c2c2c2-2222-2222-2222-222222222224"),
                    SymptomCode = "MUI_KHET_TU_MAY",
                    Name = "Mùi Khét Từ Máy",
                    Description = "Máy phát ra mùi khét, có thể do quá nhiệt hoặc dây điện bị nóng.",
                    IsCommon = false,
                    OccurrenceCount = 8,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d1d1d1d1-1111-1111-1111-111111111114"),
                    SymptomCode = "CHI_KHONG_LEN",
                    Name = "Chỉ Không Được Kéo Lên",
                    Description = "Cơ cấu kéo chỉ trên không hoạt động, dẫn đến chỉ không xuất hiện trên vải.",
                    IsCommon = true,
                    OccurrenceCount = 15,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d2d2d2d2-2222-2222-2222-222222222225"),
                    SymptomCode = "THOI_DIEM_MOC_SAI",
                    Name = "Thời Điểm Móc Chỉ Sai",
                    Description = "Móc chỉ dưới không đồng bộ với kim, gây ra việc chỉ không được kéo lên.",
                    IsCommon = true,
                    OccurrenceCount = 12,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d3d3d3d3-3333-3333-3333-333333333336"),
                    SymptomCode = "TIENG_KHI_XI",
                    Name = "Tiếng Khí Xì Ra",
                    Description = "Máy phát ra tiếng xì xì do rò rỉ khí từ các bộ phận kín hoặc ống dẫn.",
                    IsCommon = true,
                    OccurrenceCount = 10,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d4d4d4d4-4444-4444-4444-444444444447"),
                    SymptomCode = "RANG_CUA_KET",
                    Name = "Răng Cưa Bị Kẹt",
                    Description = "Răng cưa kéo vải bị kẹt, ngăn vải di chuyển qua máy.",
                    IsCommon = true,
                    OccurrenceCount = 18,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d5d5d5d5-5555-5555-5555-555555555558"),
                    SymptomCode = "CANG_CHI_DUOI_LONG",
                    Name = "Căng Chỉ Dưới Quá Lỏng",
                    Description = "Độ căng chỉ dưới quá lỏng, gây rối chỉ và tạo cục dưới vải.",
                    IsCommon = true,
                    OccurrenceCount = 16,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d6d6d6d6-6666-6666-6666-666666666669"),
                    SymptomCode = "KICH_THUOC_KIM_SAI",
                    Name = "Kích Thước Kim Không Phù Hợp",
                    Description = "Kim quá lớn hoặc không phù hợp với vải, gây lỗ lớn hoặc hỏng vải.",
                    IsCommon = true,
                    OccurrenceCount = 14,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d7d7d7d7-7777-7777-7777-777777777780"),
                    SymptomCode = "NGUON_DIEN_KHONG_ON_DINH",
                    Name = "Nguồn Điện Không Ổn Định",
                    Description = "Nguồn điện dao động, gây ra hiện tượng máy giật mạnh khi chạy.",
                    IsCommon = true,
                    OccurrenceCount = 13,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("d8d8d8d8-8888-8888-8888-888888888891"),
                    SymptomCode = "DONG_CO_QUA_NONG",
                    Name = "Động Cơ Quá Nóng",
                    Description = "Động cơ bị quá nhiệt, có thể gây khói nhẹ và mùi khí cháy.",
                    IsCommon = false,
                    OccurrenceCount = 8,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e1e1e1e1-1111-1111-1111-111111111115"),
                    SymptomCode = "BO_PHAN_CO_KHI_LONG",
                    Name = "Bộ Phận Cơ Khí Lỏng Lẻo",
                    Description = "Các bộ phận cơ khí như bánh răng hoặc trục bị lỏng, gây tiếng lạch cạch khi chạy.",
                    IsCommon = true,
                    OccurrenceCount = 16,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e2e2e2e2-2222-2222-2222-222222222226"),
                    SymptomCode = "NGUON_DIEN_HONG",
                    Name = "Nguồn Điện Hỏng",
                    Description = "Nguồn điện không được cung cấp đến máy, có thể do công tắc hoặc dây nguồn hỏng.",
                    IsCommon = true,
                    OccurrenceCount = 14,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e3e3e3e3-3333-3333-3333-333333333337"),
                    SymptomCode = "KIM_MAI_MON",
                    Name = "Kim Mài Mòn Hoặc Hỏng",
                    Description = "Kim may bị mòn hoặc cong, không đủ sức xuyên qua vải để tạo đường may.",
                    IsCommon = true,
                    OccurrenceCount = 15,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e4e4e4e4-4444-4444-4444-444444444448"),
                    SymptomCode = "THANH_KIM_NGOAI_LECH",
                    Name = "Thanh Kim Bị Ngắt Kết Nối",
                    Description = "Thanh kim không kết nối với cơ cấu truyền động, khiến máy chạy nhưng kim không di chuyển.",
                    IsCommon = true,
                    OccurrenceCount = 12,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e5e4e5e4-5554-5555-4444-555555555559"),
                    SymptomCode = "LINH_KIEN_DIEN_QUA_NONG",
                    Name = "Linh Kiện Điện Quá Nóng",
                    Description = "Các linh kiện điện như dây hoặc bo mạch quá nóng, gây mùi nhựa hoặc kim loại cháy.",
                    IsCommon = false,
                    OccurrenceCount = 9,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e6e6e6e6-6666-6666-6666-666666666670"),
                    SymptomCode = "CO_CAU_CHAN_VIT_CUNG",
                    Name = "Cơ Cấu Chân Vịt Cứng",
                    Description = "Cơ cấu nâng/hạ chân vịt bị kẹt hoặc thiếu bôi trơn, làm chân vịt nặng khi điều chỉnh.",
                    IsCommon = true,
                    OccurrenceCount = 13,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e7e7e7e7-7777-7777-7777-777777777781"),
                    SymptomCode = "O_CMA_DONG_CO_MA_NG",
                    Name = "Ổ Cam Động Cơ Mài Mòn",
                    Description = "Ổ cam trong động cơ bị mài mòn, gây tiếng rè rè liên tục khi máy chạy.",
                    IsCommon = true,
                    OccurrenceCount = 11,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e8e8e8e8-8888-8888-8888-888888888892"),
                    SymptomCode = "AP_LUC_CHAN_VIT_SAI",
                    Name = "Áp Lực Chân Vịt Không Phù Hợp",
                    Description = "Áp lực chân vịt quá mạnh hoặc quá yếu, khiến vải bị nhăn hoặc co khi may.",
                    IsCommon = true,
                    OccurrenceCount = 17,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new TechnicalSymptom
                {
                    Id = Guid.Parse("e8e8e8e8-e8e8-e8e8-e8e8-e8e8e8e8e8e8"),
                    SymptomCode = "CAN_BAO_HANH",
                    Name = "Cần bảo hành",
                    Description = "Thiết bị cần được đem đi bảo hành.",
                    IsCommon = true,
                    OccurrenceCount = 5,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}