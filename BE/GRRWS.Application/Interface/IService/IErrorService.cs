using GRRWS.Application.Common.Result;
using GRRWS.Domain.Entities;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorService
    {
        Task<Result> GetErrorSuggestionsAsync(string query, int maxResults);
        Task<Result> GetSparepartsByErrorIdAsync(Guid errorId);
    }
}
