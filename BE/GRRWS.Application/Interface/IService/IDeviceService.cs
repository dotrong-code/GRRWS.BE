using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Device;

namespace GRRWS.Application.Interface.IService
{
    public interface IDeviceService
    {
        Task<Result> CreateDeviceAsync(CreateDeviceRequest request);
        Task<Result> GetDeviceByIdAsync(Guid id);
        Task<Result> GetAllDevicesAsync(
            string? deviceName = null,
            string? deviceCode = null,
            string? status = null,
            Guid? positionId = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<Result> UpdateDeviceAsync(UpdateDeviceRequest request);
        Task<Result> DeleteDeviceAsync(Guid id);
        Task<Result> GetPositionsAsync();
        Task<Result> GetZoneByPositionAsync(Guid positionId);
        Task<Result> GetAreaByZoneAsync(Guid zoneId);
    }
}
