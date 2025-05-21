using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class AssignTaskDTO
    {
        public Guid AssigneeId { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
