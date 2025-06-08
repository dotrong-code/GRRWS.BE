using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class RequestTakeSparePartUsageConfiguration : IEntityTypeConfiguration<RequestTakeSparePartUsage>
    {
        public void Configure(EntityTypeBuilder<RequestTakeSparePartUsage> builder)
        {
            builder.HasData(

                new RequestTakeSparePartUsage
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                    RequestCode = "REQ-001",
                    RequestDate = DateTime.UtcNow,
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Status = SparePartRequestStatus.Unconfirmed,
                    ConfirmedDate = DateTime.UtcNow,
                    ConfirmedById = null,
                    Notes = "Lấy phụ tùng cho ErrorDetail 1"
                },

                new RequestTakeSparePartUsage
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                    RequestCode = "REQ-002",
                    RequestDate = DateTime.UtcNow,
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Status = SparePartRequestStatus.Unconfirmed,
                    ConfirmedDate = DateTime.UtcNow,
                    ConfirmedById = null,
                    Notes = "Lấy phụ tùng cho ErrorDetail 2"
                },

                new RequestTakeSparePartUsage
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000003"),
                    RequestCode = "REQ-003",
                    RequestDate = DateTime.UtcNow,
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Status = SparePartRequestStatus.Confirmed,
                    ConfirmedDate = DateTime.UtcNow,
                    ConfirmedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    Notes = "Lấy phụ tùng cho ErrorDetail 3"
                },

                new RequestTakeSparePartUsage
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000004"),
                    RequestCode = "REQ-004",
                    RequestDate = DateTime.UtcNow,
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Status = SparePartRequestStatus.Confirmed,
                    ConfirmedDate = DateTime.UtcNow,
                    ConfirmedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    Notes = "Lấy phụ tùng cho ErrorDetail 4"
                },

                new RequestTakeSparePartUsage
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000005"),
                    RequestCode = "REQ-005",
                    RequestDate = DateTime.UtcNow,
                    RequestedById = Guid.Parse("32222222-2222-2222-2222-222222222222"),
                    Status = SparePartRequestStatus.Delivered,
                    ConfirmedDate = DateTime.UtcNow,
                    ConfirmedById = Guid.Parse("23333333-3333-3333-3333-333333333343"),
                    Notes = "Lấy phụ tùng cho ErrorDetail 5"
                }
            );
        }
    }
}
