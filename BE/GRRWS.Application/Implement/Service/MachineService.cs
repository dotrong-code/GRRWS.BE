using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
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

       
    }
}
