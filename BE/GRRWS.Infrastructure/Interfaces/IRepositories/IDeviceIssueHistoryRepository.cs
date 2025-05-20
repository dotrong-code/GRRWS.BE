using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IDeviceIssueHistoryRepository : IGenericRepository<DeviceIssueHistory>
    {
        Task<List<DeviceIssueHistory>> GetAllDeviceIssueHistoryAsync();
        Task<List<DeviceIssueHistory>> GetDeviceIssueHistoryByDeviceIdAsync(Guid id);
    }
}
