using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class SparepartService : ISparepartService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public SparepartService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<Result> GetAllAsync()
        {
            var spareparts = await _unit.SparepartRepository.GetAllAsync();
            var dtos = _mapper.Map<List<SparepartViewDTO>>(spareparts);
            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));
            var dto = _mapper.Map<SparepartViewDTO>(sparepart);
            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> CreateAsync(CreateSparepartDTO dto)
        {
            var sparepart = _mapper.Map<Sparepart>(dto);
            sparepart.IsAvailable = dto.StockQuantity > 0;
            sparepart.ExpectedAvailabilityDate = dto.StockQuantity == 0 ? (DateTime?)null : dto.ExpectedAvailabilityDate; // Đặt null nếu có sẵn
            await _unit.SparepartRepository.CreateAsync(sparepart);
            await _unit.SaveChangesAsync();
            var resultDto = _mapper.Map<SparepartViewDTO>(sparepart);
            return Result.SuccessWithObject(resultDto);
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateSparepartDTO dto)
        {
            var sparepart = await _unit.SparepartRepository.GetByIdAsync(id);
            if (sparepart == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("NotFound", "Sparepart not found.", 0));
            _mapper.Map(dto, sparepart);
            sparepart.IsAvailable = dto.StockQuantity > 0;
            sparepart.ExpectedAvailabilityDate = dto.StockQuantity == 0 ? dto.ExpectedAvailabilityDate : null; // Cập nhật nếu không có sẵn
            await _unit.SparepartRepository.UpdateAsync(sparepart);
            await _unit.SaveChangesAsync();
            var resultDto = _mapper.Map<SparepartViewDTO>(sparepart);
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
                IsAvailable = s.StockQuantity > 0,
                ExpectedAvailabilityDate = s.StockQuantity == 0 ? s.ExpectedAvailabilityDate : null
            }).ToList();

            return Result.SuccessWithObject(availability);
        }
    }
}
