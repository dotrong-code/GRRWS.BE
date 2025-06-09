using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMachineSparepartRepository : IGenericRepository<MachineSparepart>
    {
        Task<(List<MachineSparepart> Items, int TotalCount)> GetSparepartsByMachineIdAsync(Guid machineId, int pageNumber, int pageSize);
    }

}
