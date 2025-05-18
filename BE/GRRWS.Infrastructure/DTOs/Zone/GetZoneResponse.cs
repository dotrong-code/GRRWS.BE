using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Zone
{
    public class GetZoneResponse
    {
        public Guid Id { get; set; }
        public string? ZoneName { get; set; }
        public Guid? AreaId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
