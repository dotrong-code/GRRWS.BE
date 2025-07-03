using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class TotalTaskRequestReportDTO
    {
        public int TotalMachines { get; set; }
        public int TotalRequests { get; set; }
        public int TotalReports { get; set; }
    }
}
