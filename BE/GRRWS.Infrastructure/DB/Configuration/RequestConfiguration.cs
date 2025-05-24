using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasData(
                new Request
                {
                    Id = Guid.Parse("a1e2f3a4-0001-0001-1001-000000000001"),
                    RequestTitle = "SXA-A01-1-4A7B2",
                    Description = "Máy ngừng hoạt động do đứt chỉ tại Dây chuyền May A, Vị trí 1, làm gián đoạn sản xuất vải mỏng.",
                    Status = "Pending",
                    Priority = "High",
                    DueDate = new DateTime(2025, 5, 23, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0002-0002-1002-000000000002"),
                    RequestTitle = "SXA-A01-3-8C9D4",
                    Description = "Động cơ ngừng hoạt động tại Dây chuyền May A, Vị trí 3. Quan trọng cho sản xuất vải cotton.",
                    Status = "Approved",
                    Priority = "High",
                    DueDate = new DateTime(2025, 5, 22, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = Guid.Parse("e1f2a3b4-0001-0001-0001-300000000001"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0003-0003-1003-000000000003"),
                    RequestTitle = "SXA-A02-1-2E5F6",
                    Description = "Kim bị kẹt tại Dây chuyền May B, Vị trí 1. Ảnh hưởng đến sản xuất vải dày.",
                    Status = "InProgress",
                    Priority = "Medium",
                    DueDate = new DateTime(2025, 5, 24, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = Guid.Parse("e1f2a3b4-0002-0002-0002-300000000002"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0004-0004-1004-000000000004"),
                    RequestTitle = "SXA-A02-3-7G8H9",
                    Description = "Máy cắt chỉ tự động bị lệch tại Dây chuyền May B, Vị trí 3. Gây ra mũi may không đều.",
                    Status = "Pending",
                    Priority = "Medium",
                    DueDate = new DateTime(2025, 5, 25, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0005-0005-1005-000000000005"),
                    RequestTitle = "SXB-B01-1-1J2K3",
                    Description = "Bộ phận cấp liệu khác biệt bị trục trặc tại Khu vực Vắt Sổ, Vị trí 1. Ảnh hưởng đến hoàn thiện vải mỏng.",
                    Status = "Approved",
                    Priority = "High",
                    DueDate = new DateTime(2025, 5, 22, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = Guid.Parse("e1f2a3b4-0003-0003-0003-300000000003"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0006-0006-1006-000000000006"),
                    RequestTitle = "SXB-B02-1-4L5M6",
                    Description = "Nguồn điện bị gián đoạn tại Khu vực May Nặng, Vị trí 1. Ảnh hưởng đến sản xuất vải denim.",
                    Status = "InProgress",
                    Priority = "High",
                    DueDate = new DateTime(2025, 5, 23, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = Guid.Parse("e1f2a3b4-0004-0004-0004-300000000004"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0007-0007-1007-000000000007"),
                    RequestTitle = "SXA-A03-1-8N9P0",
                    Description = "Cần bảo trì định kỳ cho Juki DDL-8700 tại Dây chuyền May C, Vị trí 1 để ngăn ngừa sự cố.",
                    Status = "Pending",
                    Priority = "Low",
                    DueDate = new DateTime(2025, 5, 28, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0008-0008-1008-000000000008"),
                    RequestTitle = "SXA-A03-3-2Q3R4",
                    Description = "Căng chỉ không đúng tại Dây chuyền May C, Vị trí 3. Ảnh hưởng đến chất lượng mũi may.",
                    Status = "Completed",
                    Priority = "Medium",
                    DueDate = new DateTime(2025, 5, 18, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = Guid.Parse("e1f2a3b4-0005-0005-0005-300000000005"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(3),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"),
                    RequestTitle = "SXA-A04-1-5S6T7",
                    Description = "Máy đang sửa chữa cần thay động cơ. Hiện không được gán vị trí.",
                    Status = "InProgress",
                    Priority = "Medium",
                    DueDate = new DateTime(2025, 5, 26, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0010-0010-1010-000000000010"),
                    RequestTitle = "SXB-B02-2-8U9V0",
                    Description = "Dây đai truyền động bị trượt tại Khu vực May Nặng, Vị trí 2. Ảnh hưởng đến sản xuất da.",
                    Status = "Pending",
                    Priority = "High",
                    DueDate = new DateTime(2025, 5, 23, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0011-0011-1011-000000000011"),
                    RequestTitle = "SXB-B01-2-1W2X3",
                    Description = "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 2 cần bôi trơn để ngăn mòn.",
                    Status = "Approved",
                    Priority = "Low",
                    DueDate = new DateTime(2025, 5, 27, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = Guid.Parse("e1f2a3b4-0007-0007-0007-300000000007"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0012-0012-1012-000000000012"),
                    RequestTitle = "SXA-A03-4-4Y5Z6",
                    Description = "Hệ thống điều khiển số cần cập nhật phần mềm tại Dây chuyền May C, Vị trí 4 để tối ưu hiệu suất.",
                    Status = "Pending",
                    Priority = "Low",
                    DueDate = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"),
                    RequestTitle = "KTV-TV1-2-7A8B9",
                    Description = "Máy đang sửa chữa cần thay cơ chế chân vịt. Hiện không được gán vị trí.",
                    Status = "Approved",
                    Priority = "Medium",
                    DueDate = new DateTime(2025, 5, 25, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0014-0014-1014-000000000014"),
                    RequestTitle = "SXA-A02-2-0C1D2",
                    Description = "Tiếng ồn lạ từ máy tại Dây chuyền May B, Vị trí 2. Có thể do vấn đề ổ bi.",
                    Status = "Denied",
                    Priority = "Medium",
                    DueDate = null,
                    DeviceId = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(3),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"),
                    RequestTitle = "SXB-B01-3-3E4F5",
                    Description = "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 3 cần vệ sinh để loại bỏ bụi vải tích tụ.",
                    Status = "Completed",
                    Priority = "Low",
                    DueDate = new DateTime(2025, 5, 17, 0, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(3),
                    IsDeleted = false
                }
            );
        }
    }
}