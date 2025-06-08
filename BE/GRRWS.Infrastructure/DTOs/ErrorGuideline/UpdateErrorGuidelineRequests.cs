using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorGuideline
{
    public class UpdateErrorGuidelineRequests
    {
        public string Title { get; set; }
        public Guid? ErrorId { get; set; }
        public List<Guid> ErrorFixStepIds { get; set; }
        public List<Guid>? ErrorSparepartIds { get; set; }
    }
}
