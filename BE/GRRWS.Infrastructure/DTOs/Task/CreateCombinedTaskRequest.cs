using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Task
{
    public class CreateCombinedTaskRequest
    {
        public Guid RequestId { get; set; }
        public List<Guid> ErrorGuidelineIds { get; set; } = new List<Guid>();
    }
}
