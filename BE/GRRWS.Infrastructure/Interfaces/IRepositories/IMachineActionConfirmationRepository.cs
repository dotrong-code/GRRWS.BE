using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMachineActionConfirmationRepository : IGenericRepository<MachineActionConfirmation>
    {
        Task<MachineActionConfirmation> GetByIdWithDetailsAsync(Guid id);
        Task<MachineActionConfirmation> GetByTaskIdAndTypeAsync(Guid? taskId, MachineActionType actionType);
        Task<(IEnumerable<MachineActionConfirmation> items, int totalCount)> GetAllAsync(
            int pageNumber, int pageSize, string? status, string? sortBy, bool isAscending);
    }
}
