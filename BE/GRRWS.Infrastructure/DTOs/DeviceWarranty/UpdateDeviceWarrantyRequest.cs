using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.DeviceWarranty
{
    public class UpdateDeviceWarrantyRequest : CreateDeviceWarrantyRequest
    {
        public Guid Id { get; set; }
    }
}
