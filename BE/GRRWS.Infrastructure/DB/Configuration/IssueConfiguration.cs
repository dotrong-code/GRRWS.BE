using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasIndex(i => i.IssueKey)
                   .HasDatabaseName("IX_Issues_IssueKey");

            builder.HasData(
                new Issue
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IssueKey = "MAY_NONG",
                    DisplayName = "Máy Bị Nóng",
                    Description = "Máy trở nên rất nóng sau khi sử dụng một lúc.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IssueKey = "KIM_GAY",
                    DisplayName = "Kim Dễ Bị Gãy",
                    Description = "Kim bị gãy liên tục trong quá trình may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IssueKey = "MAY_KHONG_CHAY",
                    DisplayName = "Máy Không Chạy Được",
                    Description = "Máy không khởi động được khi bật công tắc.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    IssueKey = "CHAY_DAU",
                    DisplayName = "Máy Bị Chảy Dầu",
                    Description = "Có dầu chảy ra từ máy, làm bẩn khu vực xung quanh.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    IssueKey = "KEU_TO",
                    DisplayName = "Máy Kêu Rất To",
                    Description = "Máy phát ra âm thanh rất to khi đang chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    IssueKey = "RACH_VAI",
                    DisplayName = "Máy Làm Rách Vải",
                    Description = "Máy làm rách vải trong quá trình may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    IssueKey = "DUONG_MAY_LOI",
                    DisplayName = "Đường May Xấu, Không Thẳng",
                    Description = "Đường may không thấy thẳng, không đều lúc may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    IssueKey = "DUT_CHI",
                    DisplayName = "Chỉ Bị Đứt Nhiều",
                    Description = "Chỉ may bị đứt liên tục trong lúc sử dụng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    IssueKey = "MAY_CHOP_TAT",
                    DisplayName = "Máy Lúc Chạy Lúc Dừng",
                    Description = "Máy lúc chạy lúc dừng, hoạt động không ổn định.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    IssueKey = "MAY_CHAY_CHAM",
                    DisplayName = "Máy Chạy Rất Chậm",
                    Description = "Máy chạy chậm hơn bình thường rất nhiều",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // 10 bản ghi mới
                new Issue
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    IssueKey = "CHI_KHONG_DEU",
                    DisplayName = "Đường Chỉ Không Đều",
                    Description = "Chỉ may lúc chặt, lúc lỏng, không đều.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    IssueKey = "DEN_BAO_LOI",
                    DisplayName = "Đèn Báo Lỗi Sáng Liên Tục",
                    Description = "Đèn báo lỗi trên máy sáng liên tục.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    IssueKey = "BAN_DAP_KHONG_HOAT_DONG",
                    DisplayName = "Bàn Đạp Máy Không Hoạt Động",
                    Description = "Bàn đạp máy may không hoạt động khi đạp.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    IssueKey = "DEN_KHONG_SANG",
                    DisplayName = "Đèn Máy Không Sáng",
                    Description = "Đèn chiếu sáng trên máy không hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    IssueKey = "KIM_KHONG_DI_CHUYEN",
                    DisplayName = "Kim Không Di Chuyển",
                    Description = "Kim may đứng yên dù máy đang chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("12121212-1212-1212-1212-121212121212"),
                    IssueKey = "MAY_RUNG_LAC",
                    DisplayName = "Máy Rung Lắc",
                    Description = "Máy rung lắc mạnh bất thường khi hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("23232323-2323-2323-2323-232323232323"),
                    IssueKey = "MUI_DAU_NANG",
                    DisplayName = "Máy Có Mùi Dầu Nặng",
                    Description = "Máy phát ra mùi dầu mạnh, bất thường.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("abababab-abab-abab-abab-abababababab"),
                    IssueKey = "VAI_KHONG_DI_CHUYEN",
                    DisplayName = "Vải Không Di Chuyển Lúc May",
                    Description = "Vải không được kéo đi khi máy đang may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("45454545-4545-4545-4545-454545454545"),
                    IssueKey = "MAY_TU_DUNG",
                    DisplayName = "Máy Tự Dừng",
                    Description = "Máy tự động dừng trong khi đang hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("56565656-5656-5656-5656-565656565656"),
                    IssueKey = "MUI_KHET",
                    DisplayName = "Máy Có Mùi Khét",
                    Description = "Máy phát ra mùi khét khi hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },


                new Issue
                {
                    Id = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                    IssueKey = "MAY_KHONG_LEN_CHI",
                    DisplayName = "Máy Không Thấy Chỉ Kéo Lên",
                    Description = "Máy không kéo được chỉ lên để may, chỉ không xuất hiện trên vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                    IssueKey = "TIENG_XI_XI",
                    DisplayName = "Máy Kêu Xì Xì",
                    Description = "Máy phát ra tiếng xì xì lạ khi chạy, giống như có khí thoát ra.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                    IssueKey = "VAI_BI_KET",
                    DisplayName = "Vải Bị Kẹt Trong Máy",
                    Description = "Vải bị kẹt cứng trong máy, không kéo ra được.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                    IssueKey = "CHI_QUAN_LUNG_TUNG",
                    DisplayName = "Chỉ May Quấn Lung Tung",
                    Description = "Chỉ may bị quấn rối ở dưới vải, tạo thành cục lùng nhùng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("60606060-6060-6060-6060-606060606060"),
                    IssueKey = "KIM_DAM_VAO_VAI",
                    DisplayName = "Kim Đâm Vào Vải Sâu, Lỗ To",
                    Description = "Kim may đâm sâu vào vải, để lại lỗ lớn hoặc làm hỏng vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("70707070-7070-7070-7070-707070707070"),
                    IssueKey = "TIENG_LACH_CACH",
                    DisplayName = "Máy Kêu Lạch Cạch",
                    Description = "Máy phát ra tiếng lạch cạch đều đều khi chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("80808080-8080-8080-8080-808080808080"),
                    IssueKey = "MAY_BI_DO",
                    DisplayName = "Máy Bật Công Tắc Không Chạy",
                    Description = "Máy không phản hồi, đứng im dù đã bật công tắc.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                    IssueKey = "CHI_KHONG_XUYEN_VAI",
                    DisplayName = "Chỉ Không Xuyên Qua Vải",
                    Description = "Chỉ may không đi qua vải, không tạo được đường may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("1a1a1a1a-1a1a-1a1a-1a1a-1a1a1a1a1a1a"),
                    IssueKey = "MAY_CHAY_NHUNG_KHONG_MAY",
                    DisplayName = "Máy Chạy Nhưng Không May Được",
                    Description = "Máy chạy nhưng không tạo được đường may trên vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("2b2b2b2b-2b2b-2b2b-2b2b-2b2b2b2b2b2b"),
                    IssueKey = "MUI_NONG_TU_MAY",
                    DisplayName = "Máy Có Mùi Nóng",
                    Description = "Máy phát ra mùi nóng, như mùi nhựa hoặc kim loại bị nóng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"),
                    IssueKey = "CHAN_VIT_NANG",
                    DisplayName = "Chân Vịt Nặng",
                    Description = "Chân vịt khó nâng lên hoặc hạ xuống, cảm giác nặng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("4d4d4d4d-4d4d-4d4d-4d4d-4d4d4d4d4d4d"),
                    IssueKey = "TIENG_RE_RE",
                    DisplayName = "Máy Kêu Rè Rè",
                    Description = "Máy phát ra tiếng rè rè nhỏ nhưng liên tục khi chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"),
                    IssueKey = "VAI_BI_NHĂN",
                    DisplayName = "Vải Bị Nhăn Khi May",
                    Description = "Vải bị nhăn hoặc co lại khi máy may qua.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("6f6f6f6f-6f6f-6f6f-6f6f-6f6f6f6f6f6f"),
                    IssueKey = "MAY_GIAT_DIEN",
                    DisplayName = "Máy Làm Bị Giật Điện",
                    Description = "Máy chạm vô làm bị giật điện",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("7a7a7a7a-7a7a-7a7a-7a7a-7a7a7a7a7a7a"),
                    IssueKey = "KIM_DI_SAI",
                    DisplayName = "Kim May Sai Lệch Vị Trí",
                    Description = "Kim may không đâm đúng vị trí, lệch khỏi đường may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("8b8b8b8b-8b8b-8b8b-8b8b-8b8b8b8b8b8b"),
                    IssueKey = "MAY_PHAT_TIENG_LA",
                    DisplayName = "Máy Phát Ra Tiếng Lạ",
                    Description = "Máy kêu những tiếng lạ, không giống tiếng bình thường.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("9c9c9c9c-9c9c-9c9c-9c9c-9c9c9c9c9c9c"),
                    IssueKey = "CHI_LUNG_TREN_VAI",
                    DisplayName = "Chỉ Bị Rối Tung Trên Vải",
                    Description = "Chỉ may lỏng lẻo, tạo vòng trên bề mặt vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("adadadad-adad-adad-adad-adadadadadad"),
                    IssueKey = "MAY_HOI_RUN",
                    DisplayName = "Máy Hơi Rung Lắc, Giật Nhẹ",
                    Description = "Máy có cảm giác giật nhẹ hoặc rung nhỏ khi chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("bebebebe-bebe-bebe-bebe-bebebebebebe"),
                    IssueKey = "DAU_BAM_TREN_VAI",
                    DisplayName = "Dầu Bị Bám Trên Vải",
                    Description = "Vải may xong có vết dầu bám, làm bẩn sản phẩm.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"),
                    IssueKey = "TIENG_KEN_KET",
                    DisplayName = "Máy Kêu Kèn Kẹt",
                    Description = "Máy phát ra tiếng kèn kẹt khi chạy, như kim loại cọ xát.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("d0d0d0d0-d0d0-d0d0-d0d0-d0d0d0d0d0d0"),
                    IssueKey = "MAY_KHONG_ON_DINH",
                    DisplayName = "Máy Chạy Không Ổn Định",
                    Description = "Máy chạy không đều, lúc nhanh lúc chậm không kiểm soát.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"),
                    IssueKey = "CHI_DUOI_LOI",
                    DisplayName = "Chỉ Bên Dưới Vải May Không Đẹp",
                    Description = "Chỉ dưới không đều, tạo đường may xấu ở mặt dưới vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"),
                    IssueKey = "KIM_LAM_TRầy_VAI",
                    DisplayName = "Kim May Làm Trầy Vải",
                    Description = "Kim may làm trầy xước bề mặt vải khi may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"),
                    IssueKey = "MAY_BI_GIAT",
                    DisplayName = "Máy Bị Giật",
                    Description = "Máy giật mạnh từng đợt khi đang chạy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("b4b4b4b4-b4b4-b4b4-b4b4-b4b4b4b4b4b4"),
                    IssueKey = "VAI_DI_SAI",
                    DisplayName = "Vải Bị Chạy Sai Hướng",
                    Description = "Vải bị kéo lệch hướng, không thẳng khi may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"),
                    IssueKey = "CHI_TREN_LOI",
                    DisplayName = "Chỉ Bên Trên Vải May Không Đẹp",
                    Description = "Chỉ trên không đều, tạo đường may lỏng ở mặt trên vải.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("d6d6d6d6-d6d6-d6d6-d6d6-d6d6d6d6d6d6"),
                    IssueKey = "MAY_KHONG_MAY_DUOC",
                    DisplayName = "Máy Không May Được",
                    Description = "Máy hoạt động nhưng không tạo được mũi may nào.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"),
                    IssueKey = "TIENG_KHUC_KHAC",
                    DisplayName = "Máy Kêu Khục Khặc",
                    Description = "Máy phát ra tiếng khục khặc, như có gì đó kẹt bên trong.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"),
                    IssueKey = "MAY_BOC_KHOI",
                    DisplayName = "Máy Bốc Khói Nhẹ",
                    Description = "Máy có khói nhẹ bốc ra khi chạy, rất bất thường.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("deadbeef-dead-beef-dead-beefdeadbeef"),
                    IssueKey = "MAY_HU_HONG_NANG",
                    DisplayName = "Máy Bị Hư Hỏng Nặng",
                    Description = "Máy gặp sự cố nghiêm trọng, không thể vận hành hoặc hoạt động bị suy giảm nghiêm trọng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
