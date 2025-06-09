using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.SparePartUsage
{
    public class UpdateRequestStatusRequest
    {
        public Guid RequestTakeSparePartUsageId { get; set; }
        public string Status { get; set; } // Sử dụng string để ánh xạ với enum, sẽ chuyển sang enum trong service
        public Guid? ConfirmedById { get; set; } // ID của người xác nhận (tuỳ chọn)
        public string Notes { get; set; } // Ghi chú (tuỳ chọn)
    }
}
