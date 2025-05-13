using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common
{
    public class CurrentUserObject
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }//1 admin, 2 manager,3 veterinarian, 4 staff, 5 customer
        public string RoleName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
