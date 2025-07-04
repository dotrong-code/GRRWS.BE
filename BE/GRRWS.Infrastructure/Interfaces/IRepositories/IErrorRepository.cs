﻿using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;

using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorRepository : IGenericRepository<Domain.Entities.Error>
    {
        Task<List<SuggestObject>> GetErrorSuggestionsAsync(string normalizedQuery, int maxResults);
        Task<List<ErrorSimpleDTO>> GetErrorsByIssueIdsAsync(List<Guid> issueIds);
       // Task<List<Sparepart>> GetSparepartsByErrorIdAsync(Guid errorId);
        Task<List<ErrorSimpleDTO>> GetErrorsByReportIdWithoutTaskAsync(Guid reportId);
        Task<List<SuggestObject>> GetNotFoundErrorDisplayNamesAsync(IEnumerable<Guid> errorIds);

        //Task<List<SparepartWeb>> GetListOfSparepartByErrorAsync(List<Guid> errorIds);

    }
}
