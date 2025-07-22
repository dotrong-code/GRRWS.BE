using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Report;

namespace GRRWS.Application.Interface.IService
{
    public interface IReportService
    {

        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllAsync();
        Task<Result> CreateReportWithIssueErrorAsync(ReportCreateWithIssueErrorDTO dto);
        Task<Result> CreateReportWithIssueSymtomAsync(ReportCreateWithIssueSymtomDTO dto);
        Task<Result> GetErrorReportByIdAsync(Guid id);
        Task<Result> CreateReportWithIssueError2Async(ReportCreateWithIssueErrorDTO dto);
    }

}
