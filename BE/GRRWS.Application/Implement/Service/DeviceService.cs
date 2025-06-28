using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.History;
using GRRWS.Infrastructure.DTOs.Import;
using GRRWS.Infrastructure.DTOs.Paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class DeviceService : IDeviceService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IValidator<CreateDeviceRequest> _createDeviceValidator;
    private readonly IValidator<UpdateDeviceRequest> _updateDeviceValidator;
    private readonly IImportService _importService;

    public DeviceService(UnitOfWork unitOfWork, IValidator<CreateDeviceRequest> createDeviceValidator, IValidator<UpdateDeviceRequest> updateDeviceValidator, IImportService importService)
    {
        _unitOfWork = unitOfWork;
        _createDeviceValidator = createDeviceValidator;
        _updateDeviceValidator = updateDeviceValidator;
        _importService = importService;
    }

    public async Task<Result> CreateDeviceAsync(CreateDeviceRequest request)
    {
        var validationResult = await _createDeviceValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => (GRRWS.Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
            return Result.Failures(errors);
        }

        if (await _unitOfWork.DeviceRepository.DeviceCodeExistsAsync(request.DeviceCode))
        {
            return Result.Failure(DeviceErrorMessage.DeviceCodeExists());
        }

        if (request.PositionId.HasValue)
        {
            var position = await _unitOfWork.PositionRepository.GetByIdAsync(request.PositionId.Value);
            if (position == null)
            {
                return Result.Failure(DeviceErrorMessage.InvalidPosition());
            }
        }

        var device = new Device
        {
            Id = Guid.NewGuid(),
            DeviceName = request.DeviceName,
            DeviceCode = request.DeviceCode,
            SerialNumber = request.SerialNumber,
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            ManufactureDate = request.ManufactureDate,
            InstallationDate = request.InstallationDate,
            Description = request.Description,
            PhotoUrl = request.PhotoUrl,
            Status = Enum.TryParse<DeviceStatus>(request.Status, out var status) ? status : DeviceStatus.Active,
            IsUnderWarranty = request.IsUnderWarranty,
            Specifications = request.Specifications,
            PurchasePrice = request.PurchasePrice,
            Supplier = request.Supplier,
            MachineId = request.MachineId,
            PositionId = request.PositionId,
            CreatedDate = DateTime.UtcNow
        };

        await _unitOfWork.DeviceRepository.CreateAsync(device);
        return Result.SuccessWithObject(device);
    }

    public async Task<Result> GetDeviceByIdAsync(Guid id)
    {
        var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(id);
        if (device == null)
        {
            return Result.Failure(DeviceErrorMessage.DeviceNotExist());
        }

        var response = new GetDeviceResponse
        {
            Id = device.Id,
            DeviceName = device.DeviceName,
            DeviceCode = device.DeviceCode,
            SerialNumber = device.SerialNumber,
            Model = device.Model,
            Manufacturer = device.Manufacturer,
            ManufactureDate = device.ManufactureDate,
            InstallationDate = device.InstallationDate,
            Description = device.Description,
            PhotoUrl = device.PhotoUrl,
            Status = device.Status.ToString(),
            PositionIndex = device.Position?.Index,
            ZoneName = device.Position?.Zone?.ZoneName,
            AreaName = device.Position?.Zone?.Area?.AreaName,
            IsUnderWarranty = device.IsUnderWarranty,
            Specifications = device.Specifications,
            PurchasePrice = device.PurchasePrice,
            Supplier = device.Supplier,
            MachineId = device.MachineId,
            PositionId = device.PositionId,
            CreatedDate = device.CreatedDate,
            ModifiedDate = device.ModifiedDate
        };

        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetAllDevicesAsync(string? deviceName, string? deviceCode, string? status, Guid? positionId, int pageNumber, int pageSize)
    {
        var (devices, totalCount) = await _unitOfWork.DeviceRepository.GetAllDevicesAsync(deviceName, deviceCode, status, positionId, pageNumber, pageSize);
        var deviceResponses = devices.Select(d => new GetDeviceResponse
        {
            Id = d.Id,
            DeviceName = d.DeviceName,
            DeviceCode = d.DeviceCode,
            SerialNumber = d.SerialNumber,
            Model = d.Model,
            Manufacturer = d.Manufacturer,
            ManufactureDate = d.ManufactureDate,
            InstallationDate = d.InstallationDate,
            Description = d.Description,
            PhotoUrl = d.PhotoUrl,
            Status = d.Status.ToString(),
            IsUnderWarranty = d.IsUnderWarranty,
            Specifications = d.Specifications,
            PurchasePrice = d.PurchasePrice,
            Supplier = d.Supplier,
            MachineId = d.MachineId,
            PositionId = d.PositionId,
            CreatedDate = d.CreatedDate,
            ModifiedDate = d.ModifiedDate
        }).ToList();

        var response = new PagedResponse<GetDeviceResponse>
        {
            Data = deviceResponses,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return Result.SuccessWithObject(response);
    }

    public async Task<Result> UpdateDeviceAsync(UpdateDeviceRequest request)
    {
        var validationResult = await _updateDeviceValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => (GRRWS.Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
            return Result.Failures(errors);
        }

        var device = await _unitOfWork.DeviceRepository.GetByIdAsync(request.Id);
        if (device == null)
        {
            return Result.Failure(DeviceErrorMessage.DeviceNotExist());
        }

        if (request.DeviceCode != device.DeviceCode && await _unitOfWork.DeviceRepository.DeviceCodeExistsAsync(request.DeviceCode))
        {
            return Result.Failure(DeviceErrorMessage.DeviceCodeExists());
        }

        if (request.PositionId.HasValue)
        {
            var position = await _unitOfWork.PositionRepository.GetByIdAsync(request.PositionId.Value);
            if (position == null)
            {
                return Result.Failure(DeviceErrorMessage.InvalidPosition());
            }
        }

        device.DeviceName = request.DeviceName;
        device.DeviceCode = request.DeviceCode;
        device.SerialNumber = request.SerialNumber;
        device.Model = request.Model;
        device.Manufacturer = request.Manufacturer;
        device.ManufactureDate = request.ManufactureDate;
        device.InstallationDate = request.InstallationDate;
        device.Description = request.Description;
        device.PhotoUrl = null;
        device.Status = Enum.TryParse<DeviceStatus>(request.Status, out var status) ? status : device.Status;
        device.IsUnderWarranty = request.IsUnderWarranty;
        device.Specifications = request.Specifications;
        device.PurchasePrice = request.PurchasePrice;
        device.Supplier = request.Supplier;
        device.MachineId = request.MachineId;
        device.PositionId = request.PositionId;
        device.ModifiedDate = DateTime.UtcNow;

        await _unitOfWork.DeviceRepository.UpdateAsync(device);
        return Result.SuccessWithObject(device);
    }

    public async Task<Result> DeleteDeviceAsync(Guid id)
    {
        var result = await _unitOfWork.DeviceRepository.DeleteDeviceAsync(id);
        if (result == 0)
        {
            return Result.Failure(DeviceErrorMessage.DeviceDeleteFailed());
        }
        return Result.SuccessWithObject(result);
    }

    public async Task<Result> GetPositionsAsync()
    {
        var positions = await _unitOfWork.PositionRepository.GetAllAsync();
        var response = positions.Select(p => new { p.Id, p.Index, p.ZoneId }).ToList();
        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetZoneByPositionAsync(Guid positionId)
    {
        var position = await _unitOfWork.PositionRepository.GetByIdAsync(positionId);
        if (position == null)
        {
            return Result.Failure(DeviceErrorMessage.InvalidPosition());
        }

        var zone = await _unitOfWork.ZoneRepository.GetByIdAsync(position.ZoneId.Value);
        if (zone == null)
        {
            return Result.Failure(DeviceErrorMessage.InvalidZone());
        }

        var response = new { zone.Id, zone.ZoneName, zone.AreaId };
        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetAreaByZoneAsync(Guid zoneId)
    {
        var zone = await _unitOfWork.ZoneRepository.GetByIdAsync(zoneId);
        if (zone == null)
        {
            return Result.Failure(DeviceErrorMessage.InvalidZone());
        }

        var area = await _unitOfWork.AreaRepository.GetByIdAsync(zone.AreaId.Value);
        if (area == null)
        {
            return Result.Failure(DeviceErrorMessage.InvalidArea());
        }

        var response = new { area.Id, area.AreaName };
        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetWarrantyStatusAsync(Guid deviceId)
    {
        var device = await _unitOfWork.DeviceRepository.    GetByIdAsync(deviceId);
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
                ? (int)(warranty.WarrantyEndDate!.Value.Date - DateTime.UtcNow.Date).TotalDays
                : null,
            LowDayWarning = warranty != null && (warranty.WarrantyEndDate!.Value.Date - DateTime.UtcNow.Date).TotalDays <= 10
        };

        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetAllDeviceAndMachineIssueHistoryByDeviceIdAsync(Guid deviceId)
    {
        var device = await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
        if (device == null)
            return Result.Failure(DeviceErrorMessage.DeviceNotExist());

        // Lấy lịch sử Issue của Device
        var deviceHistory = await _unitOfWork.DeviceRepository.GetDeviceIssueHistoryByDeviceIdAsync(deviceId);

        // Lấy lịch sử Issue của Machine (nếu có)
        var machineHistory = device.MachineId.HasValue
            ? await _unitOfWork.DeviceRepository.GetMachineIssueHistoryByMachineIdAsync(device.MachineId.Value)
            : new List<MachineIssueHistoryResponse>();

        // Loại bỏ các Issue trùng lặp trong MachineHistory
        if (deviceHistory.Any() && machineHistory.Any())
        {
            // Lấy danh sách IssueId từ DeviceHistory
            var deviceIssueIds = deviceHistory.Select(dh => dh.IssueId).ToHashSet();

            // Loại bỏ các bản ghi trong MachineHistory có IssueId trùng với DeviceHistory
            machineHistory = machineHistory
                .Where(mh => !deviceIssueIds.Contains(mh.IssueId))
                .ToList();
        }

        var response = new DeviceAndMachineIssueHistoryResponse
        {
            DeviceHistory = deviceHistory,
            MachineHistory = machineHistory
        };

        return Result.SuccessWithObject(response);
    }

    public async Task<Result> GetAllDeviceAndMachineErrorHistoryByDeviceIdAsync(Guid deviceId)
    {
        var device = await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
        if (device == null)
            return Result.Failure(DeviceErrorMessage.DeviceNotExist());

        // Lấy lịch sử Error của Device
        var deviceHistory = await _unitOfWork.DeviceRepository.GetDeviceErrorHistoryByDeviceIdAsync(deviceId);

        // Lấy lịch sử Error của Machine (nếu có)
        var machineHistory = device.MachineId.HasValue
            ? await _unitOfWork.DeviceRepository.GetMachineErrorHistoryByMachineIdAsync(device.MachineId.Value)
            : new List<MachineErrorHistoryResponse>();

        // Loại bỏ các Error trùng lặp trong MachineHistory
        if (deviceHistory.Any() && machineHistory.Any())
        {
            // Lấy danh sách ErrorId từ DeviceHistory
            var deviceErrorIds = deviceHistory.Select(dh => dh.ErrorId).ToHashSet();

            // Loại bỏ các bản ghi trong MachineHistory có ErrorId trùng với DeviceHistory
            machineHistory = machineHistory
                .Where(mh => !deviceErrorIds.Contains(mh.ErrorId))
                .ToList();
        }

        var response = new DeviceAndMachineErrorHistoryResponse
        {
            DeviceHistory = deviceHistory,
            MachineHistory = machineHistory
        };

        return Result.SuccessWithObject(response);
    }
    public async Task<Result> ImportDevicesAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Failure(GRRWS.Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
        }

        return await _importService.ImportAsync<Device>(file.OpenReadStream(), _unitOfWork.DeviceRepository);
    }
    public async Task<Result> GetAllDeviceAndMachineTechnicalSymptomHistoryByDeviceIdAsync(Guid deviceId)
    {
        var device = await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId);
        if (device == null)
            return Result.Failure(DeviceErrorMessage.DeviceNotExist());

        // Lấy lịch sử TechnicalSymptom của Device
        var deviceHistory = await _unitOfWork.DeviceRepository.GetDeviceTechnicalSymptomHistoryByDeviceIdAsync(deviceId);

        // Lấy lịch sử TechnicalSymptom của Machine (nếu có)
        var machineHistory = device.MachineId.HasValue
            ? await _unitOfWork.DeviceRepository.GetMachineTechnicalSymptomHistoryByMachineIdAsync(device.MachineId.Value)
            : new List<MachineTechnicalSymptomHistoryResponse>();

        // Loại bỏ các TechnicalSymptom trùng lặp trong MachineHistory
        if (deviceHistory.Any() && machineHistory.Any())
        {
            // Lấy danh sách TechnicalSymptomId từ DeviceHistory
            var deviceTechnicalSymptomIds = deviceHistory.Select(dh => dh.TechnicalSymptomId).ToHashSet();

            // Loại bỏ các bản ghi trong MachineHistory có TechnicalSymptomId trùng với DeviceHistory
            machineHistory = machineHistory
                .Where(mh => !deviceTechnicalSymptomIds.Contains(mh.TechnicalSymptomId))
                .ToList();
        }

        var response = new DeviceAndMachineTechnicalSymptomHistoryResponse
        {
            DeviceHistory = deviceHistory,
            MachineHistory = machineHistory
        };

        return Result.SuccessWithObject(response);
    }

}
