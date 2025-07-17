using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDTO;
using GRRWS.Infrastructure.DTOs.Machine;
using GRRWS.Infrastructure.DTOs.Paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = GRRWS.Infrastructure.DTOs.Common.Error;

namespace GRRWS.Application.Implement.Service
{
    public class MachineService : IMachineService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IImportService _importService;

        public MachineService(UnitOfWork unitOfWork, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _importService = importService;
        }

        public async Task<Result> ImportMachinesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<Machine>(file.OpenReadStream(), _unitOfWork.MachineRepository);
        }

        public async Task<Result> GetAllMachinesAsync(string? machineName, string? machineCode, int pageNumber, int pageSize)
        {
            var (machines, totalCount) = await _unitOfWork.MachineRepository.GetAllMachinesAsync(machineName, machineCode, pageNumber, pageSize);

            var machineResponses = machines.Select(m => new GetMachineResponse
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
                DeviceIds = m.DeviceIds
            }).ToList();

            var response = new PagedResponse<GetMachineResponse>
            {
                Data = machineResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }
        public async Task<Result> UpdateMachineAsync(UpdateMachineRequest updateDTO)
        {
            if (updateDTO == null)
            {
                return Result.Failure(Error.Validation("InvalidUpdateData", "Update data cannot be null."));
            }

            var isUpdated = await _unitOfWork.MachineRepository.UpdateMachineAsync(updateDTO);
            if (!isUpdated)
            {
                return Result.Failure(Error.Validation("UpdateFailed", "Failed to update the machine."));
            }
            return Result.SuccessWithObject("Machine updated successfully!");
        }
        public async Task<Result> DeleteMachineAsync(Guid id)
        {
            var isDeleted = await _unitOfWork.MachineRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return Result.Failure(Error.Validation("DeleteFailed", "Failed to delete the machine."));
            }
            return Result.SuccessWithObject("Machine deleted successfully!");
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var getMachine = await _unitOfWork.MachineRepository.GetByIdAsync(id);
            if (getMachine == null)
            {
                return Result.Failure(Error.NotFound("MachineNotFound", "Machine not found."));
            }
            var machine = new GetMachineResponse
            {
                Id = getMachine.Id,
                MachineName = getMachine.MachineName,
                MachineCode = getMachine.MachineCode,
                Manufacturer = getMachine.Manufacturer,
                Model = getMachine.Model,
                Description = getMachine.Description,
                Status = getMachine.Status,
                ReleaseDate = getMachine.ReleaseDate,
                Specifications = getMachine.Specifications,
                PhotoUrl = getMachine.PhotoUrl,
                CreatedDate = getMachine.CreatedDate,
                CreatedBy = getMachine.CreatedBy,
                ModifiedDate = getMachine.ModifiedDate,
                ModifiedBy = getMachine.ModifiedBy,
                IsDeleted = getMachine.IsDeleted,
                DeviceIds = getMachine.Devices != null ? getMachine.Devices.Select(d => d.Id).ToList() : new List<Guid>()
            };
            return Result.SuccessWithObject(machine);
        }
    }
}
