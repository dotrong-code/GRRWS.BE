﻿using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorService
    {
        Task<Result> GetErrorSuggestionsAsync(string query, int maxResults);

        Task<Result> GetRecommendedErrorsAsync(IssueIdsRequestDTO dto);

        Task<Result> GetSparepartsByErrorIdAsync(Guid errorId);
        Task<Result> GetErrorsByReportIdWithoutTaskAsync(Guid reportId);
        Task<Result> GetListOfSparepartByErrorAsync(List<Guid> errorIds);
    }
}
