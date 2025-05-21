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
    public class DeviceHistoryRepository : GenericRepository<DeviceHistory>, IDeviceHistoryRepository
    {
        public DeviceHistoryRepository(GRRWSContext context) : base(context) { }
        public async Task<List<DeviceHistory>> GetAllDeviceHistoryAsync()
        {
            return await _context.Set<DeviceHistory>()
                .Include(dh => dh.Device)
                .Where(dh => dh.IsDeleted != true)
                .ToListAsync();
        }
        public async Task<List<DeviceHistory>> GetDeviceHistoryByDeviceIdAsync(Guid id)
        {
            return await _context.DeviceHistories
                .Include(dh => dh.Device)
                .Where(dh => dh.IsDeleted != true && dh.DeviceId == id)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }
    }
}
