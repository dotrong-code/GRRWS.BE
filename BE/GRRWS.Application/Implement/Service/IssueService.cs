using DocumentFormat.OpenXml.Office2010.Excel;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.IssueDTO;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Error = GRRWS.Infrastructure.DTOs.Common.Error;

namespace GRRWS.Application.Implement.Service
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IImportService _importService;
        public IssueService(IUnitOfWork unitOfWork, IMemoryCache cache, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _importService = importService;
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
        public async Task<Result> ImportIssuesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<Issue>(file.OpenReadStream(), _unitOfWork.IssueRepository);
        }
        public async Task<Result> GetAllIssuesAsync(int pageNumber, int pageSize, string? searchByName)
        {
            var issues = await _unitOfWork.IssueRepository.GetAllIssuesAsync(pageNumber, pageSize, searchByName);
            if (issues == null || !issues.Any())
                return Result.Failure(Error.NotFound("NoIssuesFound", "No issues found."));
            return Result.SuccessWithObject(issues);
        }
        public async Task<Result> UpdateIssueAsync(UpdateIssueDTO updateIssueDTO)
        {
            if (updateIssueDTO == null)
            {
                return Result.Failure(Error.Validation("InvalidUpdateData", "Update data cannot be null."));
            }

            var currentIssues = await _unitOfWork.IssueRepository.GetAllAsync();

            var currentIssue = currentIssues.FirstOrDefault(i => i.Id == updateIssueDTO.Id);
            if (currentIssue == null)
            {
                return Result.Failure(Error.Validation("IssueNotFound", "Issue not found."));
            }

            if (!string.IsNullOrEmpty(updateIssueDTO.IssueKey))
            {
                var duplicateIssue = currentIssues.FirstOrDefault(i => i.IssueKey == updateIssueDTO.IssueKey && i.Id != updateIssueDTO.Id);
                if (duplicateIssue != null)
                {
                    return Result.Failure(Error.Validation("DuplicateIssueKey", "The IssueKey already exists."));
                }
            }

            var isUpdated = await _unitOfWork.IssueRepository.UpdateIssueAsync(updateIssueDTO);
            if (!isUpdated)
            {
                return Result.Failure(Error.Validation("UpdateFailed", "Failed to update the issue."));
            }
            return Result.SuccessWithObject("Issue updated successfully!");
        }
        public async Task<Result> DeleteIssuesAsync(Guid id)
        {
            var isDeleted = await _unitOfWork.IssueRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return Result.Failure(Error.Validation("DeleteFailed", "Failed to delete the issue."));
            }
            return Result.SuccessWithObject("Issue deleted successfully!");
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var getIssue = await _unitOfWork.IssueRepository.GetByIdAsync(id);
            if (getIssue == null)
            {
                return Result.Failure(Error.NotFound("IssueNotFound", "Issue not found."));
            }
            var issue = new IssueDTO
            {
                IssueKey = getIssue.IssueKey,
                DisplayName = getIssue.DisplayName,
                Description = getIssue.Description,
                OccurrenceCount = getIssue.OccurrenceCount,
                IsCommon = getIssue.IsCommon
            };
            return Result.SuccessWithObject(issue);
        }
    }
}