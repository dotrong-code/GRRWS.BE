using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.DTOs.Machine;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.DTOs.Supplier;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class SparepartService : ISparepartService
    {
        private readonly IUnitOfWork _unit;
        private readonly IFirebaseService _firebaseService;

        public SparepartService(IUnitOfWork unit, IFirebaseService firebaseService)
        {
            _unit = unit;
            _firebaseService = firebaseService;
        }

        public async Task<Result> GetAllAsync(int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var (items, totalCount) = await _unit.SparepartRepository.GetAllActiveSparepartsAsync(pageNumber, pageSize, searchSparepartName);
            var dtos = new List<SparepartViewDTO>();
            foreach (var sp in items)
            {
                var dto = await MapSparepartToDTOAsync(sp);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdWithDetailsAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));

            var dto = await MapSparepartToDTOAsync(sparepart);
            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> CreateAsync(CreateSparepartDTO dto)
        {
            string? imgUrl = null;
            if (dto.ImageFile != null)
            {
                var imageRequest = new AddImageRequest(dto.ImageFile, "Spareparts");
                var uploadResult = await _unit.FirebaseRepository.UploadImageAsync(imageRequest);
                if (uploadResult.Success)
                {
                    imgUrl = uploadResult.FilePath;
                }
                else
                {
                    return Result.Failure(uploadResult.Error);
                }
            }

            var sparepart = new Sparepart
            {
                SparepartCode = dto.SparepartCode,
                SparepartName = dto.SparepartName,
                Description = dto.Description,
                Specification = dto.Specification,
                StockQuantity = dto.StockQuantity,
                Unit = dto.Unit,
                UnitPrice = dto.UnitPrice,
                ExpectedAvailabilityDate = dto.StockQuantity >= 0 ? dto.ExpectedAvailabilityDate : null,
                IsAvailable = dto.StockQuantity > 0,
                SupplierId = dto.SupplierId,
                Category = dto.Category,
                ImgUrl = imgUrl
            };

            await _unit.SparepartRepository.CreateAsync(sparepart);
            await _unit.SaveChangesAsync();

            var resultDto = await MapSparepartToDTOAsync(sparepart);
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
            sparepart.IsAvailable = dto.StockQuantity > 0;
            sparepart.SupplierId = dto.SupplierId;
            sparepart.Category = dto.Category;

            if (dto.ImageFile != null)
            {
                var imageRequest = new AddImageRequest(dto.ImageFile, "Spareparts");
                var uploadResult = await _unit.FirebaseRepository.UploadImageAsync(imageRequest);
                if (uploadResult.Success)
                {
                    sparepart.ImgUrl = uploadResult.FilePath;
                }
            }

            await _unit.SparepartRepository.UpdateAsync(sparepart);
            await _unit.SaveChangesAsync();

            var resultDto = await MapSparepartToDTOAsync(sparepart);
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

        public async Task<Result> GetAvailabilityAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unit.SparepartRepository.GetAllActiveSparepartsAsync(pageNumber, pageSize);
            var dtos = new List<SparepartViewDTO>();
            foreach (var sp in items)
            {
                var dto = await MapSparepartToDTOAsync(sp);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetSparepartsByMachineIdAsync(Guid machineId, int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var (machineSpareparts, totalCount) = await _unit.MachineSparepartRepository.GetSparepartsByMachineIdAsync(machineId, pageNumber, pageSize, searchSparepartName);
           

            var dtos = new List<SparepartViewDTO>();
            foreach (var ms in machineSpareparts)
            {
                var dto = await MapSparepartToDTOAsync(ms.Sparepart);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetSparepartsBySupplierAsync(Guid supplierId, int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var (items, totalCount) = await _unit.SparepartRepository.GetSparepartsBySupplierIdAsync(supplierId, pageNumber, pageSize, searchSparepartName);
            

            var dtos = new List<SparepartViewDTO>();
            foreach (var sp in items)
            {
                var dto = await MapSparepartToDTOAsync(sp);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetLowStockSparepartsAsync(int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var (items, totalCount) = await _unit.SparepartRepository.GetLowStockSparepartsAsync(pageNumber, pageSize, searchSparepartName);
            var dtos = new List<SparepartViewDTO>();
            foreach (var sp in items)
            {
                var dto = await MapSparepartToDTOAsync(sp);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetOutOfStockSparepartsAsync(int pageNumber, int pageSize, string? searchSparepartName = null)
        {
            var (items, totalCount) = await _unit.SparepartRepository.GetOutOfStockSparepartsAsync(pageNumber, pageSize, searchSparepartName);
            var dtos = new List<SparepartViewDTO>();
            foreach (var sp in items)
            {
                var dto = await MapSparepartToDTOAsync(sp);
                dtos.Add(dto);
            }
            var response = new PagedResponse<SparepartViewDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllMachinesAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unit.MachineRepository.GetAllActiveMachinesAsync(pageNumber, pageSize);
            var dtos = items.Select(m => new MachineDTO
            {
                Id = m.Id,
                MachineName = m.MachineName
            }).ToList();

            var response = new PagedResponse<MachineDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllSuppliersAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unit.SupplierRepository.GetAllActiveSuppliersAsync(pageNumber, pageSize);
            var dtos = items.Select(s => new SupplierDTO
            {
                Id = s.Id,
                SupplierName = s.SupplierName,
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                LinkWeb = s.LinkWeb
            }).ToList();

            var response = new PagedResponse<SupplierDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        private async Task<SparepartViewDTO> MapSparepartToDTOAsync(Sparepart sp)
        {
            string? imgUrl = null;
            if (!string.IsNullOrEmpty(sp.ImgUrl))
            {
                var getImageRequest = new GetImageRequest(sp

.ImgUrl);
                var imageResult = await _unit.FirebaseRepository.GetImageAsync(getImageRequest);
                if (imageResult.Success && !string.IsNullOrEmpty(imageResult.ImageUrl))
                {
                    imgUrl = imageResult.ImageUrl;
                }
            }

            return new SparepartViewDTO
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
                SupplierId = sp.SupplierId,
                SupplierName = sp.Supplier?.SupplierName,
                Category = sp.Category,
                MachineIds = sp.MachineSpareparts?.Select(ms => ms.MachineId).ToList(),
                MachineNames = sp.MachineSpareparts?.Select(ms => ms.Machine?.MachineName).ToList(),
                ImgUrl = imgUrl
            };
        }
    }
}