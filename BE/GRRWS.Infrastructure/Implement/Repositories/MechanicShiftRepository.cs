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
        public async Task<bool> CreateMechanicShift(Guid userId, Guid taskId, Guid shiftId)
        {
            try
            {
                var mechanicShift = new MechanicShift
                {
                    MechanicId = userId,
                    TaskId = taskId,
                    ShiftId = shiftId,
                    Date = DateTime.Now,
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
        public MechanicShiftRepository(GRRWSContext context) : base(context) { }
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
