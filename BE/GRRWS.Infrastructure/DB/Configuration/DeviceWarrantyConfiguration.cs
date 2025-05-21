using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceWarrantyConfiguration : IEntityTypeConfiguration<DeviceWarranty>
    {
        public void Configure(EntityTypeBuilder<DeviceWarranty> builder)
        {
            builder.HasData(
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0001-4001-8001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "Máy mới",
                    WarrantyStartDate = new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2022, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-001",
                    Notes = "Bảo hành định kỳ cho máy mới, bao gồm kiểm tra cơ chế căng chỉ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_01.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0002-4002-8002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    Status = "Pending",
                    WarrantyType = "Extended",
                    WarrantyReason = "Sau sửa chữa",
                    WarrantyStartDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-002",
                    Notes = "Gia hạn bảo hành sau sửa chữa động cơ bị cháy",
                    Cost = 500000,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_03.pdf",
                    SparePartCode = "SP007",
                    SparePartName = "Mô Tơ Máy May",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0003-4003-8003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "Sau thay thế",
                    WarrantyStartDate = new DateTime(2024, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2025, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-003",
                    Notes = "Bảo hành sau khi thay thế động cơ do hỏng hóc",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_04.pdf",
                    SparePartCode = "SP007",
                    SparePartName = "Mô Tơ Máy May",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0004-4004-8004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "Máy mới",
                    WarrantyStartDate = new DateTime(2020, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2022, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-004",
                    Notes = "Bảo hành máy mới, kiểm tra và sửa lỗi kẹt kim",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_07.pdf",
                    SparePartCode = "SP009",
                    SparePartName = "Trụ Gắn Kim",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0005-4005-8005-000000000005"),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "Máy mới",
                    WarrantyStartDate = new DateTime(2022, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-005",
                    Notes = "Bảo hành máy mới, hiệu chỉnh hệ thống cắt chỉ tự động",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl9000c_01.pdf",
                    SparePartCode = "SP029",
                    SparePartName = "Bộ Điều Khiển Điện Tử",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0006-4006-8006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    Status = "Pending",
                    WarrantyType = "Extended",
                    WarrantyReason = "Sau sửa chữa",
                    WarrantyStartDate = new DateTime(2025, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2026, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-006",
                    Notes = "Gia hạn bảo hành sau sửa chữa bộ phận cắt chỉ",
                    Cost = 500000,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl9000c_03.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0007-4007-8007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    Status = "Completed",
                    WarrantyType = "ThirdParty",
                    WarrantyReason = "Sau thay thế",
                    WarrantyStartDate = new DateTime(2024, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Brother Vietnam",
                    WarrantyCode = "WAR-BROTHER-001",
                    Notes = "Bảo hành bên thứ ba sau thay thế bộ phận cấp liệu khác biệt",
                    Cost = 1000000,
                    DocumentUrl = "https://example.com/docs/warranty_brother_b957_01.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0008-4008-8008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "Máy mới",
                    WarrantyStartDate = new DateTime(2021, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    WarrantyEndDate = new DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    Provider = "Singer Vietnam",
                    WarrantyCode = "WAR-SINGER-001",
                    Notes = "Bảo hành máy mới, sửa chữa bo mạch nguồn bị lỗi",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_singer_4452_01.pdf",
                    SparePartCode = "SP029",
                    SparePartName = "Bộ Điều Khiển Điện Tử",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            );
        }
    }
}