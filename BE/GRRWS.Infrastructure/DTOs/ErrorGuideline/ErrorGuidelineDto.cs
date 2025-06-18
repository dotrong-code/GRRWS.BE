using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.ErrorGuideline
{
    public class ErrorGuidelineDto
    {
        public Guid Id { get; set; }
        public Guid ErrorId { get; set; }
        public string? Title { get; set; }
        public TimeSpan? EstimatedRepairTime { get; set; }
        public Priority? Priority { get; set; }
        public List<ErrorFixStepDto>? ErrorFixSteps { get; set; }
        public List<ErrorSparepartDto>? ErrorSpareparts { get; set; }
    }

    public class ErrorFixStepDto
    {
        public Guid Id { get; set; }
        public string? StepDescription { get; set; }
        public int StepOrder { get; set; }
    }

    public class ErrorSparepartDto
    {
        public Guid SparepartId { get; set; }
        public int? QuantityNeeded { get; set; }
    }
}
