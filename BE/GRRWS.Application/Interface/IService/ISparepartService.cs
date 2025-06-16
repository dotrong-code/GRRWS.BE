using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Sparepart;
using System;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ISparepartService
    {
        Task<Result> GetAllAsync(int pageNumber, int pageSize, string? searchSparepartName = null, string? searchCategory = null);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateSparepartDTO dto);
        Task<Result> UpdateAsync(Guid id, UpdateSparepartDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetAvailabilityAsync(int pageNumber, int pageSize);
        Task<Result> GetSparepartsByMachineIdAsync(Guid machineId, int pageNumber, int pageSize, string? searchSparepartName = null, string? searchCategory = null);
        Task<Result> GetSparepartsBySupplierAsync(Guid supplierId, int pageNumber, int pageSize, string? searchSparepartName = null, string? searchCategory = null);
        Task<Result> GetLowStockSparepartsAsync(int pageNumber, int pageSize, string? searchSparepartName = null, string? searchCategory = null);
        Task<Result> GetOutOfStockSparepartsAsync(int pageNumber, int pageSize, string? searchSparepartName = null, string? searchCategory = null);
        Task<Result> GetAllMachinesAsync(int pageNumber, int pageSize);
        Task<Result> GetAllSuppliersAsync(int pageNumber, int pageSize);
        Task<Result> UpdateStockQuantityAsync(UpdateSparepartStockQuantityRequest dto);
    }
}