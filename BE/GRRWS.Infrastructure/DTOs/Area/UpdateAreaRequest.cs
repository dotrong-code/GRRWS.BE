using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Area
{
    public class UpdateAreaRequest : CreateAreaRequest
    {
        public Guid Id { get; set; }
    }
}
