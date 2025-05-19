using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Position
{
    public class CreatePositionRequest
    {
        public int Index { get; set; }
        public Guid ZoneId { get; set; }
    }
}
