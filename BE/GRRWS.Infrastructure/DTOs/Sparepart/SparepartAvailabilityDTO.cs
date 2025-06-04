using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class SparepartAvailabilityDTO
    {
        public Guid Id { get; set; }
        public string SparepartCode { get; set; }
        public string SparepartName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? ExpectedAvailabilityDate { get; set; } // Ngày dự kiến nếu không có sẵn
    }
}
