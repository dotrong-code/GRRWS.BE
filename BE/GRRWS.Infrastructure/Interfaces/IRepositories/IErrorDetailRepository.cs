using GRRWS.Domain.Entities;

using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorDetailRepository : IGenericRepository<ErrorDetail>
    {
        Task<List<ErrorDetail>> GetByRequestIdAsync(Guid requestId);
        Task<bool> ErrorDetailExistsAsync(Guid errorId, Guid reportId);
        Task<Guid> GetReportIdByRequestIdAsync(Guid requestId);
        Task<ErrorDetail> GetByIdWithDetailsAsync(Guid id);
    }
}