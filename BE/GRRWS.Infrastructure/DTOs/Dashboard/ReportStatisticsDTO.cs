using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class ReportStatisticsDTO
    {
        public double WarrantyReportsPercentage { get; set; }
        public double RepairReportsPercentage { get; set; }
        public int TotalWarrantyReports { get; set; }
        public int TotalRepairReports { get; set; }
        public int TotalReports { get; set; }
        public int TotalCompletedReports { get; set; }
        public int TotalPendingReports { get; set; }
    }
}
