using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IDeviceHistoryRepository : IGenericRepository<DeviceHistory>
    {
        Task<List<DeviceHistory>> GetAllDeviceHistoryAsync();
        Task<List<DeviceHistory>> GetDeviceHistoryByDeviceIdAsync(Guid id);
    }
}
