using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Sparepart;


using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorService : IErrorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public ErrorService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
        public async Task<Result> GetErrorSuggestionsAsync(string query, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result.Failure(Error.Validation("InvalidQuery", "Query cannot be null or empty."));

            if (maxResults <= 0)
                return Result.Failure(Error.Validation("InvalidMaxResults", "maxResults must be greater than 0."));

            // Generate cache key
            var cacheKey = $"suggestions_{query.ToLowerInvariant()}";

            // Check cache
            if (_cache.TryGetValue(cacheKey, out List<SuggestObject> cachedSuggestions) && cachedSuggestions != null)
            {
                return Result.SuccessWithObject(cachedSuggestions);
            }

            // Normalize query
            var normalizedQuery = StringHelper.RemoveDiacritics(query);

            // Get suggestions from repository
            var suggestions = await _unitOfWork.ErrorRepository.GetErrorSuggestionsAsync(normalizedQuery, maxResults);

            // Store in cache (expires after 5 minutes)
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            _cache.Set(cacheKey, suggestions, cacheEntryOptions);

            return Result.SuccessWithObject(suggestions);
        }




        public async Task<Result> GetRecommendedErrorsAsync(IssueIdsRequestDTO dto)
        {
            if (dto == null || dto.IssueIds == null || !dto.IssueIds.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Issue IDs cannot be empty.", 0));
            }

            var errors = await _unitOfWork.ErrorRepository.GetErrorsByIssueIdsAsync(dto.IssueIds);
            if (!errors.Any())
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No errors found for the provided issues.", 0));
            }

            return Result.SuccessWithObject(errors);
        }
        public async Task<Result> GetSparepartsByErrorIdAsync(Guid errorId)
        {
            var spareparts = await _unitOfWork.ErrorRepository.GetSparepartsByErrorIdAsync(errorId);
            if (spareparts == null || !spareparts.Any())
            {
                return Result.Failure(Error.Validation("Guid not found", "Sparepart not found for this error"));
            }
            var sparepartDtos = spareparts.Select(s => new SparepartDto
            {
                SparepartCode = s.SparepartCode,
                SparepartName = s.SparepartName,
                Description = s.Description,
                Specification = s.Specification,
                StockQuantity = s.StockQuantity
            }).ToList();

            return Result.SuccessWithObject(sparepartDtos);
        }
    }
}
