using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
using GRRWS.Infrastructure.DTOs.IssueDTO;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Error = GRRWS.Infrastructure.DTOs.Common.Error;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorService : IErrorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IImportService _importService;
        public ErrorService(IUnitOfWork unitOfWork, IMemoryCache cache, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _importService = importService;
        }
        public async Task<Result> GetErrorSuggestionsAsync(string query, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("InvalidQuery", "Query cannot be null or empty."));

            if (maxResults <= 0)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("InvalidMaxResults", "maxResults must be greater than 0."));

            // Generate cache key
            var cacheKey = $"error_suggestions_{query.ToLowerInvariant()}";

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


        public async Task<Result> GetErrorsByReportIdWithoutTaskAsync(Guid reportId)
        {
            if (reportId == Guid.Empty)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("InvalidReportId", "Report ID cannot be empty."));
            }

            var errors = await _unitOfWork.ErrorRepository.GetErrorsByReportIdWithoutTaskAsync(reportId);

            if (errors == null || !errors.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NoErrors", "No unassigned errors found for this report."));
            }

            return Result.SuccessWithObject(errors);
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
            //var spareparts = await _unitOfWork.ErrorRepository.GetSparepartsByErrorIdAsync(errorId);
            //if (spareparts == null || !spareparts.Any())
            //{
            //    return Result.Failure(Error.Validation("Guid not found", "Sparepart not found for this error"));
            //}
            //var sparepartDtos = spareparts.Select(s => new SparepartDto
            //{
            //    Id = s.Id,
            //    SparepartCode = s.SparepartCode,
            //    SparepartName = s.SparepartName,
            //    Description = s.Description,
            //    Specification = s.Specification,
            //    StockQuantity = s.StockQuantity
            //}).ToList();

            //return Result.SuccessWithObject(spareparts);
            return null;
        }


        public async Task<Result> GetListOfSparepartByErrorAsync(List<Guid> errorIds)
        {
            //// Await the task to get the result before calling Any()
            //var missingError = await _unitOfWork.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(errorIds);
            //if (missingError.Any())
            //{
            //    return Result.Failure(Error.Validation("Error not found", $"The following errors were not found: {string.Join(", ", missingError)}"));
            //}
            //var spareparts = await _unitOfWork.ErrorRepository.GetListOfSparepartByErrorAsync(errorIds);

            //// Add appropriate return statement if needed
            //return Result.SuccessWithObject(spareparts);
            return  null;
        }
        public async Task<Result> ImportErrorsAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<Domain.Entities.Error>(file.OpenReadStream(), _unitOfWork.ErrorRepository);
        }
        public async Task<Result> GetAllErrorsAsync(int pageNumber, int pageSize, string? searchByName)
        {
            var errors = await _unitOfWork.ErrorRepository.GetAllErrorsAsync(pageNumber, pageSize, searchByName);
            if (errors == null || !errors.Any())
                return Result.Failure(Error.NotFound("NoErrorsFound", "No errors found."));
            return Result.SuccessWithObject(errors);
        }
        public async Task<Result> UpdateErrorAsync(UpdateErrorDTO updateErrorDTO)
        {
            if (updateErrorDTO == null)
            {
                return Result.Failure(Error.Validation("InvalidUpdateData", "Update data cannot be null."));
            }

            var currentErrors = await _unitOfWork.ErrorRepository.GetAllAsync();

            var currentError = currentErrors.FirstOrDefault(i => i.Id == updateErrorDTO.Id);
            if (currentError == null)
            {
                return Result.Failure(Error.Validation("ErrorNotFound", "Error not found."));
            }

            if (!string.IsNullOrEmpty(updateErrorDTO.ErrorCode))
            {
                var duplicateError = currentErrors.FirstOrDefault(i => i.ErrorCode == updateErrorDTO.ErrorCode && i.Id != updateErrorDTO.Id);
                if (duplicateError != null)
                {
                    return Result.Failure(Error.Validation("DuplicateErrorCode", "The ErrorCode already exists."));
                }
            }

            var isUpdated = await _unitOfWork.ErrorRepository.UpdateErrorAsync(updateErrorDTO);
            if (!isUpdated)
            {
                return Result.Failure(Error.Validation("UpdateFailed", "Failed to update the error."));
            }
            return Result.SuccessWithObject("Error updated successfully!");
        }
        public async Task<Result> DeleteErrorsAsync(Guid id)
        {
            var isDeleted = await _unitOfWork.ErrorRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return Result.Failure(Error.Validation("DeleteFailed", "Failed to delete the error."));
            }
            return Result.SuccessWithObject("Error deleted successfully!");
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var getError = await _unitOfWork.ErrorRepository.GetByIdAsync(id);
            if (getError == null)
            {
                return Result.Failure(Error.NotFound("ErrorNotFound", "Error not found."));
            }
            var error = new ErrorDTO
            {
                Name = getError.Name,
                ErrorCode = getError.ErrorCode,
                Description = getError.Description,
                OccurrenceCount = getError.OccurrenceCount,
                IsCommon = getError.IsCommon,
                EstimatedRepairTime = getError.EstimatedRepairTime,
                Severity = getError.Severity
            };
            return Result.SuccessWithObject(error);
        }
    }
}
