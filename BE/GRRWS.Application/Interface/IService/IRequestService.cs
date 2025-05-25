using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Application.Interface.IService
{
    public interface IRequestService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId);
        Task<Result> UpdateAsync(UpdateRequestDTO dto, Guid id);
        Task<Result> CancelRequestAsync(CancelRequestDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetRequestByDeviceIdAsync(Guid id);
        Task<Result> GetRequestByUserIdAsync(Guid userId);
        Task<Result> GetIssuesByRequestIdAsync(Guid requestId);
        Task<Result> GetRequestSummary();
        //Task<Result> CreateRequestAsync(CreateRequest request, Guid userId);
    }
}
