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
    public class DeviceErrorHistoryService : IDeviceErrorHistoryService
    {
        private readonly IDeviceErrorHistoryRepository _deviceErrorHistoryRepository;
        public DeviceErrorHistoryService(IDeviceErrorHistoryRepository deviceErrorHistoryRepository)
        {
            _deviceErrorHistoryRepository = deviceErrorHistoryRepository;
        }
        public async Task<List<DeviceErrorHistory>> GetAllDeviceErrorHistoryAsync()
        {
            return await _deviceErrorHistoryRepository.GetAllDeviceErrorHistoryAsync();
        }
        public async Task<DeviceErrorHistory> GetDeviceErrorHistoryByDeviceIdAsync(Guid id)
        {
            return await _deviceErrorHistoryRepository.GetDeviceErrorHistoryByDeviceIdAsync(id);
        }
    }
}
