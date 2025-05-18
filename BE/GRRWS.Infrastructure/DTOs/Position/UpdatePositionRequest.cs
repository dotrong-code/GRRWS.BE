using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Position
{
    public class UpdatePositionRequest : CreatePositionRequest
    {
        public Guid Id { get; set; }
    }
}
