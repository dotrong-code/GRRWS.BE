using GRRWS.Application.Common.Result;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Sparepart;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ISparepartService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateSparepartDTO dto);
        Task<Result> UpdateAsync(Guid id, UpdateSparepartDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetAvailabilityAsync();
        Task<Result> GetSparepartsByMachineIdAsync(Guid machineId);
        Task<Result> GetSparepartsBySupplierAsync(Guid supplierId);
    }
}