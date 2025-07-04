﻿using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMechanicShiftRepository : IGenericRepository<MechanicShift>
    {
        Task<bool> UpdateMechanicShiftAvailableAsync(Guid mechanicShiftId);
        Task<bool> CreateMechanicShift(Guid userId, Guid taskId);
        Task<List<MechanicShift>> GetAllMechanicShiftAsync();
        Task<List<MechanicShift>> GetOverlappingShiftsAsync(Guid mechanicId, DateTime startTime, DateTime endTime);
        Task<List<MechanicShift>> GetMechanicShiftsByShiftAndDateAsync(Guid shiftId, DateTime date);
        Task<MechanicShift?> GetCurrentShiftAsync(Guid mechanicId, DateTime currentTime);
    }
}