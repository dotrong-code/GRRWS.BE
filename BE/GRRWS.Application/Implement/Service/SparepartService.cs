using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class SparepartService : ISparepartService
    {
        private readonly IUnitOfWork _unit;

        public SparepartService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Result> GetAllAsync()
        {
            var spareparts = await _unit.SparepartRepository.GetAllAsync();
            var dtos = spareparts.Select(sp => new SparepartViewDTO
            {
                Id = sp.Id,
                SparepartCode = sp.SparepartCode,
                SparepartName = sp.SparepartName,
                Description = sp.Description,
                Specification = sp.Specification,
                StockQuantity = sp.StockQuantity,
                IsAvailable = sp.StockQuantity > 0, // Đổi logic: StockQuantity < 0 thì IsAvailable = true
                Unit = sp.Unit,
                UnitPrice = sp.UnitPrice,
                ExpectedAvailabilityDate = sp.ExpectedAvailabilityDate,
                SupplierName = sp.Supplier?.SupplierName // Thêm thông tin Supplier
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));

            var dto = new SparepartViewDTO
            {
                Id = sparepart.Id,
                SparepartCode = sparepart.SparepartCode,
                SparepartName = sparepart.SparepartName,
                Description = sparepart.Description,
                Specification = sparepart.Specification,
                StockQuantity = sparepart.StockQuantity,
                IsAvailable = sparepart.StockQuantity > 0, // Đổi logic
                Unit = sparepart.Unit,
                UnitPrice = sparepart.UnitPrice,
                ExpectedAvailabilityDate = sparepart.ExpectedAvailabilityDate,
                SupplierName = sparepart.Supplier?.SupplierName
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> CreateAsync(CreateSparepartDTO dto)
        {
            var sparepart = new Sparepart
            {
                SparepartCode = dto.SparepartCode,
                SparepartName = dto.SparepartName,
                Description = dto.Description,
                Specification = dto.Specification,
                StockQuantity = dto.StockQuantity,
                Unit = dto.Unit,
                UnitPrice = dto.UnitPrice,
                ExpectedAvailabilityDate = dto.StockQuantity >= 0 ? dto.ExpectedAvailabilityDate : null, // Nếu Stock < 0, không cần ngày dự kiến
                IsAvailable = dto.StockQuantity > 0, // Đổi logic
                SupplierId = dto.SupplierId // Thêm SupplierId
            };

            await _unit.SparepartRepository.CreateAsync(sparepart);
            await _unit.SaveChangesAsync();

            var resultDto = new SparepartViewDTO
            {
                Id = sparepart.Id,
                SparepartCode = sparepart.SparepartCode,
                SparepartName = sparepart.SparepartName,
                Description = sparepart.Description,
                Specification = sparepart.Specification,
                StockQuantity = sparepart.StockQuantity,
                IsAvailable = sparepart.StockQuantity > 0,
                Unit = sparepart.Unit,
                UnitPrice = sparepart.UnitPrice,
                ExpectedAvailabilityDate = sparepart.ExpectedAvailabilityDate,
                SupplierName = sparepart.Supplier?.SupplierName
            };

            return Result.SuccessWithObject(resultDto);
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateSparepartDTO dto)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));

            sparepart.SparepartName = dto.SparepartName;
            sparepart.Description = dto.Description;
            sparepart.Specification = dto.Specification;
            sparepart.StockQuantity = dto.StockQuantity;
            sparepart.Unit = dto.Unit;
            sparepart.UnitPrice = dto.UnitPrice;
            sparepart.ExpectedAvailabilityDate = dto.StockQuantity >= 0 ? dto.ExpectedAvailabilityDate : null;
            sparepart.IsAvailable = dto.StockQuantity > 0; // Đổi logic
            sparepart.SupplierId = dto.SupplierId; // Cập nhật SupplierId

            await _unit.SparepartRepository.UpdateAsync(sparepart);
            await _unit.SaveChangesAsync();

            var resultDto = new SparepartViewDTO
            {
                Id = sparepart.Id,
                SparepartCode = sparepart.SparepartCode,
                SparepartName = sparepart.SparepartName,
                Description = sparepart.Description,
                Specification = sparepart.Specification,
                StockQuantity = sparepart.StockQuantity,
                IsAvailable = sparepart.StockQuantity > 0,
                Unit = sparepart.Unit,
                UnitPrice = sparepart.UnitPrice,
                ExpectedAvailabilityDate = sparepart.ExpectedAvailabilityDate,
                SupplierName = sparepart.Supplier?.SupplierName
            };

            return Result.SuccessWithObject(resultDto);
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));
            await _unit.SparepartRepository.RemoveAsync(sparepart);
            await _unit.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> GetAvailabilityAsync()
        {
            var spareparts = await _unit.SparepartRepository.GetAllAsync();
            var availability = spareparts.Select(s => new SparepartAvailabilityDTO
            {
                Id = s.Id,
                SparepartCode = s.SparepartCode,
                SparepartName = s.SparepartName,
                IsAvailable = s.StockQuantity > 0, // Đổi logic
                ExpectedAvailabilityDate = s.StockQuantity >= 0 ? s.ExpectedAvailabilityDate : null
            }).ToList();

            return Result.SuccessWithObject(availability);
        }

        public async Task<Result> GetSparepartsByMachineIdAsync(Guid machineId)
        {
            var machineSpareparts = await _unit.MachineSparepartRepository.GetSparepartsByMachineIdAsync(machineId);
            if (machineSpareparts == null || !machineSpareparts.Any())
                return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "No spareparts found for this machine.", 0));

            var dtos = machineSpareparts.Select(ms => new MachineSparepartDTO
            {
                SparepartId = ms.SparepartId,
                SparepartName = ms.Sparepart.SparepartName,
                MachineId = ms.MachineId,
                
                StockQuantity = ms.Sparepart.StockQuantity,
                IsAvailable = ms.Sparepart.StockQuantity > 0,
                SupplierName = ms.Sparepart.Supplier?.SupplierName
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetSparepartsBySupplierAsync(Guid supplierId)
        {
            var spareparts = await _unit.SparepartRepository.GetSparepartsBySupplierIdAsync(supplierId);
            if (spareparts == null || !spareparts.Any())
                return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "No spareparts found for this supplier.", 0));

            var dtos = spareparts.Select(sp => new SparepartViewDTO
            {
                Id = sp.Id,
                SparepartCode = sp.SparepartCode,
                SparepartName = sp.SparepartName,
                Description = sp.Description,
                Specification = sp.Specification,
                StockQuantity = sp.StockQuantity,
                IsAvailable = sp.StockQuantity > 0,
                Unit = sp.Unit,
                UnitPrice = sp.UnitPrice,
                ExpectedAvailabilityDate = sp.ExpectedAvailabilityDate,
                SupplierName = sp.Supplier?.SupplierName
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }
    }
}