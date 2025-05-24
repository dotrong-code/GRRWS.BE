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
                // Device 1: Juki DDL-8700 Unit 1 (3 warranties)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0001-4001-8001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 1),
                    WarrantyEndDate = new DateTime(2022, 2, 1),
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
                    Id = Guid.Parse("d1e2f3a4-0009-4009-8009-000000000009"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    Status = "Completed",
                    WarrantyType = "Extended",
                    WarrantyReason = "AfterWarranty",
                    WarrantyStartDate = new DateTime(2022, 2, 2),
                    WarrantyEndDate = new DateTime(2023, 2, 2),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-009",
                    Notes = "Gia hạn bảo hành sau khi hết bảo hành nhà sản xuất",
                    Cost = 500000,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_01_ext.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0010-4010-8010-000000000010"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    Status = "InProgress",
                    WarrantyType = "ThirdParty",
                    WarrantyReason = "AfterRepair",
                    WarrantyStartDate = new DateTime(2024, 6, 1),
                    WarrantyEndDate = new DateTime(2025, 6, 1),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-010",
                    Notes = "Bảo hành sau sửa chữa bộ phận cấp liệu",
                    Cost = 800000,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_01_repair.pdf",
                    SparePartCode = "SP008",
                    SparePartName = "Bộ Cấp Liệu",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 2: Juki DDL-8700 Unit 2 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0011-4011-8011-000000000011"),
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 3),
                    WarrantyEndDate = new DateTime(2022, 2, 3),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-011",
                    Notes = "Bảo hành máy mới, kiểm tra hệ thống kim và chỉ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_02.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 3: Juki DDL-8700 Unit 3 (2 warranties)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0002-4002-8002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    Status = "Pending",
                    WarrantyType = "Extended",
                    WarrantyReason = "AfterRepair",
                    WarrantyStartDate = new DateTime(2025, 5, 10),
                    WarrantyEndDate = new DateTime(2026, 5, 10),
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
                    Id = Guid.Parse("d1e2f3a4-0012-4012-8012-000000000012"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 5),
                    WarrantyEndDate = new DateTime(2022, 2, 5),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-012",
                    Notes = "Bảo hành máy mới, kiểm tra động cơ và bộ căng chỉ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_03_new.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 4: Juki DDL-8700 Unit 4 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0003-4003-8003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "AfterReplacement",
                    WarrantyStartDate = new DateTime(2024, 11, 20),
                    WarrantyEndDate = new DateTime(2025, 11, 20),
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
                // Device 5: Juki DDL-8700 Unit 5 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0013-4013-8013-000000000013"),
                    DeviceId = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 10),
                    WarrantyEndDate = new DateTime(2022, 2, 10),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-013",
                    Notes = "Bảo hành máy mới, hiệu chỉnh hệ thống cấp liệu",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_05.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 6: Juki DDL-8700 Unit 6 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0014-4014-8014-000000000014"),
                    DeviceId = Guid.Parse("d1e2f3a4-0006-0006-0006-000000000006"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 12),
                    WarrantyEndDate = new DateTime(2022, 2, 12),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-014",
                    Notes = "Bảo hành máy mới, đã hết hạn trước khi máy ngừng sử dụng",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_06.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 7: Juki DDL-8700 Unit 7 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0004-4004-8004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 15),
                    WarrantyEndDate = new DateTime(2022, 2, 15),
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
                // Device 8: Juki DDL-8700 Unit 8 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0015-4015-8015-000000000015"),
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 18),
                    WarrantyEndDate = new DateTime(2022, 2, 18),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-015",
                    Notes = "Bảo hành máy mới, kiểm tra hệ thống căng chỉ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_08.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 9: Juki DDL-8700 Unit 9 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0016-4016-8016-000000000016"),
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 20),
                    WarrantyEndDate = new DateTime(2022, 2, 20),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-016",
                    Notes = "Bảo hành máy mới, hiệu chỉnh động cơ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_09.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 10: Juki DDL-8700 Unit 10 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0017-4017-8017-000000000017"),
                    DeviceId = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2020, 2, 22),
                    WarrantyEndDate = new DateTime(2022, 2, 22),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-017",
                    Notes = "Bảo hành máy mới, kiểm tra hệ thống cấp liệu",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_10.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 11: Juki DDL-9000C Unit 1 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0005-4005-8005-000000000005"),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2022, 3, 1),
                    WarrantyEndDate = new DateTime(2024, 3, 1),
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
                // Device 12: Juki DDL-9000C Unit 2 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0018-4018-8018-000000000018"),
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2022, 3, 3),
                    WarrantyEndDate = new DateTime(2024, 3, 3),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-018",
                    Notes = "Bảo hành máy mới, kiểm tra hệ thống cắt chỉ",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl9000c_02.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 13: Juki DDL-9000C Unit 3 (2 warranties)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0006-4006-8006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    Status = "Pending",
                    WarrantyType = "Extended",
                    WarrantyReason = "AfterRepair",
                    WarrantyStartDate = new DateTime(2025, 5, 12),
                    WarrantyEndDate = new DateTime(2026, 5, 12),
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
                    Id = Guid.Parse("d1e2f3a4-0019-4019-8019-000000000019"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2022, 3, 5),
                    WarrantyEndDate = new DateTime(2024, 3, 5),
                    Provider = "Juki Vietnam",
                    WarrantyCode = "WAR-JUKI-019",
                    Notes = "Bảo hành máy mới, hiệu chỉnh bộ cắt chỉ tự động",
                    Cost = 0,
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl9000c_03_new.pdf",
                    SparePartCode = null,
                    SparePartName = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Device 15: Brother B957 Unit 1 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0007-4007-8007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    Status = "Completed",
                    WarrantyType = "ThirdParty",
                    WarrantyReason = "AfterReplacement",
                    WarrantyStartDate = new DateTime(2024, 3, 10),
                    WarrantyEndDate = new DateTime(2025, 3, 10),
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
                // Device 18: Singer 4452 Unit 1 (1 warranty)
                new DeviceWarranty
                {
                    Id = Guid.Parse("d1e2f3a4-0008-4008-8008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    Status = "Completed",
                    WarrantyType = "Manufacturer",
                    WarrantyReason = "NewDevice",
                    WarrantyStartDate = new DateTime(2021, 4, 1),
                    WarrantyEndDate = new DateTime(2023, 4, 1),
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