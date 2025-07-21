using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.RequestTakeSparePartUsage;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestTakeSparePartUsageRepository : IGenericRepository<RequestTakeSparePartUsage>
    {
        Task<(List<RequestTakeSparePartUsageViewDTO> items, int totalCount)> GetAllAsync(
int pageNumber,
int pageSize,
string? status = null,
string? sortBy = null,
bool isAscending = true);
        Task<RequestTakeSparePartUsage> GetByIdAsync(Guid id);
        Task<RequestTakeSparePartUsage> GetByIdIncludeSparePartUsagesAsync(Guid id);
    }
}