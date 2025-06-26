using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IWarrantyClaimRepository : IGenericRepository<WarrantyClaim>
    {
        Task<WarrantyClaim> FirstOrDefaultAsync(Func<WarrantyClaim, bool> predicate);
        void Update(WarrantyClaim warrantyClaim);
    }
}
