using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class TaskByWeekAndMonthDTO
    {
        public int TotalTasksThisWeek { get; set; }
        public int TotalTasksThisMonth { get; set; }
    }
}
