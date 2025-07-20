using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.Position;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Task;
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
                CreatedDate = TimeHelper.GetHoChiMinhTime()
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
            position.ModifiedDate = TimeHelper.GetHoChiMinhTime();

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
        public async Task<Result> GetPositionsByAreaIdAsync(Guid areaId)
        {
            try
            {
                var positions = await _unitOfWork.PositionRepository.GetPositionsByAreaIdAsync(areaId);
                if (!positions.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"No positions found for AreaId: {areaId}."));
                }

                var response = new List<GetPositionByAreaResponse>();
                foreach (var position in positions)
                {
                    var positionResponse = new GetPositionByAreaResponse
                    {
                        PositionId = position.Id,
                        PositionName = $"{position.Zone?.Area?.AreaName} - {position.Zone?.ZoneName} - Vị trí {position.Index}"
                    };

                    // Current Device
                    if (position.DeviceId.HasValue)
                    {
                        var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(position.DeviceId.Value);
                        if (device != null)
                        {
                            positionResponse.CurrentDevice = new CurrentDeviceDetails
                            {
                                DeviceId = device.Id,
                                DeviceName = device.DeviceName ?? "N/A",
                                DeviceCode = device.DeviceCode ?? "N/A",
                                SerialNumber = device.SerialNumber ?? "N/A",
                                Model = device.Model ?? "N/A",
                                Status = device.Status.ToString(),
                                IsUnderWarranty = device.IsUnderWarranty
                            };
                        }
                    }

                    // Current Request (only Pending, Approved, or InProgress)
                    var request = await _unitOfWork.RequestRepository.GetActiveRequestByPositionIdAsync(position.Id);
                    if (request != null && new[] { Status.Pending, Status.Approved, Status.InProgress }.Contains(request.Status))
                    {
                        positionResponse.CurrentRequest = new CurrentRequestDetails
                        {
                            RequestId = request.Id,
                            RequestTitle = request.RequestTitle ?? "N/A",
                            Description = request.Description ?? "No description",
                            Status = request.Status.ToString(),
                            IsSolved = request.IsSovled,
                            DueDate = request.DueDate,
                            Priority = request.Priority.ToString(),
                            IsNeedSign = request.IsNeedSign
                        };
                    }

                    response.Add(positionResponse);
                }

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", $"Failed to retrieve positions: {ex.Message}"));
            }
        }
        public async Task<Result> GetRequestsByPositionIdAsync(Guid positionId)
        {
            try
            {
                // Check if position exists
                var position = await _unitOfWork.PositionRepository.GetByIdAsync(positionId);
                if (position == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"Position with ID {positionId} not found."));
                }

                var requests = await _unitOfWork.PositionRepository.GetRequestsByPositionIdAsync(positionId);
                if (!requests.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"No requests found for PositionId: {positionId}."));
                }

                var response = requests.Select(r => new GetRequestResponse
                {
                    Id = r.Id,
                    RequestTitle = r.RequestTitle ?? "N/A",
                    Description = r.Description ?? "No description",
                    Status = r.Status.ToString(),
                    IsSolved = r.IsSovled,
                    DueDate = r.DueDate,
                    Priority = r.Priority.ToString(),
                    IsNeedSign = r.IsNeedSign,
                    DeviceId = r.DeviceId,
                    DeviceName = r.Device?.DeviceName ?? "N/A",
                    RequestedById = r.RequestedById,
                    SenderName = r.Sender?.FullName ?? "N/A",
                    PositionId = r.PositionId,
                    CreatedDate = r.CreatedDate,
                    ModifiedDate = r.ModifiedDate
                }).ToList();

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", $"Failed to retrieve requests: {ex.Message}"));
            }
        }
        public async Task<Result> GetTaskConfirmationsByPositionIdAsync(Guid positionId)
        {
            try
            {
                // Check if position exists
                var position = await _unitOfWork.PositionRepository.GetByIdAsync(positionId);
                if (position == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"Position with ID {positionId} not found."));
                }

                var taskConfirmations = await _unitOfWork.PositionRepository.GetTaskConfirmationsByPositionIdAsync(positionId);
                if (!taskConfirmations.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"No task confirmations found for PositionId: {positionId}."));
                }

                var response = taskConfirmations.Select(tc => new GetTaskConfirmationResponse
                {
                    Id = tc.Id,
                    TaskId = tc.TaskId,
                    TaskName = tc.Task?.TaskName ?? "N/A",
                    SignerId = tc.SignerId,
                    SignerName = tc.Signer?.FullName ?? "N/A",
                    SignerRole = tc.SignerRole,
                    SignatureBase64 = tc.SignatureBase64 ?? "N/A",
                    DeviceSerial = tc.DeviceSerial ?? "N/A",
                    DeviceModel = tc.DeviceModel ?? "N/A",
                    DeviceCondition = tc.DeviceCondition ?? "N/A",
                    ConfirmationType = tc.ConfirmationType,
                    Notes = tc.Notes ?? "No notes",
                    CreatedDate = tc.CreatedDate,
                    ModifiedDate = tc.ModifiedDate
                }).ToList();

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", $"Failed to retrieve task confirmations: {ex.Message}"));
            }
        }

    }
}
