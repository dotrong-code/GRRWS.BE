using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Machine;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMachineRepository : IGenericRepository<Machine>
    {
        Task<(List<Machine> Items, int TotalCount)> GetAllActiveMachinesAsync(int pageNumber, int pageSize);
        Task<(List<GetMachineResponse> Machines, int TotalCount)> GetAllMachinesAsync(
            string? machineName, string? machineCode, int pageNumber, int pageSize);
        Task<Machine> GetByIdAsync(Guid id);
        Task<bool> UpdateMachineAsync(UpdateMachineRequest updateMachineDto);
        Task<bool> DeleteAsync(Guid id);
    }
}

