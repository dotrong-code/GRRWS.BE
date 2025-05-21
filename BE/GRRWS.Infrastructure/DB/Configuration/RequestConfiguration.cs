using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            // Sample data (10 records)
            builder.HasData(
                new Request
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                    RequestTitle = "Repair Sewing Machine 1",
                    Description = "Sewing machine overheating issue.",
                    Status = "Pending",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = "High",
                    DeviceId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222233"),
                    RequestTitle = "Fix Needle Breakage",
                    Description = "Needle broke during operation.",
                    Status = "Approved",
                    DueDate = DateTime.UtcNow.AddDays(2),
                    Priority = "Medium",
                    DeviceId = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333344"),
                    RequestTitle = "Machine Not Starting",
                    Description = "Machine fails to start.",
                    Status = "Pending",
                    DueDate = DateTime.UtcNow.AddDays(4),
                    Priority = "High",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444455"),
                    RequestTitle = "Oil Leak Repair",
                    Description = "Oil leaking from machine.",
                    Status = "Denied",
                    DueDate = DateTime.UtcNow.AddDays(5),
                    Priority = "High",
                    DeviceId = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555566"),
                    RequestTitle = "Noise Issue",
                    Description = "Machine making loud noise.",
                    Status = "Pending",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = "Medium",
                    DeviceId = Guid.Parse("55555555-5555-5555-5555-555555555556"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666677"),
                    RequestTitle = "Thread Tension Adjustment",
                    Description = "Thread tension causing uneven stitches.",
                    Status = "Approved",
                    DueDate = DateTime.UtcNow.AddDays(2),
                    Priority = "Medium",
                    DeviceId = Guid.Parse("66666666-6666-6666-6666-666666666667"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777788"),
                    RequestTitle = "Bobbin Issue",
                    Description = "Bobbin not winding properly.",
                    Status = "Pending",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = "Low",
                    DeviceId = Guid.Parse("77777777-7777-7777-7777-777777777778"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888899"),
                    RequestTitle = "Slow Machine Operation",
                    Description = "Machine running slower than usual.",
                    Status = "Approved",
                    DueDate = DateTime.UtcNow.AddDays(4),
                    Priority = "Medium",
                    DeviceId = Guid.Parse("88888888-8888-8888-8888-888888888889"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999991010"),
                    RequestTitle = "Stitch Irregularity",
                    Description = "Irregular stitches detected.",
                    Status = "Pending",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = "Medium",
                    DeviceId = Guid.Parse("99999999-9999-9999-9999-999999999910"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Request
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaabb"),
                    RequestTitle = "Foot Pedal Repair",
                    Description = "Foot pedal not responding.",
                    Status = "Approved",
                    DueDate = DateTime.UtcNow.AddDays(2),
                    Priority = "High",
                    DeviceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                    RequestedById = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    SerderId = Guid.Parse("43333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
