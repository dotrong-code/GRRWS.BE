using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Zone
{
    public class UpdateZoneRequest : CreateZoneRequest
    {
        public Guid Id { get; set; }
    }
}
