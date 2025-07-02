using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Position;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class PositionService : IPositionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<CreatePositionRequest> _createPositionValidator;
        private readonly IValidator<UpdatePositionRequest> _updatePositionValidator;
        private readonly IImportService _importService;
        public PositionService(UnitOfWork unitOfWork, IValidator<CreatePositionRequest> createPositionValidator, IValidator<UpdatePositionRequest> updatePositionValidator, IImportService importService)
        {
            _unitOfWork = unitOfWork;
            _createPositionValidator = createPositionValidator;
            _updatePositionValidator = updatePositionValidator;
            _importService = importService;
        }

        public async Task<Result> CreatePositionAsync(CreatePositionRequest request)
        {
            var validationResult = await _createPositionValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            

            var position = new Position
            {
                Id = Guid.NewGuid(),
                Index = request.Index,
                ZoneId = request.ZoneId,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.PositionRepository.CreateAsync(position);
            return Result.SuccessWithObject(position);
        }

        public async Task<Result> GetPositionByIdAsync(Guid id)
        {
            var position = await _unitOfWork.PositionRepository.GetByIdAsync(id);
            if (position == null)
            {
                return Result.Failure(PositionErrorMessage.PositionNotExist());
            }

            var response = new GetPositionResponse
            {
                Id = position.Id,
                Index = position.Index,
                ZoneId = position.ZoneId,
                DeviceId = position.DeviceId,
                CreatedDate = position.CreatedDate,
                ModifiedDate = position.ModifiedDate
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllPositionsAsync(Guid? zoneId, int pageNumber, int pageSize)
        {
            var (positions, totalCount) = await _unitOfWork.PositionRepository.GetAllPositionsAsync(zoneId, pageNumber, pageSize);
            var positionResponses = positions.Select(p => new GetPositionResponse
            {
                Id = p.Id,
                Index = p.Index,
                ZoneId = p.ZoneId,
                DeviceId = p.DeviceId,
                CreatedDate = p.CreatedDate,
                ModifiedDate = p.ModifiedDate
            }).ToList();

            var response = new PagedResponse<GetPositionResponse>
            {
                Data = positionResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdatePositionAsync(UpdatePositionRequest request)
        {
            var validationResult = await _updatePositionValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var position = await _unitOfWork.PositionRepository.GetByIdAsync(request.Id);
            if (position == null)
            {
                return Result.Failure(PositionErrorMessage.PositionNotExist());
            }

            

            position.Index = request.Index;
            position.ZoneId = request.ZoneId;
            position.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.PositionRepository.UpdateAsync(position);
            return Result.SuccessWithObject(position);
        }

        public async Task<Result> DeletePositionAsync(Guid id)
        {
            var result = await _unitOfWork.PositionRepository.DeletePositionAsync(id);
            if (result == 0)
            {
                return Result.Failure(PositionErrorMessage.PositionDeleteFailed());
            }
            return Result.SuccessWithObject(result);
        }
        public async Task<Result> ImportPositionsAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
            }

            return await _importService.ImportAsync<Position>(file.OpenReadStream(), _unitOfWork.PositionRepository);
        }
    }
}
