using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Zone;

namespace GRRWS.Application.Interface.IService
{
    public interface IZoneService
    {
        Task<Result> CreateZoneAsync(CreateZoneRequest request);
        Task<Result> GetZoneByIdAsync(Guid id);
        Task<Result> GetAllZonesAsync(Guid? areaId, int pageNumber, int pageSize);
        Task<Result> UpdateZoneAsync(UpdateZoneRequest request);
        Task<Result> DeleteZoneAsync(Guid id);
    }
}
