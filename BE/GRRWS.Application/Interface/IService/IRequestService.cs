using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using static GRRWS.Infrastructure.DTOs.RequestDTO.CreateRequestFormDTO;

namespace GRRWS.Application.Interface.IService
{
    public interface IRequestService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId);
        Task<Result> CreateTestAsync(TestCreateRequestDTO dto, Guid userId);
        Task<Result> UpdateAsync(UpdateRequestDTO dto, Guid id);
        Task<Result> CancelRequestAsync(CancelRequestDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetRequestByDeviceIdAsync(Guid id);
        Task<Result> GetRequestByUserIdAsync(Guid userId);
        Task<Result> GetIssuesByRequestIdAsync(Guid requestId);
        Task<Result> GetRequestSummary();
        Task<Result> UpdateRequestIssueStatusAsync(Guid requestId, Guid issueId, bool isRejected, string rejectionReason, string rejectionDetails);
        Task<Result> UpdateRequestStatusAsync(Guid requestId, bool isRejected, string rejectionReason, string rejectionDetails);
        //Task<Result> CreateRequestAsync(CreateRequest request, Guid userId);
        Task<Result> GetRequestDetailWebByIdAsync(Guid requestId);
        Task<Result> GetErrorsForRequestDetailWebAsync(Guid requestId);
        Task<Result> GetTechnicalIssuesForRequestDetailWebAsync(Guid requestId);
        Task<Result> GetTasksForRequestDetailWebAsync(Guid requestId);
    }
}
