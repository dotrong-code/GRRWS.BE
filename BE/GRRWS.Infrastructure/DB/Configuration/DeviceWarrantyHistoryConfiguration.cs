using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceWarrantyHistoryConfiguration : IEntityTypeConfiguration<DeviceWarrantyHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceWarrantyHistory> builder)
        {
            builder.HasData(
                // Juki DDL-8700 Unit 1 (2 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1001-1001-1001-000000000001"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao cho vải nhẹ",
                    Status = true,
                    SendDate = new DateTime(2023, 6, 10, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2023, 6, 15, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member",
                    Note = "Kiểm tra và hiệu chỉnh hệ thống căng chỉ",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1002-1002-1002-000000000002"),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao cho vải nhẹ",
                    Status = true,
                    SendDate = new DateTime(2024, 7, 5, 9, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 7, 10, 15, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 2",
                    Note = "Sửa chữa bộ cấp liệu bị kẹt",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-8700 Unit 2 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1003-1003-1003-000000000003"),
                    DeviceId = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao cho vải trung bình",
                    Status = true,
                    SendDate = new DateTime(2024, 8, 15, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 8, 20, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 3",
                    Note = "Thay thế mô tơ máy may do lỗi tốc độ",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-8700 Unit 3 (2 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1004-1004-1004-000000000004"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2024, 6, 1, 9, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 6, 5, 15, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 4",
                    Note = "Sửa chữa động cơ bị cháy",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1005-1005-1005-000000000005"),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2025, 3, 10, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2025, 3, 15, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 5",
                    Note = "Kiểm tra và vệ sinh hệ thống kim may",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-8700 Unit 4 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1006-1006-1006-000000000006"),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    DeviceDescription = "Máy may kim đơn tốc độ cao cho vải cotton",
                    Status = true,
                    SendDate = new DateTime(2024, 12, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 12, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 6",
                    Note = "Thay thế động cơ do hỏng hóc",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-9000C Unit 1 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1007-1007-1007-000000000007"),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động",
                    Status = true,
                    SendDate = new DateTime(2023, 9, 10, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2023, 9, 15, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 7",
                    Note = "Hiệu chỉnh hệ thống cắt chỉ tự động",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-9000C Unit 3 (2 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1008-1008-1008-000000000008"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2024, 6, 15, 9, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 6, 20, 15, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 8",
                    Note = "Sửa chữa bộ phận cắt chỉ tự động",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1009-1009-1009-000000000009"),
                    DeviceId = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2025, 2, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2025, 2, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 9",
                    Note = "Kiểm tra và vệ sinh hệ thống cắt chỉ",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-9000C Unit 4 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1010-1010-1010-000000000010"),
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động",
                    Status = true,
                    SendDate = new DateTime(2024, 9, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 9, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 10",
                    Note = "Sửa chữa hệ thống cắt chỉ tự động",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4452 Unit 1 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1011-1011-1011-000000000011"),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    DeviceDescription = "Máy may nặng cho vải denim",
                    Status = true,
                    SendDate = new DateTime(2023, 10, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2023, 10, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member",
                    Note = "Sửa chữa bo mạch nguồn bị lỗi",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4452 Unit 2 (2 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1012-1012-1012-000000000012"),
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    DeviceDescription = "Máy may nặng cho vải da",
                    Status = true,
                    SendDate = new DateTime(2024, 8, 10, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 8, 15, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 2",
                    Note = "Kiểm tra và hiệu chỉnh hệ thống may nặng",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1013-1013-1013-000000000013"),
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    DeviceDescription = "Máy may nặng cho vải da",
                    Status = true,
                    SendDate = new DateTime(2025, 1, 5, 9, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2025, 1, 10, 15, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 3",
                    Note = "Thay thế chân vịt do mòn",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4452 Unit 3 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1014-1014-1014-000000000014"),
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    DeviceDescription = "Máy may nặng cho vải canvas, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2024, 7, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 7, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 4",
                    Note = "Sửa chữa hệ thống may nặng do kẹt",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki MO-6714S Unit 1 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1015-1015-1015-000000000015"),
                    DeviceId = Guid.Parse("d1e2f3a4-0021-0021-0021-000000000021"),
                    DeviceDescription = "Máy vắt sổ 4 chỉ tốc độ cao cho vải cotton và tổng hợp",
                    Status = true,
                    SendDate = new DateTime(2024, 9, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 9, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 5",
                    Note = "Sửa chữa bộ cấp liệu bị kẹt",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki MO-6714S Unit 2 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1016-1016-1016-000000000016"),
                    DeviceId = Guid.Parse("d1e2f3a4-0022-0022-0022-000000000022"),
                    DeviceDescription = "Máy vắt sổ 4 chỉ tốc độ cao cho vải nhẹ, đang sửa chữa",
                    Status = true,
                    SendDate = new DateTime(2024, 8, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 8, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 6",
                    Note = "Kiểm tra và vệ sinh hệ thống vắt sổ",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Brother S-7200C Unit 1 (2 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1017-1017-1017-000000000017"),
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động cho vải trung bình",
                    Status = true,
                    SendDate = new DateTime(2024, 10, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 10, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 7",
                    Note = "Hiệu chỉnh hệ thống cắt chỉ tự động",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1018-1018-1018-000000000018"),
                    DeviceId = Guid.Parse("d1e2f3a4-0023-0023-0023-000000000023"),
                    DeviceDescription = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động cho vải trung bình",
                    Status = true,
                    SendDate = new DateTime(2025, 3, 1, 9, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2025, 3, 5, 15, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 8",
                    Note = "Thay thế bộ điều khiển điện tử",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4423 Unit 1 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1019-1019-1019-000000000019"),
                    DeviceId = Guid.Parse("d1e2f3a4-0025-0025-0025-000000000025"),
                    DeviceDescription = "Máy may nặng cho vải denim và canvas",
                    Status = true,
                    SendDate = new DateTime(2024, 11, 1, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 11, 5, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 9",
                    Note = "Kiểm tra và hiệu chỉnh hệ thống may nặng",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki LH-3568S Unit 1 (1 lịch sử)
                new DeviceWarrantyHistory
                {
                    Id = Guid.Parse("e1e2f3a4-1020-1020-1020-000000000020"),
                    DeviceId = Guid.Parse("d1e2f3a4-0027-0027-0027-000000000027"),
                    DeviceDescription = "Máy may hai kim công nghiệp cho vải jeans",
                    Status = true,
                    SendDate = new DateTime(2024, 12, 10, 8, 0, 0, DateTimeKind.Utc),
                    ReceiveDate = new DateTime(2024, 12, 15, 14, 0, 0, DateTimeKind.Utc),
                    Provider = "Staff Member 10",
                    Note = "Kiểm tra và hiệu chỉnh hệ thống hai kim",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}