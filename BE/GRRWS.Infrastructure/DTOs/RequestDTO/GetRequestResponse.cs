using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.RequestDTO
{
    public class GetRequestResponse
    {
        public Guid Id { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsSolved { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
        public bool? IsNeedSign { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public Guid RequestedById { get; set; }
        public string SenderName { get; set; }
        public Guid? PositionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
