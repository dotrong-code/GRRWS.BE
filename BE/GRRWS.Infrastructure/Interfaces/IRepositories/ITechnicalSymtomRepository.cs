using GRRWS.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ITechnicalSymtomRepository
    {
        Task<List<SuggestObject>> GetNotFoundTechnicalSymtomDisplayNamesAsync(IEnumerable<Guid> technicalSymtomIds);
    }
}
