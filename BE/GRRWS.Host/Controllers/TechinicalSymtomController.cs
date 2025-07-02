using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.RequestDTO;
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
    }
}
