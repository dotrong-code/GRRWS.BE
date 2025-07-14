using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<Request> GetRequestByIdAsync(Guid id);
        Task<(List<Request> Items, int TotalCount)> GetAllRequestAsync(int pageNumber, int pageSize, string? search, string? searchType);
        
        Task UpdateRequestAsync(Request request, List<Guid> newIssueIds);
        Task<List<Request>> GetRequestByDeviceIdAsync(Guid deviceId);
        Task<(List<Request> Items, int TotalCount)> GetRequestByUserIdAsync(Guid userId, int pageNumber, int pageSize, string? search, string? searchType);
        Task<List<RequestIssue>> GetIssuesByRequestIdAsync(Guid requestId);
        Task<List<RequestSummary>> GetRequestSummaryAsync();
        Task<RequestDetailWeb?> GetRequestDetailWebByIdAsync(Guid requestId);
        Task<List<ErrorForRequestDetailWeb>> GetErrorsForRequestDetailWebAsync(Guid requestId);
        Task<List<TechnicalIssueForRequestDetailWeb>> GetTechnicalIssuesForRequestDetailWebAsync(Guid requestId);
        Task<List<TaskForRequestDetailWeb>> GetTasksForRequestDetailWebAsync(Guid requestId);
        Task<Request> GetByTaskIdAsync(Guid taskId);
    }
}
