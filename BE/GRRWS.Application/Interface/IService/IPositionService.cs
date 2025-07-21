using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Position;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IPositionService
    {
        Task<Result> CreatePositionAsync(CreatePositionRequest request);
        Task<Result> GetPositionByIdAsync(Guid id);
        Task<Result> GetAllPositionsAsync(Guid? zoneId, int pageNumber, int pageSize);
        Task<Result> UpdatePositionAsync(UpdatePositionRequest request);
        Task<Result> DeletePositionAsync(Guid id);
        Task<Result> ImportPositionsAsync(IFormFile file);
        Task<Result> GetPositionsByAreaIdAsync(Guid areaId);
        Task<Result> GetRequestsByPositionIdAsync(Guid positionId);
        Task<Result> GetTaskConfirmationsByPositionIdAsync(Guid positionId);
    }
}
