using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.DTOs.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class SparePartUsageRepository : GenericRepository<SparePartUsage>, ISparePartUsageRepository
    {
        public SparePartUsageRepository(GRRWSContext context) : base(context) { }

        
    }
}