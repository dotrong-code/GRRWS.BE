using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class ErrorAction : BaseEntity
    {
        public Guid ErrorId { get; set; }
        public int StepOrder { get; set; }
        public string ActionName { get; set; }
        public string? Description { get; set; }
        public TimeSpan? EstimatedTime { get; set; }

        // Navigation
        public Error Error { get; set; }

        public ICollection<TaskAction> TaskActions { get; set; }

    }
}
