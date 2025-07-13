using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Enum
{
    public enum RequestMachineReplacementType
    {
        Replacement, // Yêu cầu thay thế máy (lấy máy mới)
        StockReturn  // Yêu cầu trả máy hư về kho
    }
}
