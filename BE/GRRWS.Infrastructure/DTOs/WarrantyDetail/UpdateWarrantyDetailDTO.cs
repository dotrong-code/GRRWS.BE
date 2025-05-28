using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.WarrantyDetail
{
    public class UpdateWarrantyDetailDTO
    {
        public string WarrantyNotes { get; set; }
        public Guid? TaskId { get; set; }
    }
}
