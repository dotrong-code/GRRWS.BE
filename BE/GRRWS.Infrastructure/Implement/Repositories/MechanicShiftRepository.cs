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
        public async Task<bool> CreateMechanicShift(Guid userId, Guid taskId, Guid shiftId)
        {
            try
            {
                var existingShift = await _context.MechanicShifts
                    .FirstOrDefaultAsync(ms => ms.MechanicId == userId && ms.ShiftId == shiftId && !ms.IsAvailable);

                var shifts = _context.Shifts.OrderBy(s => s.StartTime).ToList();
                var currentShift = shifts.FirstOrDefault(s => s.Id == shiftId);
                if (currentShift == null) return false;

                var currentDate = DateTime.Now.Date;
                var startIndex = shifts.IndexOf(currentShift);
                DateTime dateToUse = DateTime.Now;

                if (existingShift != null)
                {
                    bool foundNextShift = false;
                    for (int i = startIndex + 1; i < shifts.Count; i++)
                    {
                        var shift = shifts[i];
                        var shiftStart = currentDate.Add(shift.StartTime);
                        if (shiftStart > DateTime.Now)
                        {
                            dateToUse = shiftStart;
                            foundNextShift = true;
                            break;
                        }
                    }

                    if (!foundNextShift)
                    {
                        var nextDay = currentDate.AddDays(1);
                        for (int i = 0; i < shifts.Count; i++)
                        {
                            var shift = shifts[i];
                            var shiftStart = nextDay.Add(shift.StartTime);
                            if (shiftStart.Date <= currentDate.AddDays(3))
                            {
                                dateToUse = shiftStart;
                                break;
                            }
                        }
                    }
                }

                var mechanicShift = new MechanicShift
                {
                    MechanicId = userId,
                    TaskId = taskId,
                    ShiftId = shiftId,
                    Date = dateToUse,
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
        public async Task<bool> UpdateMechanicShiftAvailableAsync(Guid userId, Guid taskId)
        {
            var mechanicShift = await _context.MechanicShifts
                .FirstOrDefaultAsync(ms => ms.MechanicId == userId && ms.TaskId == taskId);

            if (mechanicShift == null)
            {
                return false;
            }

            mechanicShift.IsAvailable = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
