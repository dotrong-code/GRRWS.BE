using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ISparePartUsageRepository : IGenericRepository<SparePartUsage>
    {
        Task<List<RequestTakeSparePartUsage>> GetAllRequestTakeSparePartUsagesAsync();
        Task<List<RequestTakeSparePartUsage>> GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus status);
        Task<RequestTakeSparePartUsage> GetRequestTakeSparePartUsageByIdAsync(Guid id);
        Task<SparePartUsage> GetByIdAsync(Guid id);
        Task<List<SparePartUsage>> GetByRequestTakeSparePartUsageIdAsync(Guid requestId);
    }
}