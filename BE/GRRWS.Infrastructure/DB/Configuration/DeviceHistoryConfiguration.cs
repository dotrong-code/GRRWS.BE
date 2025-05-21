using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceHistoryConfiguration : IEntityTypeConfiguration<DeviceHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceHistory> builder)
        {
            builder.HasData(
                // Record 1: Warranty for Juki DDL-8700 Unit 1 (Thread Break, Request 1)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0001-0001-0001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    ActionType = "Warranty",
                    EventDate = new DateTime(2023, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Bảo hành máy do lỗi đứt chỉ liên tục",
                    Reason = "Lỗi kỹ thuật từ nhà sản xuất trong cơ chế căng chỉ",
                    Provider = "Juki Vietnam",
                    Cost = 0,
                    ComponentCode = "TNS-001",
                    ComponentName = "Thread Tension Unit",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_01.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 2: Repair for Juki DDL-8700 Unit 3 (InRepair, Request 9)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0002-0002-0002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    ActionType = "Repair",
                    EventDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Sửa chữa máy do hỏng động cơ",
                    Reason = "Động cơ bị cháy do quá tải trong sản xuất",
                    Provider = "Juki Vietnam",
                    Cost = 1500000,
                    ComponentCode = "MTR-001",
                    ComponentName = "Motor",
                    Status = "Pending",
                    DocumentUrl = "https://example.com/docs/repair_juki_ddl8700_03.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 3: Replacement for Juki DDL-8700 Unit 4 (Motor Failure, Request 2)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0003-0003-0003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    ActionType = "Replacement",
                    EventDate = new DateTime(2024, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Thay thế động cơ cho máy Juki DDL-8700",
                    Reason = "Động cơ cũ bị hỏng do sử dụng lâu dài",
                    Provider = "Juki Vietnam",
                    Cost = 2000000,
                    ComponentCode = "MTR-002",
                    ComponentName = "Motor",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/replacement_juki_ddl8700_04.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 4: Warranty for Juki DDL-8700 Unit 7 (Needle Jam, Request 3)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0004-0004-0004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    ActionType = "Warranty",
                    EventDate = new DateTime(2022, 8, 10, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Bảo hành máy do kẹt kim",
                    Reason = "Lỗi cơ chế kim từ nhà sản xuất",
                    Provider = "Juki Vietnam",
                    Cost = 0,
                    ComponentCode = "NDL-001",
                    ComponentName = "Needle Bar",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl8700_07.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 5: Repair for Juki DDL-8700 Unit 8 (Noise Issue, Request 14)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0005-0005-0005-000000000005"),
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    ActionType = "Repair",
                    EventDate = new DateTime(2025, 4, 15, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Sửa chữa tiếng ồn bất thường từ máy",
                    Reason = "Ổ bi bị mòn do thiếu bôi trơn",
                    Provider = "Juki Vietnam",
                    Cost = 800000,
                    ComponentCode = "BRG-001",
                    ComponentName = "Bearing",
                    Status = "Failed",
                    DocumentUrl = "https://example.com/docs/repair_juki_ddl8700_08.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 6: Warranty for Juki DDL-9000C Unit 1 (Calibration Issue, Request 4)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0006-0006-0006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    ActionType = "Warranty",
                    EventDate = new DateTime(2023, 12, 5, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Bảo hành máy do lỗi hiệu chỉnh cắt chỉ tự động",
                    Reason = "Hệ thống điều khiển số bị lỗi từ nhà sản xuất",
                    Provider = "Juki Vietnam",
                    Cost = 0,
                    ComponentCode = "CTR-001",
                    ComponentName = "Control Unit",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/warranty_juki_ddl9000c_01.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 7: Repair for Juki DDL-9000C Unit 3 (InRepair)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0007-0007-0007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    ActionType = "Repair",
                    EventDate = new DateTime(2025, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Sửa chữa máy do lỗi hệ thống cắt chỉ",
                    Reason = "Bộ phận cắt chỉ bị kẹt do hao mòn",
                    Provider = "Juki Vietnam",
                    Cost = 1200000,
                    ComponentCode = "CTR-002",
                    ComponentName = "Thread Trimmer",
                    Status = "Pending",
                    DocumentUrl = "https://example.com/docs/repair_juki_ddl9000c_03.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 8: Replacement for Brother B957 Unit 1 (Overlock Issue, Request 5)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0008-0008-0008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    ActionType = "Replacement",
                    EventDate = new DateTime(2024, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Thay thế bộ phận cấp liệu khác biệt",
                    Reason = "Bộ phận cấp liệu bị hỏng do sử dụng sai",
                    Provider = "Brother Vietnam",
                    Cost = 1000000,
                    ComponentCode = "DFD-001",
                    ComponentName = "Differential Feed Dog",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/replacement_brother_b957_01.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 9: Warranty for Singer 4452 Unit 1 (Power Issue, Request 6)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0009-0009-0009-000000000009"),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    ActionType = "Warranty",
                    EventDate = new DateTime(2022, 10, 20, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Bảo hành máy do sự cố nguồn điện",
                    Reason = "Bo mạch nguồn bị lỗi từ nhà sản xuất",
                    Provider = "Singer Vietnam",
                    Cost = 0,
                    ComponentCode = "PWR-001",
                    ComponentName = "Power Board",
                    Status = "Completed",
                    DocumentUrl = "https://example.com/docs/warranty_singer_4452_01.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                // Record 10: Repair for Singer 4452 Unit 3 (InRepair, Request 13)
                new DeviceHistory
                {
                    Id = Guid.Parse("33cc4a77-0010-0010-0010-000000000010"),
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    ActionType = "Repair",
                    EventDate = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Sửa chữa máy do hỏng cơ chế chân vịt",
                    Reason = "Cơ chế chân vịt bị mòn do sử dụng lâu dài",
                    Provider = "Singer Vietnam",
                    Cost = 900000,
                    ComponentCode = "PFT-001",
                    ComponentName = "Presser Foot Mechanism",
                    Status = "Pending",
                    DocumentUrl = "https://example.com/docs/repair_singer_4452_03.pdf",
                    RelatedTaskId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            );
        }
    }
}