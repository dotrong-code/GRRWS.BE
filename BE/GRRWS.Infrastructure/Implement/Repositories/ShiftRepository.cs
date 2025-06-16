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
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(GRRWSContext context) : base(context) { }

        public async Task<Shift> GetCurrentShiftAsync(DateTime currentTime)
        {
            var currentTimeOfDay = currentTime.TimeOfDay;

            var shift = await _context.Shifts
                .Where(s => s.StartTime <= currentTimeOfDay && s.EndTime >= currentTimeOfDay)
                .FirstOrDefaultAsync();

            if (shift == null)
            {
                throw new InvalidOperationException($"No shift found for current time {currentTime:HH:mm}.");
            }
            return shift;
        }
    }
}