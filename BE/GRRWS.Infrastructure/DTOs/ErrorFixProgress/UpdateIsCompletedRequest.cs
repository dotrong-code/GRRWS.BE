using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorFixProgress
{
    public class UpdateIsCompletedRequest
    {
        public List<Guid> ErrorFixProgressIds { get; set; }
        public bool IsCompleted { get; set; }
    }
}
