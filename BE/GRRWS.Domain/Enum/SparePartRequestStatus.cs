using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Enum
{
    public enum SparePartRequestStatus
    {
        Unconfirmed = 0,  // Chưa xác nhận
        Confirmed = 1,    // Đã xác nhận
        Insufficient = 2, // Chưa đủ
        Delivered = 3,    // Đã giao
        Cancelled = 4     // Đã hủy
    }
}
