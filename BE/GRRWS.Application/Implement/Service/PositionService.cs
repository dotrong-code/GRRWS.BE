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
        public async Task<Result> GetAllPositionDetailsAsync(Guid? areaId = null)
        {
            try
            {
                var positions = await _unitOfWork.PositionRepository.GetAllPositionsWithDetailsAsync();
                if (areaId.HasValue)
                {
                    positions = positions.Where(p => p.Zone?.AreaId == areaId.Value).ToList();
                }

                if (!positions.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No positions found for the specified area."));
                }

                var response = new List<GetPositionDetailsResponse>();
                foreach (var position in positions)
                {
                    var positionResponse = new GetPositionDetailsResponse
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
                                Serial = device.SerialNumber ?? "N/A",
                                Model = device.Model ?? "N/A",
                                Status = device.Status switch
                                {
                                    DeviceStatus.InUse => "InUse",
                                    DeviceStatus.InWarranty => "WarrantyOut",
                                    _ => device.InUsed == true ? "Temporary" : "Unknown"
                                }
                            };
                        }
                    }

                    // Current Request
                    var request = await _unitOfWork.RequestRepository.GetActiveRequestByPositionIdAsync(position.Id);
                    if (request != null)
                    {
                        var taskGroup = await _unitOfWork.TaskGroupRepository.GetByRequestIdAsync(request.Id);
                        var tasks = taskGroup != null ? await _unitOfWork.TaskRepository.GetTasksByTaskGroupIdAsync(taskGroup.Id) : new List<Tasks>();
                        var warrantyClaim = tasks.FirstOrDefault(t => t.WarrantyClaimId.HasValue)?.WarrantyClaim;
                        var requestMachine = tasks.FirstOrDefault(t => t.RequestMachineReplacement != null)?.RequestMachineReplacement;

                        positionResponse.CurrentRequest = new CurrentRequestDetails
                        {
                            RequestId = request.Id,
                            Status = request.Status == Status.Completed ? "Done" : "InProgress",
                            IsSolved = request.IsSovled,
                            ExpectedReturnDate = warrantyClaim?.ExpectedReturnDate,
                            Note = request.CompletedDetails ?? warrantyClaim?.WarrantyNotes ?? "No notes",
                            OldDevice = requestMachine != null && requestMachine.OldDeviceId != null
                                ? new DeviceDetails
                                {
                                    Serial = requestMachine.OldDevice?.SerialNumber ?? "N/A",
                                    Model = requestMachine.OldDevice?.Model ?? "N/A"
                                }
                                : null,
                            TemporaryDevice = requestMachine != null && requestMachine.NewDeviceId.HasValue
                                ? new DeviceDetails
                                {
                                    Serial = requestMachine.NewDevice?.SerialNumber ?? "N/A",
                                    Model = requestMachine.NewDevice?.Model ?? "N/A"
                                }
                                : null,
                            Handover = new HandoverDetails
                            {
                                Staff = requestMachine?.Assignee?.FullName ?? "N/A",
                                Status = requestMachine != null
                                    ? (requestMachine.AssigneeConfirm && requestMachine.StokkKeeperConfirm ? "Delivered" : "Awaiting")
                                    : "N/A"
                            }
                        };
                    }

                    response.Add(positionResponse);
                }

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", $"Failed to retrieve position details: {ex.Message}"));
            }
        }


    }
}
