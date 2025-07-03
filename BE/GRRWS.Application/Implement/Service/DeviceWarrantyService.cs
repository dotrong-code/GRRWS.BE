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
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;
using GRRWS.Infrastructure.DTOs.Paging;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceWarrantyService : IDeviceWarrantyService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<CreateDeviceWarrantyRequest> _createDeviceWarrantyValidator;
        private readonly IValidator<UpdateDeviceWarrantyRequest> _updateDeviceWarrantyValidator;

        public DeviceWarrantyService(UnitOfWork unitOfWork, IValidator<CreateDeviceWarrantyRequest> createDeviceWarrantyValidator, IValidator<UpdateDeviceWarrantyRequest> updateDeviceWarrantyValidator)
        {
            _unitOfWork = unitOfWork;
            _createDeviceWarrantyValidator = createDeviceWarrantyValidator;
            _updateDeviceWarrantyValidator = updateDeviceWarrantyValidator;
        }

        public async Task<Result> CreateDeviceWarrantyAsync(CreateDeviceWarrantyRequest request)
        {
            var validationResult = await _createDeviceWarrantyValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var device = await _unitOfWork.DeviceRepository.GetByIdAsync(request.DeviceId);
            if (device == null)
            {
                return Result.Failure(DeviceWarrantyErrorMessage.InvalidDevice());
            }

            var deviceWarranty = new DeviceWarranty
            {
                Id = Guid.NewGuid(),
                Status = request.Status,
                WarrantyType = request.WarrantyType,
                WarrantyReason = request.WarrantyReason,
                WarrantyStartDate = request.WarrantyStartDate,
                WarrantyEndDate = request.WarrantyEndDate,
                Provider = request.Provider,
                WarrantyCode = request.WarrantyCode,
                Notes = request.Notes,
                Cost = request.Cost,
                DocumentUrl = request.DocumentUrl,
                SparePartCode = request.SparePartCode,
                SparePartName = request.SparePartName,
                DeviceId = request.DeviceId,
                CreatedDate = TimeHelper.GetHoChiMinhTime()
            };

            await _unitOfWork.DeviceWarrantyRepository.CreateAsync(deviceWarranty);
            return Result.SuccessWithObject(deviceWarranty);
        }

        public async Task<Result> GetDeviceWarrantyByIdAsync(Guid id)
        {
            var deviceWarranty = await _unitOfWork.DeviceWarrantyRepository.GetByIdAsync(id);
            if (deviceWarranty == null)
            {
                return Result.Failure(DeviceWarrantyErrorMessage.DeviceWarrantyNotExist());
            }

            var response = new GetDeviceWarrantyResponse
            {
                Id = deviceWarranty.Id,
                Status = deviceWarranty.Status,
                WarrantyType = deviceWarranty.WarrantyType,
                WarrantyReason = deviceWarranty.WarrantyReason,
                WarrantyStartDate = deviceWarranty.WarrantyStartDate,
                WarrantyEndDate = deviceWarranty.WarrantyEndDate,
                Provider = deviceWarranty.Provider,
                WarrantyCode = deviceWarranty.WarrantyCode,
                Notes = deviceWarranty.Notes,
                Cost = deviceWarranty.Cost,
                DocumentUrl = deviceWarranty.DocumentUrl,
                SparePartCode = deviceWarranty.SparePartCode,
                SparePartName = deviceWarranty.SparePartName,
                DeviceId = deviceWarranty.DeviceId,
                CreatedDate = deviceWarranty.CreatedDate,
                ModifiedDate = deviceWarranty.ModifiedDate
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllDeviceWarrantiesAsync(Guid? deviceId, int pageNumber, int pageSize)
        {
            var (deviceWarranties, totalCount) = await _unitOfWork.DeviceWarrantyRepository.GetAllDeviceWarrantiesAsync(deviceId, pageNumber, pageSize);
            var deviceWarrantyResponses = deviceWarranties.Select(dw => new GetDeviceWarrantyResponse
            {
                Id = dw.Id,
                Status = dw.Status,
                WarrantyType = dw.WarrantyType,
                WarrantyReason = dw.WarrantyReason,
                WarrantyStartDate = dw.WarrantyStartDate,
                WarrantyEndDate = dw.WarrantyEndDate,
                Provider = dw.Provider,
                WarrantyCode = dw.WarrantyCode,
                Notes = dw.Notes,
                Cost = dw.Cost,
                DocumentUrl = dw.DocumentUrl,
                SparePartCode = dw.SparePartCode,
                SparePartName = dw.SparePartName,
                DeviceId = dw.DeviceId,
                CreatedDate = dw.CreatedDate,
                ModifiedDate = dw.ModifiedDate
            }).ToList();

            var response = new PagedResponse<GetDeviceWarrantyResponse>
            {
                Data = deviceWarrantyResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdateDeviceWarrantyAsync(UpdateDeviceWarrantyRequest request)
        {
            var validationResult = await _updateDeviceWarrantyValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var deviceWarranty = await _unitOfWork.DeviceWarrantyRepository.GetByIdAsync(request.Id);
            if (deviceWarranty == null)
            {
                return Result.Failure(DeviceWarrantyErrorMessage.DeviceWarrantyNotExist());
            }

            var device = await _unitOfWork.DeviceRepository.GetByIdAsync(request.DeviceId);
            if (device == null)
            {
                return Result.Failure(DeviceWarrantyErrorMessage.InvalidDevice());
            }

            deviceWarranty.Status = request.Status;
            deviceWarranty.WarrantyType = request.WarrantyType;
            deviceWarranty.WarrantyReason = request.WarrantyReason;
            deviceWarranty.WarrantyStartDate = request.WarrantyStartDate;
            deviceWarranty.WarrantyEndDate = request.WarrantyEndDate;
            deviceWarranty.Provider = request.Provider;
            deviceWarranty.WarrantyCode = request.WarrantyCode;
            deviceWarranty.Notes = request.Notes;
            deviceWarranty.Cost = request.Cost;
            deviceWarranty.DocumentUrl = request.DocumentUrl;
            deviceWarranty.SparePartCode = request.SparePartCode;
            deviceWarranty.SparePartName = request.SparePartName;
            deviceWarranty.DeviceId = request.DeviceId;
            deviceWarranty.ModifiedDate = TimeHelper.GetHoChiMinhTime();

            await _unitOfWork.DeviceWarrantyRepository.UpdateAsync(deviceWarranty);
            return Result.SuccessWithObject(deviceWarranty);
        }

        public async Task<Result> DeleteDeviceWarrantyAsync(Guid id)
        {
            var result = await _unitOfWork.DeviceWarrantyRepository.DeleteDeviceWarrantyAsync(id);
            if (result == 0)
            {
                return Result.Failure(DeviceWarrantyErrorMessage.DeviceWarrantyDeleteFailed());
            }
            return Result.SuccessWithObject(result);
        }

        public async Task<Result> GetWarrantyStatusAsync(Guid deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
            if (device == null)
            {
                return Result.Failure(DeviceErrorMessage.DeviceNotExist());
            }

            var warranty = await _unitOfWork.DeviceRepository.GetActiveWarrantyAsync(deviceId);
            if (warranty == null)
            {
                return Result.Failure(DeviceErrorMessage.WarrantyNotExist());
            }

            var response = new DeviceWarrantyStatusResponse
            {
                IsUnderWarranty = warranty != null,
                WarrantyStatus = warranty?.Status,
                WarrantyCode = warranty?.WarrantyCode,
                WarrantyType = warranty?.WarrantyType,
                Provider = warranty?.Provider,
                WarrantyStartDate = warranty?.WarrantyStartDate,
                WarrantyEndDate = warranty?.WarrantyEndDate,
                Notes = warranty?.Notes,
                Cost = warranty?.Cost,
                DocumentUrl = warranty?.DocumentUrl,
                DaysRemaining = warranty != null
                    ? (int)(warranty.WarrantyEndDate!.Value.Date - TimeHelper.GetHoChiMinhTime().Date).TotalDays
                    : null,
                LowDayWarning = warranty != null && (warranty.WarrantyEndDate!.Value.Date - TimeHelper.GetHoChiMinhTime().Date).TotalDays <= 10
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllWarrantiesByDeviceIdAsync(Guid deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
            if (device == null)
            {
                return Result.Failure(DeviceErrorMessage.DeviceNotExist());
            }

            var warranties = await _unitOfWork.DeviceRepository.GetAllWarrantiesByDeviceIdAsync(deviceId);
            if (!warranties.Any())
            {
                return Result.SuccessWithObject(new List<DeviceWarrantyStatusResponse>());
            }

            return Result.SuccessWithObject(warranties);
        }

        public async Task<Result> GetDeviceWarrantyHistoryAsync(Guid deviceId)
        {
            var deviceExists = await _unitOfWork.DeviceRepository.DeviceIdExistsAsync(deviceId);
            if (!deviceExists)
                return Result.Failure(DeviceErrorMessage.DeviceNotExist());

            var history = await _unitOfWork.DeviceWarrantyRepository.GetDeviceWarrantyHistoryByDeviceIdAsync(deviceId);
            var historyDTOs = history.Select(dh => new DeviceWarrantyHistoryDTO
            {
                DeviceId = dh.DeviceId,
                DeviceDescription = dh.DeviceDescription,
                Status = dh.Status,
                SendDate = dh.SendDate,
                ReceiveDate = dh.ReceiveDate,
                Provider = dh.Provider,
                Note = dh.Note
            }).ToList();

            return Result.SuccessWithObject(historyDTOs);
        }
    }
}
