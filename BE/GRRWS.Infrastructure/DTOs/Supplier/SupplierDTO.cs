using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Supplier
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? LinkWeb { get; set; }
    }
}
