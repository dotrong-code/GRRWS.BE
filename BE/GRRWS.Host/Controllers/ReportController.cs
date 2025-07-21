using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Report;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }



        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsSuccess
? ResultExtensions.ToSuccessDetails(result, "Successfully")
: ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("with-issue-error")]
        public async Task<IResult> CreateReportWithIssueError([FromBody] ReportCreateWithIssueErrorDTO dto)
        {
            var result = await _service.CreateReportWithIssueError2Async(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Report created successfully with IssueErrors")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("with-issue-symptom")]
        public async Task<IResult> CreateReportWithIssueSymptom([FromBody] ReportCreateWithIssueSymtomDTO dto)
        {
            var result = await _service.CreateReportWithIssueSymtomAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Report created successfully with IssueSymptoms!")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("error-report/{id}")]
        public async Task<IResult> GetErrorReportById(Guid id)
        {
            var result = await _service.GetErrorReportByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved error report with details")
                : ResultExtensions.ToProblemDetails(result);
        }
    }

}
