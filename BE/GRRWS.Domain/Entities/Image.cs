using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }
        public Guid RequestIssueId { get; set; }
        public RequestIssue RequestIssue { get; set; }
    }
}
