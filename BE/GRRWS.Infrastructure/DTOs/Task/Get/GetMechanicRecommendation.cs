using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetMechanicRecommendation
    {
        public Guid MechanicId { get; set; }
        public string FullName { get; set; }
        public double AverageCompletionTime { get; set; }
        public string ShiftName { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}