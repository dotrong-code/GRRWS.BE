using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class SparepartConfiguration : IEntityTypeConfiguration<Sparepart>
    {
        public void Configure(EntityTypeBuilder<Sparepart> builder)
        {
            builder.HasIndex(sp => sp.SparepartCode)
                   .IsUnique()
                   .HasDatabaseName("IX_Spareparts_SparepartCode");

            builder.HasData(
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    SparepartCode = "SP001",
                    SparepartName = "Kim May Công Nghiệp",
                    Description = "Kim thép không gỉ dùng cho máy may công nghiệp",
                    Specification = "Loại DBX1, cỡ 90/14",
                    StockQuantity = 150,
                    Unit = "Cái",
                    UnitPrice = 5000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    SparepartCode = "SP002",
                    SparepartName = "Dây Curoa",
                    Description = "Dây truyền động cho máy may",
                    Specification = "Chiều dài 1m, bản 10mm",
                    StockQuantity = 60,
                    Unit = "Cái",
                    UnitPrice = 12000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    SparepartCode = "SP003",
                    SparepartName = "Bàn Đạp Máy",
                    Description = "Bàn đạp điều khiển tốc độ",
                    Specification = "Điện áp 220V",
                    StockQuantity = 20,
                    Unit = "Cái",
                    UnitPrice = 45000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    SparepartCode = "SP004",
                    SparepartName = "Ống Chỉ",
                    Description = "Ống chỉ nhựa cho máy may tự động",
                    Specification = "Đường kính 2.5cm",
                    StockQuantity = 500,
                    Unit = "Cái",
                    UnitPrice = 1500,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    SparepartCode = "SP005",
                    SparepartName = "Bộ Điều Khiển Nhiệt",
                    Description = "Điều chỉnh nhiệt độ máy may",
                    Specification = "Tối đa 200°C",
                    StockQuantity = 10,
                    Unit = "Bộ",
                    UnitPrice = 120000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    SparepartCode = "SP006",
                    SparepartName = "Công Tắc Máy May",
                    Description = "Công tắc bật/tắt máy may",
                    Specification = "Công suất 250V - 10A",
                    StockQuantity = 35,
                    Unit = "Cái",
                    UnitPrice = 8000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    SparepartCode = "SP007",
                    SparepartName = "Mô Tơ Máy May",
                    Description = "Mô tơ điện cho máy may công nghiệp",
                    Specification = "Công suất 370W, điện 220V",
                    StockQuantity = 8,
                    Unit = "Cái",
                    UnitPrice = 350000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                    SparepartCode = "SP008",
                    SparepartName = "Đèn LED Gắn Máy",
                    Description = "Đèn chiếu sáng cho khu vực may",
                    Specification = "LED 12V, 5W, dán keo",
                    StockQuantity = 120,
                    Unit = "Cái",
                    UnitPrice = 18000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                    SparepartCode = "SP009",
                    SparepartName = "Trụ Gắn Kim",
                    Description = "Trụ gắn kim thay thế cho đầu máy",
                    Specification = "Thép hợp kim bền cao",
                    StockQuantity = 40,
                    Unit = "Cái",
                    UnitPrice = 22000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                    SparepartCode = "SP010",
                    SparepartName = "Bộ Truyền Kim",
                    Description = "Cơ cấu truyền động kim máy may",
                    Specification = "Cơ khí chính xác cao",
                    StockQuantity = 12,
                    Unit = "Bộ",
                    UnitPrice = 85000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000011"),
                    SparepartCode = "SP011",
                    SparepartName = "Ổ Chỉ Dưới",
                    Description = "Bộ phận giữ chỉ dưới trong máy may",
                    Specification = "Kim loại bền, chuẩn công nghiệp",
                    StockQuantity = 100,
                    Unit = "Cái",
                    UnitPrice = 10000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000012"),
                    SparepartCode = "SP012",
                    SparepartName = "Bánh Răng Truyền Động",
                    Description = "Bánh răng dẫn động kim và trụ máy",
                    Specification = "Hợp kim, răng xoắn",
                    StockQuantity = 60,
                    Unit = "Cái",
                    UnitPrice = 30000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000013"),
                    SparepartCode = "SP013",
                    SparepartName = "Trục Kim Máy May",
                    Description = "Trục truyền động từ mô tơ đến kim",
                    Specification = "Thép tôi cứng, chống mài mòn",
                    StockQuantity = 25,
                    Unit = "Cái",
                    UnitPrice = 40000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    SparepartCode = "SP014",
                    SparepartName = "Giá Đỡ Ống Chỉ",
                    Description = "Khung giữ ống chỉ phía trên máy",
                    Specification = "Nhựa chịu lực hoặc kim loại",
                    StockQuantity = 80,
                    Unit = "Cái",
                    UnitPrice = 7000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000015"),
                    SparepartCode = "SP015",
                    SparepartName = "Cảm Biến Tốc Độ",
                    Description = "Phụ kiện cảm biến tốc độ quay mô tơ",
                    Specification = "Điện áp 5V TTL, chuẩn hall sensor",
                    StockQuantity = 15,
                    Unit = "Cái",
                    UnitPrice = 65000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000016"),
                    SparepartCode = "SP016",
                    SparepartName = "Khung Máy Nhỏ",
                    Description = "Bộ khung bên ngoài cho máy loại nhỏ",
                    Specification = "Nhôm đúc",
                    StockQuantity = 12,
                    Unit = "Bộ",
                    UnitPrice = 90000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000017"),
                    SparepartCode = "SP017",
                    SparepartName = "Đế Cao Su Chống Rung",
                    Description = "Lót chân máy may giảm rung, chống ồn",
                    Specification = "Cao su tổng hợp, đường kính 5cm",
                    StockQuantity = 150,
                    Unit = "Cái",
                    UnitPrice = 3000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000018"),
                    SparepartCode = "SP018",
                    SparepartName = "Puli Dây Curoa",
                    Description = "Bánh dẫn puli gắn với động cơ",
                    Specification = "Đường kính 80mm, thép hợp kim",
                    StockQuantity = 30,
                    Unit = "Cái",
                    UnitPrice = 18000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000019"),
                    SparepartCode = "SP019",
                    SparepartName = "Dây Điện Động Cơ",
                    Description = "Dây cấp nguồn cho mô tơ máy may",
                    Specification = "2 lõi, dài 1.5m, bọc cách điện",
                    StockQuantity = 100,
                    Unit = "Cuộn",
                    UnitPrice = 10000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000020"),
                    SparepartCode = "SP020",
                    SparepartName = "Bộ Gương Chắn Dầu",
                    Description = "Chắn dầu chống tràn ra khỏi ổ",
                    Specification = "Nhựa chịu nhiệt, lắp trong trục máy",
                    StockQuantity = 50,
                    Unit = "Bộ",
                    UnitPrice = 22000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000021"),
                    SparepartCode = "SP021",
                    SparepartName = "Kim May Dày",
                    Description = "Kim chuyên dụng cho vải dày, da, nỉ",
                    Specification = "Cỡ 100/16, loại DPX17",
                    StockQuantity = 80,
                    Unit = "Cái",
                    UnitPrice = 6000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000022"),
                    SparepartCode = "SP022",
                    SparepartName = "Dây Curoa Dự Phòng",
                    Description = "Loại dây curoa dự phòng cho máy lập trình",
                    Specification = "Bản rộng 8mm, răng hình thang",
                    StockQuantity = 40,
                    Unit = "Cái",
                    UnitPrice = 15000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000023"),
                    SparepartCode = "SP023",
                    SparepartName = "Đèn Chiếu Sáng Máy May",
                    Description = "Loại đèn LED gắn bên cạnh trục kim",
                    Specification = "LED trắng 6W, 220V",
                    StockQuantity = 100,
                    Unit = "Cái",
                    UnitPrice = 20000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000024"),
                    SparepartCode = "SP024",
                    SparepartName = "Bộ Điều Khiển Chân Vịt",
                    Description = "Cơ cấu điều khiển chân vịt tự động",
                    Specification = "Tích hợp cảm biến áp suất",
                    StockQuantity = 18,
                    Unit = "Bộ",
                    UnitPrice = 95000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000025"),
                    SparepartCode = "SP025",
                    SparepartName = "Ổ Chỉ Dưới (Loại A)",
                    Description = "Ổ chỉ dưới thay thế cho máy Brother",
                    Specification = "Chuẩn A, có lò xo giữ",
                    StockQuantity = 75,
                    Unit = "Cái",
                    UnitPrice = 11000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000026"),
                    SparepartCode = "SP026",
                    SparepartName = "Trục Quay Bàn Đạp",
                    Description = "Thanh truyền động từ bàn đạp đến mô tơ",
                    Specification = "Thép đặc, dài 30cm",
                    StockQuantity = 25,
                    Unit = "Cái",
                    UnitPrice = 27000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000027"),
                    SparepartCode = "SP027",
                    SparepartName = "Đế Máy May",
                    Description = "Đế cao su chống trượt cho máy may",
                    Specification = "4 miếng/bộ, cao su EPDM",
                    StockQuantity = 90,
                    Unit = "Bộ",
                    UnitPrice = 18000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000028"),
                    SparepartCode = "SP028",
                    SparepartName = "Khung Gắn Đèn",
                    Description = "Giá đỡ đèn LED trên thân máy",
                    Specification = "Inox không gỉ",
                    StockQuantity = 40,
                    Unit = "Cái",
                    UnitPrice = 9000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000029"),
                    SparepartCode = "SP029",
                    SparepartName = "Bộ Điều Khiển Điện Tử",
                    Description = "Bo mạch điều khiển trung tâm cho máy điện tử",
                    Specification = "Mainboard 8-bit MCU",
                    StockQuantity = 5,
                    Unit = "Bộ",
                    UnitPrice = 350000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Sparepart
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000030"),
                    SparepartCode = "SP030",
                    SparepartName = "Giá Đỡ Chỉ Đứng",
                    Description = "Khung gắn chỉ đứng dùng cho máy công nghiệp",
                    Specification = "2 trục, cao 60cm",
                    StockQuantity = 55,
                    Unit = "Cái",
                    UnitPrice = 13000,
                    SupplierId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
