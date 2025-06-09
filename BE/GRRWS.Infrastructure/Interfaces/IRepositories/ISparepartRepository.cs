using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ISparepartRepository : IGenericRepository<Sparepart>
    {
        Task<(List<Sparepart> Items, int TotalCount)> GetSparepartsBySupplierIdAsync(Guid supplierId, int pageNumber, int pageSize);
        Task<(List<Sparepart> Items, int TotalCount)> GetAllActiveSparepartsAsync(int pageNumber, int pageSize);
        Task<(List<Sparepart> Items, int TotalCount)> GetLowStockSparepartsAsync(int pageNumber, int pageSize);
        Task<(List<Sparepart> Items, int TotalCount)> GetOutOfStockSparepartsAsync(int pageNumber, int pageSize);
        Task<Sparepart> GetByIdWithDetailsAsync(Guid id);
    }
}
