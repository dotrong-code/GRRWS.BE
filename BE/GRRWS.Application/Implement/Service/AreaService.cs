using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Area;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Zone;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class AreaService : IAreaService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<CreateAreaRequest> _createAreaValidator;
        private readonly IValidator<UpdateAreaRequest> _updateAreaValidator;
        private readonly IImportService _importService;
        public AreaService(UnitOfWork unitOfWork, IValidator<CreateAreaRequest> createAreaValidator, IValidator<UpdateAreaRequest> updateAreaValidator, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _createAreaValidator = createAreaValidator;
            _updateAreaValidator = updateAreaValidator;
            _importService = importService;
        }

        public async Task<Result> CreateAreaAsync(CreateAreaRequest request)
        {
            var validationResult = await _createAreaValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var area = new Area
            {
                Id = Guid.NewGuid(),
                AreaName = request.AreaName,
                CreatedDate = TimeHelper.GetHoChiMinhTime()
            };

            await _unitOfWork.AreaRepository.CreateAsync(area);
            return Result.SuccessWithObject(area);
        }

        public async Task<Result> GetAreaByIdAsync(Guid id)
        {
            var area = await _unitOfWork.AreaRepository.GetByIdAsync(id);
            if (area == null)
            {
                return Result.Failure(AreaErrorMessage.AreaNotExist());
            }

            var response = new GetAreaResponse
            {
                Id = area.Id,
                AreaName = area.AreaName,
                CreatedDate = area.CreatedDate,
                ModifiedDate = area.ModifiedDate
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllAreasAsync(int pageNumber, int pageSize)
        {
            var (areas, totalCount) = await _unitOfWork.AreaRepository.GetAllAreasAsync(pageNumber, pageSize);
            var areaResponses = areas.Select(a => new GetAreaResponse
            {
                Id = a.Id,
                AreaName = a.AreaName,
                CreatedDate = a.CreatedDate,
                ModifiedDate = a.ModifiedDate
            }).ToList();

            var response = new PagedResponse<GetAreaResponse>
            {
                Data = areaResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdateAreaAsync(UpdateAreaRequest request)
        {
            var validationResult = await _updateAreaValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var area = await _unitOfWork.AreaRepository.GetByIdAsync(request.Id);
            if (area == null)
            {
                return Result.Failure(AreaErrorMessage.AreaNotExist());
            }

            area.AreaName = request.AreaName;
            area.ModifiedDate = TimeHelper.GetHoChiMinhTime();

            await _unitOfWork.AreaRepository.UpdateAsync(area);
            return Result.SuccessWithObject(area);
        }

        public async Task<Result> DeleteAreaAsync(Guid id)
        {
            var result = await _unitOfWork.AreaRepository.DeleteAreaAsync(id);
            if (result == 0)
            {
                return Result.Failure(AreaErrorMessage.AreaDeleteFailed());
            }
            return Result.SuccessWithObject(result);
        }
        public async Task<Result> GetZonesByAreaAsync(Guid areaId, int pageNumber, int pageSize)
        {
            var area = await _unitOfWork.AreaRepository.GetByIdAsync(areaId);
            if (area == null)
            {
                return Result.Failure(AreaErrorMessage.AreaNotExist());
            }

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
        public async Task<Result> ImportAreasAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<Area>(file.OpenReadStream(), _unitOfWork.AreaRepository);
        }
    }
}
