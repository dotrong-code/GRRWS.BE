using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Area;

namespace GRRWS.Application.Interface.IService
{
    public interface IAreaService
    {
        Task<Result> CreateAreaAsync(CreateAreaRequest request);
        Task<Result> GetAreaByIdAsync(Guid id);
        Task<Result> GetAllAreasAsync(int pageNumber, int pageSize);
        Task<Result> UpdateAreaAsync(UpdateAreaRequest request);
        Task<Result> DeleteAreaAsync(Guid id);
    }
}
