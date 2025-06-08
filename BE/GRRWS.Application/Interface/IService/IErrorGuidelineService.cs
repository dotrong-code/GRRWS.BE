using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.ErrorDetail;
using GRRWS.Infrastructure.DTOs.ErrorGuideline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorGuidelineService
    {
        Task<Result> CreateErrorGuidelineAsync(CreateErrorGuidelineRequest request);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllByErrorIdAsync(Guid errorId);
        Task<Result> UpdateErrorGuidelineAsync(Guid id, UpdateErrorGuidelineRequests request);
        Task<Result> DeleteAsync(Guid id);
    }
}
