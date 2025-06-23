using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Dashboard
{
    public class TotalUserByRoleDTO
    {
        public int TotalUsers { get; set; }
        public int TotalMechanics { get; set; }
        public int TotalStockKeepers { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalHeadsOfDepartment { get; set; }
        public int TotalHeadsOfTechnical { get; set; }
    }
}
