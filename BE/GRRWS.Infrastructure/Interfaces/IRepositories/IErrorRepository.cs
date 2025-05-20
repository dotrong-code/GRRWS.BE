using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorRepository
    {
        Task<List<SuggestObject>> GetErrorSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<Sparepart>> GetSparepartsByErrorIdAsync(Guid errorId);
    }
}
