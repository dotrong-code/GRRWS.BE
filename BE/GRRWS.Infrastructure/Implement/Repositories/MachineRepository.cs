using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
using GRRWS.Infrastructure.DTOs.Machine;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MachineRepository : GenericRepository<Machine>, IMachineRepository
    {
        public MachineRepository(GRRWSContext context) : base(context) { }
        public async Task<(List<Machine> Items, int TotalCount)> GetAllActiveMachinesAsync(int pageNumber, int pageSize)
        {
            var query = _context.Machines
                .Where(m => !m.IsDeleted);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(m => m.MachineName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task<(List<GetMachineResponse> Machines, int TotalCount)> GetAllMachinesAsync(
            string? machineName, string? machineCode, int pageNumber, int pageSize)
        {
            var query = _context.Machines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(machineName))
                query = query.Where(m => m.MachineName.Contains(machineName));

            if (!string.IsNullOrWhiteSpace(machineCode))
                query = query.Where(m => m.MachineCode.Contains(machineCode));

            query = query.Where(m => !m.IsDeleted).Include(m => m.Devices);

            int totalCount = await query.CountAsync();

            var machines = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new GetMachineResponse
                {
                    Id = m.Id,
                    MachineName = m.MachineName,
                    MachineCode = m.MachineCode,
                    Manufacturer = m.Manufacturer,
                    Model = m.Model,
                    Description = m.Description,
                    Status = m.Status,
                    ReleaseDate = m.ReleaseDate,
                    Specifications = m.Specifications,
                    PhotoUrl = m.PhotoUrl,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy,
                    ModifiedDate = m.ModifiedDate,
                    ModifiedBy = m.ModifiedBy,
                    IsDeleted = m.IsDeleted,
                    DeviceIds = m.Devices != null ? m.Devices.Select(d => d.Id).ToList() : new List<Guid>()
                })
                .ToListAsync();

            return (machines, totalCount);
        }
        public async Task<Machine> GetByIdAsync(Guid id)
        {
            return await _context.Machines
                .Include(e => e.Devices)
                .Include(i => i.MachineSpareparts)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }
        public async Task<bool> UpdateMachineAsync(UpdateMachineRequest updateDto)
        {
            var machine = await _context.Machines.FindAsync(updateDto.Id);
            if (machine == null)
            {
                return false;
            }

            machine.MachineName = updateDto.MachineName;
            machine.MachineCode = updateDto.MachineCode;
            machine.Manufacturer = updateDto.Manufacturer;
            machine.Model = updateDto.Model;
            machine.Description = updateDto.Description;
            machine.Status = updateDto.Status;
            machine.ReleaseDate = updateDto.ReleaseDate;
            machine.Specifications = updateDto.Specifications;
            machine.PhotoUrl = updateDto.PhotoUrl;
            _context.Machines.Update(machine);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return false;
            }
            machine.IsDeleted = true;
            _context.Machines.Update(machine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
