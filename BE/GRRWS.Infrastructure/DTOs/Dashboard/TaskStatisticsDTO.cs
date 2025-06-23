using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class TaskStatisticsDTO
    {
        public double WarrantyTasksPercentage { get; set; }
        public double RepairTasksPercentage { get; set; }
        public double ReplaceTasksPercentage { get; set; }
        public int TotalWarrantyTasks { get; set; }
        public int TotalRepairTasks { get; set; }
        public int TotalReplaceTasks { get; set; }
        public int TotalTasks { get; set; }
        public int TotalCompletedTasks { get; set; }
        public int TotalPendingTasks { get; set; }
    }
}
