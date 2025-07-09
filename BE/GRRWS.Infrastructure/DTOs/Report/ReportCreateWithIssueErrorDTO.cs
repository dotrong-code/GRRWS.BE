using GRRWS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Report
{
    public class ReportCreateWithIssueErrorDTO
    {
        public Guid? RequestId { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public List<Guid> ErrorIds { get; set; } // Errors độc lập
        public Dictionary<Guid, List<Guid>> IssueErrorMappings { get; set; } // IssueId -> List<ErrorId>
        public ActionType ActionType { get; set; } // Thêm trường ActionType để phân biệt Replacement hay Repair
    }
    public class ReportCreateWithIssueSymtomDTO
    {
        public Guid? RequestId { get; set; }
        public int Priority { get; set; }
        public string Location { get; set; }
        public List<Guid>? TechnicalSymtomIds { get; set; }
        public Dictionary<Guid, List<Guid>>? IssueSymtomMappings { get; set; }
    }
}
