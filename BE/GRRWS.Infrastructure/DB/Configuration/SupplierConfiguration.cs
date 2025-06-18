using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s => s.SupplierName).IsRequired().HasMaxLength(200);

            builder.HasIndex(s => s.SupplierName).IsUnique().HasDatabaseName("IX_Suppliers_SupplierName");

            // Dữ liệu mẫu (bạn có thể thêm nhiều supplier thực tế vào đây)
            builder.HasData(
                new Supplier
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    SupplierName = "Công ty TNHH Thiết Bị May Việt Nhật",
                    Address = "123 Nguyễn Văn Linh, Quận 7, TP.HCM",
                    Phone = "028 1234 5678",
                    Email = "contact@vietnhatmay.vn",
                    LinkWeb = "https://vietnhatmay.vn",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Supplier
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    SupplierName = "Công ty CP Phụ Tùng May Minh Long",
                    Address = "456 Lý Thường Kiệt, Quận Tân Bình, TP.HCM",
                    Phone = "028 8765 4321",
                    Email = "support@minhlongspareparts.vn",
                    LinkWeb = "https://minhlongspareparts.vn",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
