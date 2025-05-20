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
    public class DeviceIssueHistoryRepository : GenericRepository<DeviceIssueHistory>, IDeviceIssueHistoryRepository
    {
        public DeviceIssueHistoryRepository(GRRWSContext context) : base(context)
        {
        }
        public async Task<List<DeviceIssueHistory>> GetAllDeviceIssueHistoryAsync()
        {
            return await _context.Set<DeviceIssueHistory>()
                .Include(deh => deh.Device)
                .Include(deh => deh.Issue)
                .Where(deh => deh.IsDeleted != true)
                .ToListAsync();
        }
        public async Task<List<DeviceIssueHistory>> GetDeviceIssueHistoryByDeviceIdAsync(Guid id)
        {
            return await _context.DeviceIssueHistories
                .Include(deh => deh.Device)
                .Include(deh => deh.Issue)
                .Where(deh => deh.IsDeleted != true && deh.DeviceId == id)
                .ToListAsync();
        }
    }
}
