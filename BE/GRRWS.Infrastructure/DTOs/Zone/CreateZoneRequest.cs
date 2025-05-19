using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Zone
{
    public class CreateZoneRequest
    {
        public string? ZoneName { get; set; }
        public Guid AreaId { get; set; }
    }
}
