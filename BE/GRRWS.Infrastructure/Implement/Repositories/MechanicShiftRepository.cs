using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MechanicShiftRepository : GenericRepository<MechanicShift>, IMechanicShiftRepository
    {
        public MechanicShiftRepository(GRRWSContext context) : base(context) { }
        public async Task<List<MechanicShift>> GetAllMechanicShiftAsync()
        {
            return await _context.MechanicShifts
                .Include(ms => ms.Shift)
                .Include(ms => ms.Mechanic)
                .Include(ms => ms.Task)
                .ToListAsync();
        }
        //public async Task<bool> CreateMechanicShift(Guid userId, Guid taskId, Guid shiftId)
        //{
        //    try
        //    {
        //        var mechanicShift = new MechanicShift
        //        {
        //            MechanicId = userId,
        //            TaskId = taskId,
        //            ShiftId = shiftId,
        //            Date = DateTime.Now,
        //            IsAvailable = false
        //        };

        //        _context.MechanicShifts.Add(mechanicShift);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public async Task<bool> CreateMechanicShift(Guid userId, Guid taskId)
        {
            try
            {
                if (userId == Guid.Empty || taskId == Guid.Empty)
                {
                    return false;
                }

                var existingShifts = await _context.MechanicShifts
                    .Where(ms => ms.MechanicId == userId && !ms.IsAvailable)
                    .ToListAsync();

                var shifts = _context.Shifts.OrderBy(s => s.StartTime).ToList();
                if (!shifts.Any()) return false;

                var getTask = await _context.Tasks
                    .FirstOrDefaultAsync(t => t.Id == taskId);

                if (getTask == null || !getTask.StartTime.HasValue)
                {
                    return false;
                }

                if (existingShifts.Any())
                {
                    return false;
                }

                var mechanicShift = new MechanicShift
                {
                    MechanicId = userId,
                    TaskId = taskId,
                    ShiftId = shifts.FirstOrDefault(s => s.StartTime <= getTask.StartTime.Value.TimeOfDay && s.EndTime >= getTask.StartTime.Value.TimeOfDay)?.Id ?? shifts[0].Id,
                    StartTime = getTask.StartTime,
                    EndTime = getTask.EndTime,
                    IsAvailable = false
                };

                _context.MechanicShifts.Add(mechanicShift);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateMechanicShiftAvailableAsync(Guid mechanicShiftId)
        {
            var mechanicShift = await _context.MechanicShifts
                .FirstOrDefaultAsync(ms => ms.Id == mechanicShiftId);

            if (mechanicShift == null)
            {
                return false;
            }

            mechanicShift.IsAvailable = true;
            _context.MechanicShifts.Update(mechanicShift);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
