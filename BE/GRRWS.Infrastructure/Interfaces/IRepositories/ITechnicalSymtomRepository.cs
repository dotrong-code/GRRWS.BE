using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.TechnicalSymtom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ITechnicalSymtomRepository
    {
        Task<List<SuggestObject>> GetNotFoundTechnicalSymtomDisplayNamesAsync(IEnumerable<Guid> technicalSymtomIds);
        Task<List<SuggestObject>> GetSymtomSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<TechnicalSymtomDTO>> GetSymtomsByIssueIdsAsync(List<Guid> issueIds);
    }
}
