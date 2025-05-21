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

            // Seed data with 20 machine records
            builder.HasData(
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"),
                    MachineCode = "MC001-JUKI-DDL8700-01",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 1, for lightweight fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-001",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"),
                    MachineCode = "MC002-JUKI-DDL8700-02",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 2, for medium-weight fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-002",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"),
                    MachineCode = "MC003-JUKI-DDL8700-03",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 3, in maintenance.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-003",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"),
                    MachineCode = "MC004-JUKI-DDL8700-04",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 4, for cotton fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-004",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_04.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"),
                    MachineCode = "MC005-JUKI-DDL8700-05",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 5, for synthetic fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-005",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_05.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"),
                    MachineCode = "MC006-JUKI-DDL8700-06",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 6, retired unit.",
                    Status = "Retired",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-006",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_06.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"),
                    MachineCode = "MC007-JUKI-DDL8700-07",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 7, for heavy fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-007",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_07.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"),
                    MachineCode = "MC008-JUKI-DDL8700-08",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 8, for thin fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-008",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_08.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"),
                    MachineCode = "MC009-JUKI-DDL8700-09",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 9, for mixed fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-009",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_09.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0010-0010-0010-000000000010"),
                    MachineCode = "MC010-JUKI-DDL8700-10",
                    MachineName = "Industrial Sewing Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-8700",
                    Description = "High-speed single needle lockstitch sewing machine, unit 10, for general use.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2020, 1, 15),
                    Specifications = "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-010",
                    PhotoUrl = "https://example.com/photos/juki_ddl8700_10.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Juki DDL-9000C series (4 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0011-0011-0011-000000000011"),
                    MachineCode = "MC011-JUKI-DDL9000C-01",
                    MachineName = "Digital Lockstitch Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Digital lockstitch machine with auto thread trimmer, unit 1.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-001",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0012-0012-0012-000000000012"),
                    MachineCode = "MC012-JUKI-DDL9000C-02",
                    MachineName = "Digital Lockstitch Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Digital lockstitch machine with auto thread trimmer, unit 2.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-002",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0013-0013-0013-000000000013"),
                    MachineCode = "MC013-JUKI-DDL9000C-03",
                    MachineName = "Digital Lockstitch Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Digital lockstitch machine with auto thread trimmer, unit 3.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-003",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0014-0014-0014-000000000014"),
                    MachineCode = "MC014-JUKI-DDL9000C-04",
                    MachineName = "Digital Lockstitch Machine",
                    Manufacturer = "Juki",
                    Model = "DDL-9000C",
                    Description = "Digital lockstitch machine with auto thread trimmer, unit 4.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2022, 2, 10),
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-004",
                    PhotoUrl = "https://example.com/photos/juki_ddl9000c_04.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Brother B957 series (3 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0015-0015-0015-000000000015"),
                    MachineCode = "MC015-BROTHER-B957-01",
                    MachineName = "Overlock Machine",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Three-thread overlock sewing machine, unit 1, for lightweight fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-001",
                    PhotoUrl = "https://example.com/photos/brother_b957_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0016-0016-0016-000000000016"),
                    MachineCode = "MC016-BROTHER-B957-02",
                    MachineName = "Overlock Machine",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Three-thread overlock sewing machine, unit 2, for synthetic fabrics.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-002",
                    PhotoUrl = "https://example.com/photos/brother_b957_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0017-0017-0017-000000000017"),
                    MachineCode = "MC017-BROTHER-B957-03",
                    MachineName = "Overlock Machine",
                    Manufacturer = "Brother",
                    Model = "B957",
                    Description = "Three-thread overlock sewing machine, unit 3, for thin materials.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2019, 6, 10),
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-003",
                    PhotoUrl = "https://example.com/photos/brother_b957_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Singer 4452 series (3 machines)
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0018-0018-0018-000000000018"),
                    MachineCode = "MC018-SINGER-4452-01",
                    MachineName = "Heavy Duty Sewing Machine",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Heavy-duty machine for thick materials, unit 1, for denim.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-001",
                    PhotoUrl = "https://example.com/photos/singer_4452_01.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0019-0019-0019-000000000019"),
                    MachineCode = "MC019-SINGER-4452-02",
                    MachineName = "Heavy Duty Sewing Machine",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Heavy-duty machine for thick materials, unit 2, for leather.",
                    Status = "Active",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-002",
                    PhotoUrl = "https://example.com/photos/singer_4452_02.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Machine
                {
                    Id = Guid.Parse("a1b2c3d4-0020-0020-0020-000000000020"),
                    MachineCode = "MC020-SINGER-4452-03",
                    MachineName = "Heavy Duty Sewing Machine",
                    Manufacturer = "Singer",
                    Model = "4452",
                    Description = "Heavy-duty machine for thick materials, unit 3, for canvas.",
                    Status = "InRepair",
                    ReleaseDate = new DateTime(2021, 3, 20),
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-003",
                    PhotoUrl = "https://example.com/photos/singer_4452_03.jpg",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}