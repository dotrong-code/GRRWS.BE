using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<List<Report>> GetReportsWithRequestAsync();
        Task<Report> GetReportWithRequestAsync(Guid id);
        Task<List<Report>> GetAllReportAsync();
        Task<Report> GetReportByIdAsync(Guid id);
        Task UpdateReportAsync(Report updatedReport, List<Guid> newErrorIds);
        Task<Report> GetReportWithErrorDetailsAsync(Guid id);
    }

}
