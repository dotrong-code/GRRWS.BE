using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorGuidelineRepository : IGenericRepository<ErrorGuideline>
    {
        Task<List<ErrorGuideline>> GetAllByErrorIdAsync(Guid errorId);
        Task<ErrorGuideline> GetByIdInclueErrorFixStepErrorSparepartssAsync(Guid id);
        Task<ErrorGuideline> GetFirstByErrorIdAsync(Guid errorId);
        Task<List<ErrorGuideline>> GetErrorGuidelinesAsync(IEnumerable<Guid> guidelineIds);
        Task<List<Guid>> GetGuidelineIdsByErrorIdsAsync(IEnumerable<Guid> errorIds);
        
    }
}
