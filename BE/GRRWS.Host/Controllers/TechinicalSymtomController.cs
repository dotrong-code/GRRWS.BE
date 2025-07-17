using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.TechnicalSymtom;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechinicalSymtomController : ControllerBase
    {
        private readonly ITechnicalSymtomService _technicalSymtomService;
        public TechinicalSymtomController(ITechnicalSymtomService technicalSymtomService)
        {
            _technicalSymtomService = technicalSymtomService;
        }
        [HttpGet("suggestions")]
        public async Task<IResult> GetSymtomSuggestions(string query, int maxResults)
        {
            var result = await _technicalSymtomService.GetSymtomSuggestionsAsync(query, maxResults);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("recommend")]
        public async Task<IResult> GetRecommendedErrors([FromBody] IssueIdsRequestDTO dto)
        {
            var result = await _technicalSymtomService.GetRecommendedSymtomsAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved recommended symtoms!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("import")]
        public async Task<IResult> ImportTechnicalSymptom(IFormFile file)
        {
            var result = await _technicalSymtomService.ImportTechnicalSymtomsAsync(file);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Technical symptoms imported successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("all")]
        public async Task<IResult> GetAllTechnicalSymptoms(int pageNumber = 1, int pageSize = 10, string? searchByName = null)
        {
            var result = await _technicalSymtomService.GetAllTechnicalSymtomsAsync(pageNumber, pageSize, searchByName);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all technical symptoms!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("update")]
        public async Task<IResult> UpdateTechnicalSymptom([FromBody] TechnicalSymptomUpdateDTO updateDTO)
        {
            var result = await _technicalSymtomService.UpdateTechnicalSymtomAsync(updateDTO);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Technical symptom updated successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IResult> DeleteTechnicalSymptom(Guid id)
        {
            var result = await _technicalSymtomService.DeleteTechnicalSymtomAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Technical symptom deleted successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var result = await _technicalSymtomService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved technical symptom by ID!")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
