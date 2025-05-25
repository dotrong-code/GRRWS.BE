using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<Request> GetRequestByIdAsync(Guid id);
        Task<List<Request>> GetAllRequestAsync();
        Task UpdateRequestAsync(Request request, List<Guid> newIssueIds);
        Task<List<Request>> GetRequestByDeviceIdAsync(Guid deviceId);
        Task<List<Request>> GetRequestByUserIdAsync(Guid userId);
        Task<List<RequestIssue>> GetIssuesByRequestIdAsync(Guid requestId);
        Task<List<RequestSummary>> GetRequestSummaryAsync();
        Task<RequestDetailWeb?> GetRequestDetailWebByIdAsync(Guid requestId);
        Task<List<ErrorForRequestDetailWeb>> GetErrorsForRequestDetailWebAsync(Guid requestId);
        Task<List<TaskForRequestDetailWeb>> GetTasksForRequestDetailWebAsync(Guid requestId);

    }
}
