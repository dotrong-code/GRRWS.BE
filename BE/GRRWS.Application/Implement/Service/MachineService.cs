using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Machine;
using GRRWS.Infrastructure.DTOs.Paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
