using System.ComponentModel.DataAnnotations;
using GRRWS.Domain.Enum;

namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class RequestSummary
    {
        public Guid RequestId { get; set; }
        public string RequestTitle { get; set; }
        
        [EnumDataType(typeof(Priority))]
        public string Priority { get; set; }
        
        [EnumDataType(typeof(Status))]
        public string Status { get; set; }
        
        public DateTime RequestDate { get; set; }
    }
}
