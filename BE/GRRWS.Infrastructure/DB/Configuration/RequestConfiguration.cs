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
                    Id = Guid.Parse("a1f2e3d4-0007-0007-1007-000000000007"),
                    RequestTitle = "SXB-B05-2-4U5V6",
                    Description = "Bộ phận cấp liệu bị lỗi tại Khu vực May Nặng, Vị trí 5. Ảnh hưởng đến sản xuất vải dày.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 31, 17, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0009-0009-1009-000000000009"),
                    RequestTitle = "SXA-A01-3-8C9D4",
                    Description = "Động cơ ngừng hoạt động tại Dây chuyền May A, Vị trí 3. Quan trọng cho sản xuất vải cotton.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 22, 17, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0010-0010-1010-000000000010"),
                    RequestTitle = "SXA-A02-1-2E5F6",
                    Description = "Kim bị kẹt tại Dây chuyền May B, Vị trí 1. Ảnh hưởng đến sản xuất vải dày.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 24, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0012-0012-1012-000000000012"),
                    RequestTitle = "SXB-B01-1-1J2K3",
                    Description = "Bộ phận cấp liệu khác biệt bị trục trặc tại Khu vực Vắt Sổ, Vị trí 1. Ảnh hưởng đến hoàn thiện vải mỏng.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 22, 17, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0013-0013-1013-000000000013"),
                    RequestTitle = "SXB-B02-1-4L5M6",
                    Description = "Nguồn điện bị gián đoạn tại Khu vực May Nặng, Vị trí 1. Ảnh hưởng đến sản xuất vải denim.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 23, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0015-0015-1015-000000000015"),
                    RequestTitle = "SXA-A03-3-2Q3R4",
                    Description = "Căng chỉ không đúng tại Dây chuyền May C, Vị trí 3. Ảnh hưởng đến chất lượng mũi may.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 18, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(3),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0016-0016-1016-000000000016"),
                    RequestTitle = "SXA-A04-1-5S6T7",
                    Description = "Máy đang sửa chữa cần thay động cơ. Hiện không được gán vị trí.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 26, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(1),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0018-0018-1018-000000000018"),
                    RequestTitle = "SXB-B01-2-1W2X3",
                    Description = "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 2 cần bôi trơn để ngăn mòn.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 27, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0020-0020-1020-000000000020"),
                    RequestTitle = "KTV-TV1-2-7A8B9",
                    Description = "Máy đang sửa chữa cần thay cơ chế chân vịt. Hiện không được gán vị trí.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 25, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(2),
                    IsDeleted = false
                },
                new Request
                {
                    Id = Guid.Parse("a1f2e3d4-0022-0022-1022-000000000022"),
                    RequestTitle = "SXB-B01-3-3E4F5",
                    Description = "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 3 cần vệ sinh để loại bỏ bụi vải tích tụ.",
                    Status = Domain.Enum.Status.Pending,
                    Priority = Domain.Enum.Priority.Low,
                    DueDate = new DateTime(2025, 5, 17, 14, 0, 0, DateTimeKind.Utc),
                    DeviceId = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    RequestedById = Guid.Parse("23333333-3333-3333-3333-333333333344"),
                    ReportId = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow.AddHours(3),
                    IsDeleted = false
                }
            );
        }
    }
}