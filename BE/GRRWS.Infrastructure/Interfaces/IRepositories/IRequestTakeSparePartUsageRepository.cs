using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestTakeSparePartUsageRepository : IGenericRepository<RequestTakeSparePartUsage>
    {
        Task<RequestTakeSparePartUsage> GetByIdAsync(Guid id);
        Task<RequestTakeSparePartUsage> GetByIdIncludeSparePartUsagesAsync(Guid id);
    }
}