using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Area;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<(List<GetAreaResponse> Areas, int TotalCount)> GetAllAreasAsync(int pageNumber, int pageSize);
        Task<int> DeleteAreaAsync(Guid id);
    }
}
