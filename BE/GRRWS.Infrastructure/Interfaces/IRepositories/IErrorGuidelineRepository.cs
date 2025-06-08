using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorGuidelineRepository : IGenericRepository<ErrorGuideline>
    {
        Task<List<ErrorGuideline>> GetAllByErrorIdAsync(Guid errorId);
        Task<ErrorGuideline> GetByIdInclueErrorFixStepErrorSparepartssAsync(Guid id);
    }
}
