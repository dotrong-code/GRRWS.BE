using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IReportService
    {
        Task<Result> CreateAsync(ReportCreateDTO dto);
        Task<Result> CreateWarrantyReportAsync(ReportWarrantyCreateDTO dto);
        Task<Result> UpdateAsync(ReportUpdateDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllAsync();
        Task<Result> CreateReportWithIssueErrorAsync(ReportCreateWithIssueErrorDTO dto);
    }

}
