using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ISparePartUsageRepository : IGenericRepository<SparePartUsage>
    {
        Task<PagedResponse<RequestTakeSparePartUsage>> GetAllRequestTakeSparePartUsagesAsync(int pageNumber, int pageSize, string assigneeName = null);
        Task<PagedResponse<RequestTakeSparePartUsage>> GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus status, int pageNumber, int pageSize, string assigneeName = null);
        Task<RequestTakeSparePartUsage> GetRequestTakeSparePartUsageByIdAsync(Guid id);
        Task<SparePartUsage> GetByIdAsync(Guid id);
        Task<List<SparePartUsage>> GetByRequestTakeSparePartUsageIdAsync(Guid requestId);
    }
}