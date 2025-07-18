﻿using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GRRWS.Application.Implement.Service
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public IssueService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result> GetIssueSuggestionsAsync(string query, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result.Failure(Error.Validation("InvalidQuery", "Query cannot be null or empty."));

            if (maxResults <= 0)
                return Result.Failure(Error.Validation("InvalidMaxResults", "maxResults must be greater than 0."));

            // Generate cache key
            var cacheKey = $"issue_suggestions_{query.ToLowerInvariant()}";

            // Check cache
            if (_cache.TryGetValue(cacheKey, out List<SuggestObject> cachedSuggestions) && cachedSuggestions != null)
            {
                return Result.SuccessWithObject(cachedSuggestions);
            }

            // Normalize query
            var normalizedQuery = StringHelper.RemoveDiacritics(query);

            // Get suggestions from repository
            var suggestions = await _unitOfWork.IssueRepository.GetIssueSuggestionsAsync(normalizedQuery, maxResults);

            // Store in cache (expires after 5 minutes)
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            _cache.Set(cacheKey, suggestions, cacheEntryOptions);

            return Result.SuccessWithObject(suggestions);
        }

        public async Task<Result> GetIssueByIdAsync(Guid id)
        {
            var issue = await _unitOfWork.IssueRepository.GetByIdAsync(id);
            if (issue == null)
                return Result.Failure(Error.NotFound("IssueNotFound", "Issue not found."));
            var issueDto = new SuggestObject
            {
                Id = issue.Id,
                Name = issue.DisplayName
            };
            return Result.SuccessWithObject(issueDto);
        }
    }
}