using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.History;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<bool> DeviceCodeExistsAsync(string deviceCode);
        Task<Device> GetDeviceByIdAsync(Guid id);
        Task<(List<GetDeviceResponse> Devices, int TotalCount)> GetAllDevicesAsync(
            string? deviceName = null,
            string? deviceCode = null,
            string? status = null,
            Guid? positionId = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<int> DeleteDeviceAsync(Guid id);

        Task<DeviceWarranty> GetActiveWarrantyAsync(Guid deviceId);

        Task<List<DeviceIssueHistoryResponse>> GetDeviceIssueHistoryByDeviceIdAsync(Guid deviceId);
        Task<List<MachineIssueHistoryResponse>> GetMachineIssueHistoryByMachineIdAsync(Guid machineId);
        Task<List<DeviceErrorHistoryResponse>> GetDeviceErrorHistoryByDeviceIdAsync(Guid deviceId);
        Task<List<MachineErrorHistoryResponse>> GetMachineErrorHistoryByMachineIdAsync(Guid machineId);
    }
}
