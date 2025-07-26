using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.ErrorDetail;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorDetailService
    {
        Task<Result> CreateErrorDetailAsync(CreateErrorDetailRequest request);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetByRequestIdAsync(Guid requestId);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> UpdateErrorTaskAsync(Guid id, UpdateErrorTaskRequest request);
        
    }
}