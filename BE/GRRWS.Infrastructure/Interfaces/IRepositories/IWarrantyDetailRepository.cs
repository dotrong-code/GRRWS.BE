using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IWarrantyDetailRepository : IGenericRepository<WarrantyDetail>
    {
        Task<WarrantyDetail> GetByIdInclueErrorFixStepErrorSparepartssAsync(Guid id);
        Task<IEnumerable<WarrantyDetail>> GetAllAsync();
        Task CreateAsync(WarrantyDetail warrantyDetail);
        Task UpdateAsync(WarrantyDetail warrantyDetail);
        Task DeleteAsync(Guid id);
    }
}
