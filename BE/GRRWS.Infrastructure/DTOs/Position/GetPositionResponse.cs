using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Position
{
    public class GetPositionResponse
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid? ZoneId { get; set; }
        public Guid? DeviceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
