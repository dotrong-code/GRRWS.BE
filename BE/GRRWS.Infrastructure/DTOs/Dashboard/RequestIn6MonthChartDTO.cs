using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class RequestIn6MonthChartDTO
    {
        public string MonthYear { get; set; }
        public int RequestCount { get; set; }
    }
}
