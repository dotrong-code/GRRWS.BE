using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.MechanicShift;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class MechanicShiftService : IMechanicShiftService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MechanicShiftService> _logger;
        private readonly IMapper _mapper;
        public MechanicShiftService(IUnitOfWork unitOfWork, ILogger<MechanicShiftService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result> CreateMechanicShiftAsync(Guid mechanicId, Guid taskId)
        {
            if (mechanicId == Guid.Empty || taskId == Guid.Empty)
            {
                _logger.LogError("Invalid mechanicId or taskId provided at {Time}", TimeHelper.GetHoChiMinhTime());
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Invalid Input", "Mechanic ID and Task ID cannot be empty."));
            }
            var createdMechanicShift = await _unitOfWork.MechanicShiftRepository.CreateMechanicShift(mechanicId, taskId);
            if (!createdMechanicShift)
            {
                _logger.LogError("Failed to create mechanic shift for task {TaskId} at {Time}", taskId, TimeHelper.GetHoChiMinhTime());
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", "Failed to create mechanic shift for the task."));
            }
            return Result.SuccessWithObject(new
            {
                Message = "Mechanic shift created successfully!",
            });
        }
        public async Task<Result> UpdateMechanicShiftStatusToAvailableAsync(Guid mechanicShiftId)
        {
            if (mechanicShiftId == Guid.Empty)
            {
                _logger.LogError("Invalid mechanicShiftId provided at {Time}", TimeHelper.GetHoChiMinhTime());
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Invalid Input", "Mechanic Shift ID cannot be empty."));
            }
            var updated = await _unitOfWork.MechanicShiftRepository.UpdateMechanicShiftAvailableAsync(mechanicShiftId);
            if (!updated)
            {
                _logger.LogError("Failed to update mechanic shift status for ID {MechanicShiftId} at {Time}", mechanicShiftId, TimeHelper.GetHoChiMinhTime());
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", "Failed to update mechanic shift status."));
            }
            return Result.SuccessWithObject(new
            {
                Message = "Mechanic shift status updated to available successfully!",
            });
        }
        public async Task<Result> GetAllMechanicShiftAsync()
        {
            var mechanicShifts = await _unitOfWork.MechanicShiftRepository.GetAllMechanicShiftAsync();
            var mechanicShiftsMap = _mapper.Map<List<MechanicShiftResponseDTO>>(mechanicShifts);
            return Result.SuccessWithObject(mechanicShiftsMap);
        }
    }
}
