using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            // Cấu hình chỉ mục cho IssueKey
            builder.HasIndex(i => i.IssueKey)
                   .HasDatabaseName("IX_Issues_IssueKey");

            // Dữ liệu mẫu (20 bản ghi)
            builder.HasData(
                // 10 bản ghi từ dữ liệu gốc
                new Issue
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IssueKey = "MAY_NONG",
                    DisplayName = "Máy Nóng",
                    Description = "Máy may bị nóng sau thời gian sử dụng ngắn.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IssueKey = "KIM_GAY",
                    DisplayName = "Kim Gãy",
                    Description = "Kim bị gãy trong quá trình may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IssueKey = "MAY_KHONG_CHAY",
                    DisplayName = "Máy Không Chạy",
                    Description = "Máy không khởi động hoặc không hoạt động khi bật công tắc.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    IssueKey = "CHAY_DAU",
                    DisplayName = "Chảy Dầu",
                    Description = "Máy bị chảy dầu ra ngoài, ảnh hưởng đến hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    IssueKey = "KEU_TO",
                    DisplayName = "Kêu To",
                    Description = "Máy phát ra tiếng ồn lớn bất thường khi hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    IssueKey = "RACH_VAI",
                    DisplayName = "Rách Vải",
                    Description = "Máy làm rách vải trong quá trình may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    IssueKey = "LUOI_KIM",
                    DisplayName = "Lưỡi Kim",
                    Description = "Kim không xuyên đúng vị trí gây lỗi đường may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    IssueKey = "DUT_CHI",
                    DisplayName = "Đứt Chỉ",
                    Description = "Chỉ bị đứt liên tục trong quá trình sử dụng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    IssueKey = "KHONG_CUON_CHI",
                    DisplayName = "Không Cuốn Chỉ",
                    Description = "Máy không cuốn chỉ hoặc chỉ bị rối.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    IssueKey = "MAY_CHAY_CHAM",
                    DisplayName = "Máy Chạy Chậm",
                    Description = "Máy chạy chậm hoặc không đều tốc độ.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // 10 bản ghi mới
                new Issue
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    IssueKey = "CHI_KHONG_DEU",
                    DisplayName = "Chỉ Không Đều",
                    Description = "Đường chỉ may không đều, lúc chặt lúc lỏng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    IssueKey = "DEN_BAO_LOI",
                    DisplayName = "Đèn Báo Lỗi",
                    Description = "Đèn báo lỗi trên máy may sáng liên tục.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    IssueKey = "BAN_DAP_KHONG_HOAT_DONG",
                    DisplayName = "Bàn Đạp Không Hoạt Động",
                    Description = "Bàn đạp không phản hồi khi sử dụng.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    IssueKey = "VAI_BI_NHAN",
                    DisplayName = "Vải Bị Nhăn",
                    Description = "Vải bị nhăn hoặc co kéo trong quá trình may.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    IssueKey = "KIM_KHONG_DI_CHUYEN",
                    DisplayName = "Kim Không Di Chuyển",
                    Description = "Kim may không di chuyển khi máy hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("12121212-1212-1212-1212-121212121212"),
                    IssueKey = "ONG_CHI_LOI",
                    DisplayName = "Ống Chỉ Lỗi",
                    Description = "Ống chỉ bị kẹt hoặc không quay đúng cách.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("23232323-2323-2323-2323-232323232323"),
                    IssueKey = "DAY_CUROA_LOI",
                    DisplayName = "Dây Curoa Lỗi",
                    Description = "Dây curoa bị lỏng hoặc đứt, gây ngừng máy.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Issue
                {
                    Id = Guid.Parse("34343434-3434-3434-3434-343434343434"),
                    IssueKey = "CHI_DUOI_LOI",
                    DisplayName = "Chỉ Dưới Lỗi",
                    Description = "Chỉ dưới không được kéo lên đúng cách.",
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
                    IssueKey = "NUT_DIEU_CHINH_LOI",
                    DisplayName = "Nút Điều Chỉnh Lỗi",
                    Description = "Nút điều chỉnh độ căng chỉ không hoạt động.",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
