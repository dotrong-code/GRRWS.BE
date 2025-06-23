using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IShiftRepository : IGenericRepository<Shift>
    {
        Task<Shift> GetCurrentShiftAsync(DateTime currentTime);
        Task<Shift> GetNearestShiftAsync(DateTime currentTime);
        Task<List<Shift>> GetShiftsByTimeRangeAsync(TimeSpan startTime, TimeSpan endTime);
    }
}