using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(GRRWSContext context) : base(context) { }

        public async Task<Shift> GetCurrentShiftAsync(DateTime currentTime)
        {
            var currentTimeOfDay = currentTime.TimeOfDay;

            var shift = await _context.Shifts
                .Where(s => s.StartTime <= currentTimeOfDay && s.EndTime >= currentTimeOfDay)
                .FirstOrDefaultAsync();

            return shift;
        }
        public async Task<Shift> GetNearestShiftAsync(DateTime currentTime)
        {
            var shifts = await _context.Shifts
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            if (!shifts.Any())
            {
                return null; // Không có ca nào trong hệ thống
            }

            var currentDate = currentTime.Date;
            var currentTimeOfDay = currentTime.TimeOfDay;

            var nearestShiftToday = shifts
                .Where(s => s.StartTime >= currentTimeOfDay)
                .OrderBy(s => s.StartTime)
                .FirstOrDefault();

            if (nearestShiftToday != null)
            {
                return new Shift
                {
                    Id = nearestShiftToday.Id,
                    ShiftName = nearestShiftToday.ShiftName,
                    StartTime = nearestShiftToday.StartTime,
                    EndTime = nearestShiftToday.EndTime
                };
            }

            var nextDay = currentDate.AddDays(1);
            var firstShiftOfNextDay = shifts.OrderBy(s => s.StartTime).First();
            return new Shift
            {
                Id = firstShiftOfNextDay.Id,
                ShiftName = firstShiftOfNextDay.ShiftName,
                StartTime = firstShiftOfNextDay.StartTime,
                EndTime = firstShiftOfNextDay.EndTime
            };
        }

        public Task<List<Shift>> GetShiftsByTimeRangeAsync(TimeSpan startTime, TimeSpan endTime)
        {
            throw new NotImplementedException();
        }
    }
}