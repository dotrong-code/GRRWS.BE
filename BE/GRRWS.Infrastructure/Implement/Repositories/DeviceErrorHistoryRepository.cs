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
    public class DeviceErrorHistoryRepository : GenericRepository<DeviceErrorHistory>, IDeviceErrorHistoryRepository
    {
        public DeviceErrorHistoryRepository(GRRWSContext context) : base(context)
        {
        }
        public async Task<List<DeviceErrorHistory>> GetAllDeviceErrorHistoryAsync()
        {
            return await _context.Set<DeviceErrorHistory>()
                .Include(deh => deh.Device)
                .Include(deh => deh.Error)
                .Where(deh => deh.IsDeleted != true)
                .ToListAsync();
        }
        public async Task<DeviceErrorHistory> GetDeviceErrorHistoryByDeviceIdAsync(Guid id)
        {
            return await _context.DeviceErrorHistories
                .Include(deh => deh.Device)
                .Include(deh => deh.Error)
                .Where(deh => deh.IsDeleted != true && deh.DeviceId == id)
                .FirstOrDefaultAsync();
        }
    }
}
