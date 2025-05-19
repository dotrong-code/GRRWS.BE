using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Zone;

namespace GRRWS.Application.Implement.Service
{
    public class ZoneService : IZoneService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<CreateZoneRequest> _createZoneValidator;
        private readonly IValidator<UpdateZoneRequest> _updateZoneValidator;

        public ZoneService(UnitOfWork unitOfWork, IValidator<CreateZoneRequest> createZoneValidator, IValidator<UpdateZoneRequest> updateZoneValidator)
        {
            _unitOfWork = unitOfWork;
            _createZoneValidator = createZoneValidator;
            _updateZoneValidator = updateZoneValidator;
        }

        public async Task<Result> CreateZoneAsync(CreateZoneRequest request)
        {
            var validationResult = await _createZoneValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            

            var zone = new Zone
            {
                Id = Guid.NewGuid(),
                ZoneName = request.ZoneName,
                AreaId = request.AreaId,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.ZoneRepository.CreateAsync(zone);
            return Result.SuccessWithObject(zone);
        }

        public async Task<Result> GetZoneByIdAsync(Guid id)
        {
            var zone = await _unitOfWork.ZoneRepository.GetByIdAsync(id);
            if (zone == null)
            {
                return Result.Failure(ZoneErrorMessage.ZoneNotExist());
            }

            var response = new GetZoneResponse
            {
                Id = zone.Id,
                ZoneName = zone.ZoneName,
                AreaId = zone.AreaId,
                CreatedDate = zone.CreatedDate,
                ModifiedDate = zone.ModifiedDate
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllZonesAsync(Guid? areaId, int pageNumber, int pageSize)
        {
            var (zones, totalCount) = await _unitOfWork.ZoneRepository.GetAllZonesAsync(areaId, pageNumber, pageSize);
            var zoneResponses = zones.Select(z => new GetZoneResponse
            {
                Id = z.Id,
                ZoneName = z.ZoneName,
                AreaId = z.AreaId,
                CreatedDate = z.CreatedDate,
                ModifiedDate = z.ModifiedDate
            }).ToList();

            var response = new PagedResponse<GetZoneResponse>
            {
                Data = zoneResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdateZoneAsync(UpdateZoneRequest request)
        {
            var validationResult = await _updateZoneValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var zone = await _unitOfWork.ZoneRepository.GetByIdAsync(request.Id);
            if (zone == null)
            {
                return Result.Failure(ZoneErrorMessage.ZoneNotExist());
            }

            

            zone.ZoneName = request.ZoneName;
            zone.AreaId = request.AreaId;
            zone.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.ZoneRepository.UpdateAsync(zone);
            return Result.SuccessWithObject(zone);
        }

        public async Task<Result> DeleteZoneAsync(Guid id)
        {
            var result = await _unitOfWork.ZoneRepository.DeleteZoneAsync(id);
            if (result == 0)
            {
                return Result.Failure(ZoneErrorMessage.ZoneDeleteFailed());
            }
            return Result.SuccessWithObject(result);
        }
    }
}
