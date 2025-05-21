using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasIndex(d => d.DeviceCode)
                   .IsUnique()
                   .HasDatabaseName("IX_Devices_DeviceCode");

            builder.HasData(
                // Devices for Juki DDL-8700 (10 devices)
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0001-0001-0001-000000000001"),
                    DeviceCode = "DEV001-JUKI-DDL8700-01",
                    DeviceName = "Juki DDL-8700 Unit 1",
                    SerialNumber = "J8700-D001",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 10),
                    InstallationDate = new DateTime(2020, 2, 1),
                    Description = "Single needle lockstitch device for lightweight fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_01.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0001-0001-0001-000000000001"), // Juki DDL-8700-01
                    PositionId = Guid.Parse("f1e2d3c4-0001-0001-0001-000000000001"), // Position in Sewing Line A, Index 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0002-0002-0002-000000000002"),
                    DeviceCode = "DEV002-JUKI-DDL8700-02",
                    DeviceName = "Juki DDL-8700 Unit 2",
                    SerialNumber = "J8700-D002",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 12),
                    InstallationDate = new DateTime(2020, 2, 3),
                    Description = "Single needle lockstitch device for medium-weight fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_02.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0002-0002-0002-000000000002"), // Juki DDL-8700-02
                    PositionId = Guid.Parse("f1e2d3c4-0002-0002-0002-000000000002"), // Position in Sewing Line A, Index 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0003-0003-0003-000000000003"),
                    DeviceCode = "DEV003-JUKI-DDL8700-03",
                    DeviceName = "Juki DDL-8700 Unit 3",
                    SerialNumber = "J8700-D003",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 15),
                    InstallationDate = new DateTime(2020, 2, 5),
                    Description = "Single needle lockstitch device, currently in repair.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_03.jpg",
                    Status = "InRepair",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0003-0003-0003-000000000003"), // Juki DDL-8700-03
                    PositionId = null, // Not assigned to any position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0004-0004-0004-000000000004"),
                    DeviceCode = "DEV004-JUKI-DDL8700-04",
                    DeviceName = "Juki DDL-8700 Unit 4",
                    SerialNumber = "J8700-D004",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 18),
                    InstallationDate = new DateTime(2020, 2, 7),
                    Description = "Single needle lockstitch device for cotton fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_04.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0004-0004-0004-000000000004"), // Juki DDL-8700-04
                    PositionId = Guid.Parse("f1e2d3c4-0003-0003-0003-000000000003"), // Position in Sewing Line A, Index 3
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0005-0005-0005-000000000005"),
                    DeviceCode = "DEV005-JUKI-DDL8700-05",
                    DeviceName = "Juki DDL-8700 Unit 5",
                    SerialNumber = "J8700-D005",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 20),
                    InstallationDate = new DateTime(2020, 2, 10),
                    Description = "Single needle lockstitch device for synthetic fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_05.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0005-0005-0005-000000000005"), // Juki DDL-8700-05
                    PositionId = Guid.Parse("f1e2d3c4-0004-0004-0004-000000000004"), // Position in Sewing Line A, Index 4
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0006-0006-0006-000000000006"),
                    DeviceCode = "DEV006-JUKI-DDL8700-06",
                    DeviceName = "Juki DDL-8700 Unit 6",
                    SerialNumber = "J8700-D006",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 22),
                    InstallationDate = new DateTime(2020, 2, 12),
                    Description = "Single needle lockstitch device, retired.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_06.jpg",
                    Status = "Retired",
                    IsUnderWarranty = false,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0006-0006-0006-000000000006"), // Juki DDL-8700-06
                    PositionId = null, // Not assigned to any position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0007-0007-0007-000000000007"),
                    DeviceCode = "DEV007-JUKI-DDL8700-07",
                    DeviceName = "Juki DDL-8700 Unit 7",
                    SerialNumber = "J8700-D007",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 25),
                    InstallationDate = new DateTime(2020, 2, 15),
                    Description = "Single needle lockstitch device for heavy fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_07.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0007-0007-0007-000000000007"), // Juki DDL-8700-07
                    PositionId = Guid.Parse("f1e2d3c4-0006-0006-0006-000000000006"), // Position in Sewing Line B, Index 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0008-0008-0008-000000000008"),
                    DeviceCode = "DEV008-JUKI-DDL8700-08",
                    DeviceName = "Juki DDL-8700 Unit 8",
                    SerialNumber = "J8700-D008",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 28),
                    InstallationDate = new DateTime(2020, 2, 18),
                    Description = "Single needle lockstitch device for thin fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_08.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0008-0008-0008-000000000008"), // Juki DDL-8700-08
                    PositionId = Guid.Parse("f1e2d3c4-0007-0007-0007-000000000007"), // Position in Sewing Line B, Index 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0009-0009-0009-000000000009"),
                    DeviceCode = "DEV009-JUKI-DDL8700-09",
                    DeviceName = "Juki DDL-8700 Unit 9",
                    SerialNumber = "J8700-D009",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 1, 30),
                    InstallationDate = new DateTime(2020, 2, 20),
                    Description = "Single needle lockstitch device for mixed fabrics.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_09.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0009-0009-0009-000000000009"), // Juki DDL-8700-09
                    PositionId = Guid.Parse("f1e2d3c4-0010-0010-0010-000000000010"), // Position in Sewing Line C, Index 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0010-0010-0010-000000000010"),
                    DeviceCode = "DEV010-JUKI-DDL8700-10",
                    DeviceName = "Juki DDL-8700 Unit 10",
                    SerialNumber = "J8700-D010",
                    Model = "DDL-8700",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 2, 1),
                    InstallationDate = new DateTime(2020, 2, 22),
                    Description = "Single needle lockstitch device for general use.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl8700_10.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5500 SPM, Stitch length: 5mm",
                    PurchasePrice = 15000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0010-0010-0010-000000000010"), // Juki DDL-8700-10
                    PositionId = Guid.Parse("f1e2d3c4-0011-0011-0011-000000000011"), // Position in Sewing Line C, Index 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Devices for Juki DDL-9000C (4 devices)
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0011-0011-0011-000000000011"),
                    DeviceCode = "DEV011-JUKI-DDL9000C-01",
                    DeviceName = "Juki DDL-9000C Unit 1",
                    SerialNumber = "J9000C-D001",
                    Model = "DDL-9000C",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2022, 2, 5),
                    InstallationDate = new DateTime(2022, 3, 1),
                    Description = "Digital lockstitch device with auto thread trimmer, unit 1.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl9000c_01.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer",
                    PurchasePrice = 20000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0011-0011-0011-000000000011"), // Juki DDL-9000C-01
                    PositionId = Guid.Parse("f1e2d3c4-0008-0008-0008-000000000008"), // Position in Sewing Line B, Index 3
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0012-0012-0012-000000000012"),
                    DeviceCode = "DEV012-JUKI-DDL9000C-02",
                    DeviceName = "Juki DDL-9000C Unit 2",
                    SerialNumber = "J9000C-D002",
                    Model = "DDL-9000C",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2022, 2, 7),
                    InstallationDate = new DateTime(2022, 3, 3),
                    Description = "Digital lockstitch device with auto thread trimmer, unit 2.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl9000c_02.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer",
                    PurchasePrice = 20000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0012-0012-0012-000000000012"), // Juki DDL-9000C-02
                    PositionId = Guid.Parse("f1e2d3c4-0012-0012-0012-000000000012"), // Position in Sewing Line C, Index 3
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0013-0013-0013-000000000013"),
                    DeviceCode = "DEV013-JUKI-DDL9000C-03",
                    DeviceName = "Juki DDL-9000C Unit 3",
                    SerialNumber = "J9000C-D003",
                    Model = "DDL-9000C",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2022, 2, 10),
                    InstallationDate = new DateTime(2022, 3, 5),
                    Description = "Digital lockstitch device with auto thread trimmer, in repair.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl9000c_03.jpg",
                    Status = "InRepair",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer",
                    PurchasePrice = 20000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0013-0013-0013-000000000013"), // Juki DDL-9000C-03
                    PositionId = null, // Not assigned to any position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0014-0014-0014-000000000014"),
                    DeviceCode = "DEV014-JUKI-DDL9000C-04",
                    DeviceName = "Juki DDL-9000C Unit 4",
                    SerialNumber = "J9000C-D004",
                    Model = "DDL-9000C",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2022, 2, 12),
                    InstallationDate = new DateTime(2022, 3, 7),
                    Description = "Digital lockstitch device with auto thread trimmer, unit 4.",
                    PhotoUrl = "https://example.com/photos/device_juki_ddl9000c_04.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 5000 SPM, Auto thread trimmer",
                    PurchasePrice = 20000000,
                    Supplier = "Juki Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0014-0014-0014-000000000014"), // Juki DDL-9000C-04
                    PositionId = Guid.Parse("f1e2d3c4-0013-0013-0013-000000000013"), // Position in Sewing Line C, Index 4
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Devices for Brother B957 (3 devices)
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0015-0015-0015-000000000015"),
                    DeviceCode = "DEV015-BROTHER-B957-01",
                    DeviceName = "Brother B957 Unit 1",
                    SerialNumber = "B957-D001",
                    Model = "B957",
                    Manufacturer = "Brother",
                    ManufactureDate = new DateTime(2019, 6, 1),
                    InstallationDate = new DateTime(2019, 7, 1),
                    Description = "Three-thread overlock device for lightweight fabrics.",
                    PhotoUrl = "https://example.com/photos/device_brother_b957_01.jpg",
                    Status = "Active",
                    IsUnderWarranty = false,
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0",
                    PurchasePrice = 12000000,
                    Supplier = "Brother Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0015-0015-0015-000000000015"), // Brother B957-01
                    PositionId = Guid.Parse("f1e2d3c4-0019-0019-0019-000000000019"), // Position in Overlock Section, Index 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0016-0016-0016-000000000016"),
                    DeviceCode = "DEV016-BROTHER-B957-02",
                    DeviceName = "Brother B957 Unit 2",
                    SerialNumber = "B957-D002",
                    Model = "B957",
                    Manufacturer = "Brother",
                    ManufactureDate = new DateTime(2019, 6, 3),
                    InstallationDate = new DateTime(2019, 7, 3),
                    Description = "Three-thread overlock device for synthetic fabrics.",
                    PhotoUrl = "https://example.com/photos/device_brother_b957_02.jpg",
                    Status = "Active",
                    IsUnderWarranty = false,
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0",
                    PurchasePrice = 12000000,
                    Supplier = "Brother Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0016-0016-0016-000000000016"), // Brother B957-02
                    PositionId = Guid.Parse("f1e2d3c4-0020-0020-0020-000000000020"), // Position in Overlock Section, Index 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0017-0017-0017-000000000017"),
                    DeviceCode = "DEV017-BROTHER-B957-03",
                    DeviceName = "Brother B957 Unit 3",
                    SerialNumber = "B957-D003",
                    Model = "B957",
                    Manufacturer = "Brother",
                    ManufactureDate = new DateTime(2019, 6, 5),
                    InstallationDate = new DateTime(2019, 7, 5),
                    Description = "Three-thread overlock device for thin materials.",
                    PhotoUrl = "https://example.com/photos/device_brother_b957_03.jpg",
                    Status = "Active",
                    IsUnderWarranty = false,
                    Specifications = "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0",
                    PurchasePrice = 12000000,
                    Supplier = "Brother Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0017-0017-0017-000000000017"), // Brother B957-03
                    PositionId = Guid.Parse("f1e2d3c4-0021-0021-0021-000000000021"), // Position in Overlock Section, Index 3
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                // Devices for Singer 4452 (3 devices)
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0018-0018-0018-000000000018"),
                    DeviceCode = "DEV018-SINGER-4452-01",
                    DeviceName = "Singer 4452 Unit 1",
                    SerialNumber = "S4452-D001",
                    Model = "4452",
                    Manufacturer = "Singer",
                    ManufactureDate = new DateTime(2021, 3, 10),
                    InstallationDate = new DateTime(2021, 4, 1),
                    Description = "Heavy-duty device for denim fabrics.",
                    PhotoUrl = "https://example.com/photos/device_singer_4452_01.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm",
                    PurchasePrice = 18000000,
                    Supplier = "Singer Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0018-0018-0018-000000000018"), // Singer 4452-01
                    PositionId = Guid.Parse("f1e2d3c4-0023-0023-0023-000000000023"), // Position in Heavy Duty Stitching Zone, Index 1
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0019-0019-0019-000000000019"),
                    DeviceCode = "DEV019-SINGER-4452-02",
                    DeviceName = "Singer 4452 Unit 2",
                    SerialNumber = "S4452-D002",
                    Model = "4452",
                    Manufacturer = "Singer",
                    ManufactureDate = new DateTime(2021, 3, 12),
                    InstallationDate = new DateTime(2021, 4, 3),
                    Description = "Heavy-duty device for leather fabrics.",
                    PhotoUrl = "https://example.com/photos/device_singer_4452_02.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm",
                    PurchasePrice = 18000000,
                    Supplier = "Singer Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0019-0019-0019-000000000019"), // Singer 4452-02
                    PositionId = Guid.Parse("f1e2d3c4-0024-0024-0024-000000000024"), // Position in Heavy Duty Stitching Zone, Index 2
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("d1e2f3a4-0020-0020-0020-000000000020"),
                    DeviceCode = "DEV020-SINGER-4452-03",
                    DeviceName = "Singer 4452 Unit 3",
                    SerialNumber = "S4452-D003",
                    Model = "4452",
                    Manufacturer = "Singer",
                    ManufactureDate = new DateTime(2021, 3, 15),
                    InstallationDate = new DateTime(2021, 4, 5),
                    Description = "Heavy-duty device for canvas, in repair.",
                    PhotoUrl = "https://example.com/photos/device_singer_4452_03.jpg",
                    Status = "InRepair",
                    IsUnderWarranty = true,
                    Specifications = "Max speed: 1100 SPM, Presser foot lift: 6mm",
                    PurchasePrice = 18000000,
                    Supplier = "Singer Vietnam",
                    MachineId = Guid.Parse("a1b2c3d4-0020-0020-0020-000000000020"), // Singer 4452-03
                    PositionId = null, // Not assigned to any position
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}