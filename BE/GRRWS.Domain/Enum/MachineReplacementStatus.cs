using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Enum
{
    public enum MachineReplacementStatus
    {
        Pending,      // Chờ duyệt
        Approved,     // Đã duyệt
        Rejected,     // Bị từ chối
        InProgress,   // Đang thực hiện
        Completed,    // Hoàn thành
        Cancelled     // Hủy bỏ
    }
}
