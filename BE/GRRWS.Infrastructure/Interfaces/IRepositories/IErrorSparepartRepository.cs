using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IErrorSparepartRepository : IGenericRepository<ErrorSparepart>
    {
        Task<List<ErrorSparepart>> GetByErrorGuidelineIdAsync(Guid errorGuidelineId);
        Task<List<ErrorSparepart>> GetByErrorGuidelineIdsAsync(IEnumerable<Guid> guidelineIds);
    }
}
