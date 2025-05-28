using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IWarrantyDetailRepository
    {
        Task<WarrantyDetail> GetByIdAsync(Guid id);
        Task<IEnumerable<WarrantyDetail>> GetAllAsync();
        Task CreateAsync(WarrantyDetail warrantyDetail);
        Task UpdateAsync(WarrantyDetail warrantyDetail);
        Task DeleteAsync(Guid id);
    }
}
