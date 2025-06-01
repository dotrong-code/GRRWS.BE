using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.HasIndex(m => m.MachineCode)
                   .IsUnique()
                   .HasDatabaseName("IX_Machines_MachineCode");

            builder.HasData(
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    MachineCode = "MC001-JUKI-DDL8700",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, phù hợp cho vải nhẹ, trung bình, và dày.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    MachineCode = "MC002-JUKI-DDL9000C",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    MachineCode = "MC003-BROTHER-B957",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Máy vắt sổ 3 chỉ, phù hợp cho vải nhẹ và tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0",
                    PhotoUrl = "https://example.com/photos/brother_b957.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    MachineCode = "MC004-SINGER-4452",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Máy may nặng cho vật liệu dày như denim, da, và canvas.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm",
                    PhotoUrl = "https://example.com/photos/singer_4452.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    MachineCode = "MC005-JUKI-MO6714S",
                    MachineName = "Máy Vắt Sổ Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "MO-6714S",
                    Description = "Máy vắt sổ 4 chỉ tốc độ cao, phù hợp cho vải cotton và tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 5, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0",
                    PhotoUrl = "https://example.com/photos/juki_mo6714s.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    MachineCode = "MC006-BROTHER-S7200C",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Brother",
                    Model = "S-7200C",
                    Description = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, phù hợp cho vải trung bình và dày.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 8, 15),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động",
                    PhotoUrl = "https://example.com/photos/brother_s7200c.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    MachineCode = "MC007-SINGER-4423",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4423",
                    Description = "Máy may nặng cho vật liệu dày như denim và canvas.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 10, 5),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm",
                    PhotoUrl = "https://example.com/photos/singer_4423.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    MachineCode = "MC008-JUKI-LH3568S",
                    MachineName = "Máy May Hai Kim",
                    Manufacturer = "Juki",
                    Model = "LH-3568S",
                    Description = "Máy may hai kim công nghiệp, phù hợp cho vải jeans và vải dày.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 12, 1),
                    Specifications = "Tốc độ tối đa: 3000 SPM, Độ dài mũi may tối đa: 5mm",
                    PhotoUrl = "https://example.com/photos/juki_lh3568s.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    MachineCode = "MC009-BROTHER-B735",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B735",
                    Description = "Máy vắt sổ 3 chỉ, phù hợp cho vải mỏng và cotton.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 4, 20),
                    Specifications = "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0",
                    PhotoUrl = "https://example.com/photos/brother_b735.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}