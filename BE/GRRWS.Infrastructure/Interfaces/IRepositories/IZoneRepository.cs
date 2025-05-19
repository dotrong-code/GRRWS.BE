using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Zone;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Task<(List<GetZoneResponse> Zones, int TotalCount)> GetAllZonesAsync(
            Guid? areaId = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<int> DeleteZoneAsync(Guid id);
    }
}
