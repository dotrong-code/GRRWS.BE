using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class MachineSparepartConfiguration : IEntityTypeConfiguration<MachineSparepart>
    {
        public void Configure(EntityTypeBuilder<MachineSparepart> builder)
        {
            // Thiết lập composite key
            builder.HasKey(ms => new { ms.MachineId, ms.SparepartId });

            // Thiết lập quan hệ Machine -> MachineSparepart
            builder.HasOne(ms => ms.Machine)
                   .WithMany(m => m.MachineSpareparts)
                   .HasForeignKey(ms => ms.MachineId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Thiết lập quan hệ Sparepart -> MachineSparepart
            builder.HasOne(ms => ms.Sparepart)
                   .WithMany(sp => sp.MachineSpareparts)
                   .HasForeignKey(ms => ms.SparepartId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Seed data (Demo mapping 10 linh kiện với các máy có sẵn)
            builder.HasData(
                // JUKI-DDL8700 (MC001) -> SP001, SP002, SP003
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000001")
                },
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000002")
                },
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000003")
                },

                // JUKI-DDL9000C (MC002) -> SP004, SP005
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000004")
                },
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000005")
                },

                // BROTHER-B957 (MC003) -> SP006, SP007
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000006")
                },
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000007")
                },

                // SINGER-4452 (MC004) -> SP008, SP009
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000008")
                },
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000009")
                },

                // BROTHER-S7200C (MC006) -> SP010
                new MachineSparepart
                {
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000010")
                }
            );
        }
    }
}
