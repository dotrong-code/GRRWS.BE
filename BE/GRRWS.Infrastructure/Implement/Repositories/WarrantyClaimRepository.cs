using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class WarrantyClaimRepository : GenericRepository<WarrantyClaim>, IWarrantyClaimRepository
    {
        public WarrantyClaimRepository(GRRWSContext context) : base(context) { }

        public Task<WarrantyClaim> FirstOrDefaultAsync(Func<WarrantyClaim, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
