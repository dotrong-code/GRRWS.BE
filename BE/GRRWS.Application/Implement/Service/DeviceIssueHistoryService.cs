using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceIssueHistoryService : IDeviceIssueHistoryService
    {
        private readonly IDeviceIssueHistoryRepository _deviceIssueHistoryRepository;
        public DeviceIssueHistoryService(IDeviceIssueHistoryRepository deviceIssueHistoryRepository)
        {
            _deviceIssueHistoryRepository = deviceIssueHistoryRepository;
        }
        public async Task<List<DeviceIssueHistory>> GetAllDeviceIssueHistoryAsync()
        {
            return await _deviceIssueHistoryRepository.GetAllDeviceIssueHistoryAsync();
        }
        public async Task<List<DeviceIssueHistory>> GetDeviceIssueHistoryByDeviceIdAsync(Guid id)
        {
            return await _deviceIssueHistoryRepository.GetDeviceIssueHistoryByDeviceIdAsync(id);
        }
    }
}
