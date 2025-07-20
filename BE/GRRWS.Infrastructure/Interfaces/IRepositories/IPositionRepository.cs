using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Position;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task<(List<GetPositionResponse> Positions, int TotalCount)> GetAllPositionsAsync(
            Guid? zoneId = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<int> DeletePositionAsync(Guid id);
        Task<List<Position>> GetPositionsByZoneIdAsync(Guid zoneId);
        Task<List<Position>> GetAllPositionsWithDetailsAsync();
        Task<Position> GetByIdAsync(Guid id);
        Task<List<Position>> GetPositionsByAreaIdAsync(Guid areaId);
    }
}
