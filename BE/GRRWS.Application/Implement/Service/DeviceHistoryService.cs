using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceHistoryService : IDeviceHistoryService
    {
        private readonly IDeviceHistoryRepository _deviceHistoryRepository;
        public DeviceHistoryService(IDeviceHistoryRepository deviceHistoryRepository)
        {
            _deviceHistoryRepository = deviceHistoryRepository;
        }
        public async Task<Result> GetAllDeviceIssueHistoryAsync()
        {
            var deviceHistories = await _deviceHistoryRepository.GetAllDeviceHistoryAsync();
            return Result.SuccessWithObject(deviceHistories);
        }
    }
}
