using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorGuideline
{
    public class CreateErrorGuidelineRequest
    {
        public string Title { get; set; }
        public Guid ErrorId { get; set; }
        public List<ErrorFixStepRequest> ErrorFixSteps { get; set; } // Thay bằng danh sách bước mới
        public List<Guid>? ErrorSparepartIds { get; set; }
    }

    public class ErrorFixStepRequest
    {
        public string StepDescription { get; set; }
        public int StepOrder { get; set; }
    }
}
