using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.TechnicalSymtom;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = GRRWS.Infrastructure.DTOs.Common.Error;

namespace GRRWS.Application.Implement.Service
{
    public class TechnicalSymtomService : ITechnicalSymtomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IImportService _importService;
        public TechnicalSymtomService(IUnitOfWork unitOfWork, IMemoryCache cache, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _importService = importService;
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
        public async Task<Result> ImportTechnicalSymtomsAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<TechnicalSymptom>(file.OpenReadStream(), _unitOfWork.TechnicalSymtomRepository);
        }
        public async Task<Result> GetAllTechnicalSymtomsAsync(int pageNumber, int pageSize, string? searchByName)
        {
            var symptoms = await _unitOfWork.TechnicalSymtomRepository.GetAllTechnicalSymptomsAsync(pageNumber, pageSize, searchByName);
            if (symptoms == null || !symptoms.Any())
                return Result.Failure(Error.NotFound("NoTechnicalSymptomsFound", "No technical symptoms found."));
            return Result.SuccessWithObject(symptoms);
        }
        public async Task<Result> UpdateTechnicalSymtomAsync(TechnicalSymptomUpdateDTO updateDTO)
        {
            if (updateDTO == null)
            {
                return Result.Failure(Error.Validation("InvalidUpdateData", "Update data cannot be null."));
            }

            var currentSymptoms = await _unitOfWork.TechnicalSymtomRepository.GetAllAsync();

            var currentSymptom = currentSymptoms.FirstOrDefault(i => i.Id == updateDTO.Id);
            if (currentSymptom == null)
            {
                return Result.Failure(Error.Validation("SymptomNotFound", "Technical symptom not found."));
            }

            if (!string.IsNullOrEmpty(updateDTO.SymptomCode))
            {
                var duplicate = currentSymptoms.FirstOrDefault(i => i.SymptomCode == updateDTO.SymptomCode && i.Id != updateDTO.Id);
                if (duplicate != null)
                {
                    return Result.Failure(Error.Validation("DuplicateSymptomCode", "The SymptomCode already exists."));
                }
            }

            var isUpdated = await _unitOfWork.TechnicalSymtomRepository.UpdateTechnicalSymptomAsync(updateDTO);
            if (!isUpdated)
            {
                return Result.Failure(Error.Validation("UpdateFailed", "Failed to update the techical symptom."));
            }
            return Result.SuccessWithObject("Techical symptom updated successfully!");
        }
        public async Task<Result> DeleteTechnicalSymtomAsync(Guid id)
        {
            var isDeleted = await _unitOfWork.TechnicalSymtomRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return Result.Failure(Error.Validation("DeleteFailed", "Failed to delete the techical symptom."));
            }
            return Result.SuccessWithObject("Techical symptom deleted successfully!");
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var getSymptom = await _unitOfWork.TechnicalSymtomRepository.GetByIdAsync(id);
            if (getSymptom == null)
            {
                return Result.Failure(Error.NotFound("SymptomNotFound", "Techical symptom not found."));
            }
            var symptom = new TechnicalSymptomViewDTO
            {
                SymptomCode = getSymptom.SymptomCode,
                Name = getSymptom.Name,
                Description = getSymptom.Description,
                IsCommon = getSymptom.IsCommon,
                OccurrenceCount = getSymptom.OccurrenceCount
            };
            return Result.SuccessWithObject(symptom);
        }
    }
}
