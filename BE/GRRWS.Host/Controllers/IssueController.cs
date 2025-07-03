using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }
        [HttpGet("suggestions")]
        public async Task<IResult> GetIssueSuggestions(string query, int maxResults)
        {
            var result = await _issueService.GetIssueSuggestionsAsync(query, maxResults);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("critical-issue")]
        public async Task<IResult> GetCriticalMachineIssue()
        {
            var criticalIssueId = Guid.Parse("deadbeef-dead-beef-dead-beefdeadbeef");
            var result = await _issueService.GetIssueByIdAsync(criticalIssueId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("import")]
        public async Task<IResult> ImportIssue(IFormFile file)
        {
            var result = await _issueService.ImportIssuesAsync(file);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Issues imported successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
