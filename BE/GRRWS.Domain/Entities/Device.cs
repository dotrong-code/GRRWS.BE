using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Device : BaseEntity
    {
        // Thông tin cơ bản
        public string DeviceName { get; set; } // Tên thiết bị, không nullable để đảm bảo luôn có giá trị
        public string DeviceCode { get; set; } // Mã thiết bị, không nullable, unique
        public string? SerialNumber { get; set; } // Số serial của thiết bị (nếu có)
        public string? Model { get; set; } // Model của thiết bị
        public string? Manufacturer { get; set; } // Nhà sản xuất
        public DateTime? ManufactureDate { get; set; } // Ngày sản xuất
        public DateTime? InstallationDate { get; set; } // Ngày lắp đặt
        public string? Description { get; set; } // Mô tả chi tiết thiết bị
        public string? PhotoUrl { get; set; } // Ảnh

        // Trạng thái thiết bị
        public string Status { get; set; } // Trạng thái: Active, Inactive, InRepair, Retired, etc.
        public bool IsUnderWarranty { get; set; } // Thiết bị có đang trong thời gian bảo hành không

        // Thông tin kỹ thuật
        public string? Specifications { get; set; } // Thông số kỹ thuật (JSON string hoặc text)
        public decimal? PurchasePrice { get; set; } // Giá mua thiết bị
        public string? Supplier { get; set; } // Nhà cung cấp thiết bị

        // Foreign keys

        public Guid? MachineId { get; set; }
        public Guid? PositionId { get; set; }

        // Navigation properties

        public Position? Position { get; set; }
        public Machine? Machine { get; set; }
        public ICollection<DeviceWarranty> Warranties { get; set; } = new List<DeviceWarranty>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public ICollection<DeviceHistory> Histories { get; set; } = new List<DeviceHistory>();
    }
}
