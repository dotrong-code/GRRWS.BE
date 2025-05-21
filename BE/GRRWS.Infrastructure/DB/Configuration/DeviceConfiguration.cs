using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            // Configure index for DeviceCode
            builder.HasIndex(d => d.DeviceCode)
                   .HasDatabaseName("IX_Devices_DeviceCode")
                   .IsUnique();

            // Sample data (10 records)
            builder.HasData(
                new Device
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    DeviceName = "Sewing Machine 1",
                    DeviceCode = "SM001",
                    SerialNumber = "SN001",
                    Model = "Model A",
                    Manufacturer = "Brother",
                    ManufactureDate = new DateTime(2020, 1, 15),
                    InstallationDate = new DateTime(2020, 2, 1),
                    Description = "Industrial sewing machine for heavy fabrics",
                    PhotoUrl = "http://example.com/photos/sm001.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"speed\": \"1200 SPM\", \"type\": \"Industrial\"}",
                    PurchasePrice = 1500.00m,
                    Supplier = "Brother Industries",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    DeviceName = "Sewing Machine 2",
                    DeviceCode = "SM002",
                    SerialNumber = "SN002",
                    Model = "Model B",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2019, 6, 20),
                    InstallationDate = new DateTime(2019, 7, 10),
                    Description = "High-speed sewing machine",
                    PhotoUrl = "http://example.com/photos/sm002.jpg",
                    Status = "InRepair",
                    IsUnderWarranty = false,
                    Specifications = "{\"speed\": \"1500 SPM\", \"type\": \"High-Speed\"}",
                    PurchasePrice = 2000.00m,
                    Supplier = "Juki Corporation",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    DeviceName = "Cutting Machine 1",
                    DeviceCode = "CM001",
                    SerialNumber = "SN003",
                    Model = "Model C",
                    Manufacturer = "Pegasus",
                    ManufactureDate = new DateTime(2021, 3, 5),
                    InstallationDate = new DateTime(2021, 4, 1),
                    Description = "Fabric cutting machine",
                    PhotoUrl = "http://example.com/photos/cm001.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"blade\": \"10 inch\", \"type\": \"Rotary\"}",
                    PurchasePrice = 1200.00m,
                    Supplier = "Pegasus Ltd",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    DeviceName = "Embroidery Machine 1",
                    DeviceCode = "EM001",
                    SerialNumber = "SN004",
                    Model = "Model D",
                    Manufacturer = "Bernina",
                    ManufactureDate = new DateTime(2020, 8, 12),
                    InstallationDate = new DateTime(2020, 9, 1),
                    Description = "Computerized embroidery machine",
                    PhotoUrl = "http://example.com/photos/em001.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"stitches\": \"1000\", \"type\": \"Computerized\"}",
                    PurchasePrice = 2500.00m,
                    Supplier = "Bernina International",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555556"),
                    DeviceName = "Sewing Machine 3",
                    DeviceCode = "SM003",
                    SerialNumber = "SN005",
                    Model = "Model E",
                    Manufacturer = "Singer",
                    ManufactureDate = new DateTime(2018, 11, 25),
                    InstallationDate = new DateTime(2019, 1, 10),
                    Description = "Heavy-duty sewing machine",
                    PhotoUrl = "http://example.com/photos/sm003.jpg",
                    Status = "Retired",
                    IsUnderWarranty = false,
                    Specifications = "{\"speed\": \"1100 SPM\", \"type\": \"Heavy-Duty\"}",
                    PurchasePrice = 1800.00m,
                    Supplier = "Singer Corporation",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666667"),
                    DeviceName = "Sewing Machine 4",
                    DeviceCode = "SM004",
                    SerialNumber = "SN006",
                    Model = "Model F",
                    Manufacturer = "Brother",
                    ManufactureDate = new DateTime(2021, 5, 10),
                    InstallationDate = new DateTime(2021, 6, 1),
                    Description = "Compact sewing machine",
                    PhotoUrl = "http://example.com/photos/sm004.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"speed\": \"800 SPM\", \"type\": \"Compact\"}",
                    PurchasePrice = 1000.00m,
                    Supplier = "Brother Industries",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777778"),
                    DeviceName = "Cutting Machine 2",
                    DeviceCode = "CM002",
                    SerialNumber = "SN007",
                    Model = "Model G",
                    Manufacturer = "Juki",
                    ManufactureDate = new DateTime(2020, 12, 15),
                    InstallationDate = new DateTime(2021, 1, 5),
                    Description = "Automatic fabric cutting machine",
                    PhotoUrl = "http://example.com/photos/cm002.jpg",
                    Status = "InRepair",
                    IsUnderWarranty = false,
                    Specifications = "{\"blade\": \"12 inch\", \"type\": \"Automatic\"}",
                    PurchasePrice = 2200.00m,
                    Supplier = "Juki Corporation",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888889"),
                    DeviceName = "Embroidery Machine 2",
                    DeviceCode = "EM002",
                    SerialNumber = "SN008",
                    Model = "Model H",
                    Manufacturer = "Bernina",
                    ManufactureDate = new DateTime(2019, 9, 20),
                    InstallationDate = new DateTime(2019, 10, 10),
                    Description = "Multi-needle embroidery machine",
                    PhotoUrl = "http://example.com/photos/em002.jpg",
                    Status = "Active",
                    IsUnderWarranty = false,
                    Specifications = "{\"stitches\": \"1200\", \"type\": \"Multi-Needle\"}",
                    PurchasePrice = 3000.00m,
                    Supplier = "Bernina International",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999910"),
                    DeviceName = "Sewing Machine 5",
                    DeviceCode = "SM005",
                    SerialNumber = "SN009",
                    Model = "Model I",
                    Manufacturer = "Singer",
                    ManufactureDate = new DateTime(2021, 7, 30),
                    InstallationDate = new DateTime(2021, 8, 15),
                    Description = "Portable sewing machine",
                    PhotoUrl = "http://example.com/photos/sm005.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"speed\": \"900 SPM\", \"type\": \"Portable\"}",
                    PurchasePrice = 900.00m,
                    Supplier = "Singer Corporation",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                },
                new Device
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                    DeviceName = "Sewing Machine 6",
                    DeviceCode = "SM006",
                    SerialNumber = "SN010",
                    Model = "Model J",
                    Manufacturer = "Pegasus",
                    ManufactureDate = new DateTime(2020, 4, 10),
                    InstallationDate = new DateTime(2020, 5, 1),
                    Description = "Overlock sewing machine",
                    PhotoUrl = "http://example.com/photos/sm006.jpg",
                    Status = "Active",
                    IsUnderWarranty = true,
                    Specifications = "{\"speed\": \"1300 SPM\", \"type\": \"Overlock\"}",
                    PurchasePrice = 1700.00m,
                    Supplier = "Pegasus Ltd",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            );
        }
    }
}
