using GRRWS.Application.Common.Result;
using GRRWS.Application.Common;
using GRRWS.Infrastructure.DTOs.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Application.Implement.Service
{
    public class TechnicalSymtomService : ITechnicalSymtomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public TechnicalSymtomService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
        public async Task<Result> GetSymtomSuggestionsAsync(string query, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result.Failure(Error.Validation("InvalidQuery", "Query cannot be null or empty."));

            if (maxResults <= 0)
                return Result.Failure(Error.Validation("InvalidMaxResults", "maxResults must be greater than 0."));

            // Generate cache key
            var cacheKey = $"symtom_suggestions_{query.ToLowerInvariant()}";

            // Check cache
            if (_cache.TryGetValue(cacheKey, out List<SuggestObject> cachedSuggestions) && cachedSuggestions != null)
            {
                return Result.SuccessWithObject(cachedSuggestions);
            }

            // Normalize query
            var normalizedQuery = StringHelper.RemoveDiacritics(query);

            // Get suggestions from repository
            var suggestions = await _unitOfWork.TechnicalSymtomRepository.GetSymtomSuggestionsAsync(normalizedQuery, maxResults);

            // Store in cache (expires after 5 minutes)
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            _cache.Set(cacheKey, suggestions, cacheEntryOptions);

            return Result.SuccessWithObject(suggestions);
        }
        public async Task<Result> GetRecommendedSymtomsAsync(IssueIdsRequestDTO dto)
        {
            if (dto == null || dto.IssueIds == null || !dto.IssueIds.Any())
            {
                return Result.Failure(Error.Validation("InvalidRequest", "Issue IDs cannot be empty."));
            }

            var symtoms = await _unitOfWork.TechnicalSymtomRepository.GetSymtomsByIssueIdsAsync(dto.IssueIds);
            if (!symtoms.Any())
            {
                return Result.Failure(Error.NotFound("NoSymptomsFound", "No symptoms found for the provided issues."));
            }

            return Result.SuccessWithObject(symtoms);
        }
    }
}
