using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMechanicShiftRepository : IGenericRepository<MechanicShift>
    {
        Task<bool> UpdateMechanicShiftAvailableAsync(Guid userId, Guid taskId);
        Task<bool> CreateMechanicShift(Guid userId, Guid taskId, Guid shiftId);
    }
}
