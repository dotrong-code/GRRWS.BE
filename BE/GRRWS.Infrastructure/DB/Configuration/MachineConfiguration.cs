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
                    MachineCode = "MC001-JUKI-DDL8700-01",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 1, dùng cho vải nhẹ.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-001",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    MachineCode = "MC002-JUKI-DDL8700-02",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 2, dùng cho vải trung bình.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-002",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    MachineCode = "MC003-JUKI-DDL8700-03",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 3, đang bảo trì.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-003",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    MachineCode = "MC004-JUKI-DDL8700-04",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 4, dùng cho vải cotton.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-004",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_04.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    MachineCode = "MC005-JUKI-DDL8700-05",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 5, dùng cho vải tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-005",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_05.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    MachineCode = "MC006-JUKI-DDL8700-06",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 6, đã ngừng sử dụng.",
                    Status = "Retired",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-006",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_06.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    MachineCode = "MC007-JUKI-DDL8700-07",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 7, dùng cho vải dày.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-007",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_07.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    MachineCode = "MC008-JUKI-DDL8700-08",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 8, dùng cho vải mỏng.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J87008",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_08.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    MachineCode = "MC009-JUKI-DDL8700-09",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 9, dùng cho vải hỗn hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-009",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_09.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0010-0010-0010-000000000010"),
                    MachineCode = "MC010-JUKI-DDL8700-10",
                    MachineName = "Máy May Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "Máy may kim đơn tốc độ cao, đơn vị 10, dùng cho mục đích chung.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-010",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_10.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-9000C series (4 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0011-0011-0011-000000000011"),
                    MachineCode = "MC011-JUKI-DDL9000C-01",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 1.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-001",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0012-0012-0012-000000000012"),
                    MachineCode = "MC012-JUKI-DDL9000C-02",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 2.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-002",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0013-0013-0013-000000000013"),
                    MachineCode = "MC013-JUKI-DDL9000C-03",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Máy may kim đơn kỹ thuật số với chức năng cắtρού chỉ tự động, đơn vị 3, đang bảo trì.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-003",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0014-0014-0014-000000000014"),
                    MachineCode = "MC014-JUKI-DDL9000C-04",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 4.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-004",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_04.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Brother B957 series (3 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0015-0015-0015-000000000015"),
                    MachineCode = "MC015-BROTHER-B957-01",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Máy vắt sổ 3 chỉ, đơn vị 1, dùng cho vải nhẹ.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-001",
                    PhotoUrl = "https://example.com/photos/brother_b957_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0016-0016-0016-000000000016"),
                    MachineCode = "MC016-BROTHER-B957-02",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Máy vắt sổ 3 chỉ, đơn vị 2, dùng cho vải tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-002",
                    PhotoUrl = "https://example.com/photos/brother_b957_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0017-0017-0017-000000000017"),
                    MachineCode = "MC017-BROTHER-B957-03",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Máy vắt sổ 3 chỉ, đơn vị 3, dùng cho vải mỏng.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-003",
                    PhotoUrl = "https://example.com/photos/brother_b957_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4452 series (3 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0018-0018-0018-000000000018"),
                    MachineCode = "MC018-SINGER-4452-01",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Máy may nặng cho vật liệu dày, đơn vị 1, dùng cho vải denim.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-001",
                    PhotoUrl = "https://example.com/photos/singer_4452_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0019-0019-0019-000000000019"),
                    MachineCode = "MC019-SINGER-4452-02",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Máy may nặng cho vật liệu dày, đơn vị 2, dùng cho da.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-002",
                    PhotoUrl = "https://example.com/photos/singer_4452_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0020-0020-0020-000000000020"),
                    MachineCode = "MC020-SINGER-4452-03",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Máy may nặng cho vật liệu dày, đơn vị 3, dùng cho vải canvas, đang bảo trì.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-003",
                    PhotoUrl = "https://example.com/photos/singer_4452_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Additional Machines (10 new records)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0021-0021-0021-000000000021"),
                    MachineCode = "MC021-JUKI-MO6714S-01",
                    MachineName = "Máy Vắt Sổ Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "MO-6714S",
                    Description = "Máy vắt sổ 4 chỉ tốc độ cao, đơn vị 1, dùng cho vải cotton và vải tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 5, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: MO6714S-001",
                    PhotoUrl = "https://example.com/photos/juki_mo6714s_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0022-0022-0022-000000000022"),
                    MachineCode = "MC022-JUKI-MO6714S-02",
                    MachineName = "Máy Vắt Sổ Công Nghiệp",
                    Manufacturer = "Juki",
                    Model = "MO-6714S",
                    Description = "Máy vắt sổ 4 chỉ tốc độ cao, đơn vị 2, dùng cho vải nhẹ, đang bảo trì.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2021, 5, 10),
                    Specifications = "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: MO6714S-002",
                    PhotoUrl = "https://example.com/photos/juki_mo6714s_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0023-0023-0023-000000000023"),
                    MachineCode = "MC023-BROTHER-S7200C-01",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Brother",
                    Model = "S-7200C",
                    Description = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 1, dùng cho vải trung bình.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 8, 15),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: S7200C-001",
                    PhotoUrl = "https://example.com/photos/brother_s7200c_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0024-0024-0024-000000000024"),
                    MachineCode = "MC024-BROTHER-S7200C-02",
                    MachineName = "Máy May Kim Đơn Kỹ Thuật Số",
                    Manufacturer = "Brother",
                    Model = "S-7200C",
                    Description = "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 2, dùng cho vải dày.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 8, 15),
                    Specifications = "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: S7200C-002",
                    PhotoUrl = "https://example.com/photos/brother_s7200c_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0025-0025-0025-000000000025"),
                    MachineCode = "MC025-SINGER-4423-01",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4423",
                    Description = "Máy may nặng cho vật liệu dày, đơn vị 1, dùng cho vải denim và canvas.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 10, 5),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4423-001",
                    PhotoUrl = "https://example.com/photos/singer_4423_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0026-0026-0026-000000000026"),
                    MachineCode = "MC026-SINGER-4423-02",
                    MachineName = "Máy May Nặng",
                    Manufacturer = "Singer",
                    Model = "4423",
                    Description = "Máy may nặng cho vật liệu dày, đơn vị 2, dùng cho da, đã ngừng sử dụng.",
                    Status = "Retired",
                    ReleaseDate = new DateTime(2020, 10, 5),
                    Specifications = "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4423-002",
                    PhotoUrl = "https://example.com/photos/singer_4423_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0027-0027-0027-000000000027"),
                    MachineCode = "MC027-JUKI-LH3568S-01",
                    MachineName = "Máy May Hai Kim",
                    Manufacturer = "Juki",
                    Model = "LH-3568S",
                    Description = "Máy may hai kim công nghiệp, đơn vị 1, dùng cho vải jeans.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 12, 1),
                    Specifications = "Tốc độ tối đa: 3000 SPM, Độ dài mũi may tối đa: 5mm, Serial: LH3568S-001",
                    PhotoUrl = "https://example.com/photos/juki_lh3568s_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0028-0028-0028-000000000028"),
                    MachineCode = "MC028-JUKI-LH3568S-02",
                    MachineName = "Máy May Hai Kim",
                    Manufacturer = "Juki",
                    Model = "LH-3568S",
                    Description = "Máy may hai kim công nghiệp, đơn vị 2, dùng cho vải dày, đang bảo trì.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2021, 12, 1),
                    Specifications = "Tốc độ tối đa: 3000 SPM, Độ dài mũi may tối đa: 5mm, Serial: LH3568S-002",
                    PhotoUrl = "https://example.com/photos/juki_lh3568s_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0029-0029-0029-000000000029"),
                    MachineCode = "MC029-BROTHER-B735-01",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B735",
                    Description = "Máy vắt sổ 3 chỉ, đơn vị 1, dùng cho vải mỏng và vải tổng hợp.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 4, 20),
                    Specifications = "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B735-001",
                    PhotoUrl = "https://example.com/photos/brother_b735_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0030-0030-0030-000000000030"),
                    MachineCode = "MC030-BROTHER-B735-02",
                    MachineName = "Máy Vắt Sổ",
                    Manufacturer = "Brother",
                    Model = "B735",
                    Description = "Máy vắt sổ 3 chỉ, đơn vị 2, dùng cho vải cotton, đã ngừng sử dụng.",
                    Status = "Retired",
                    ReleaseDate = new DateTime(2020, 4, 20),
                    Specifications = "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B735-002",
                    PhotoUrl = "https://example.com/photos/brother_b735_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}