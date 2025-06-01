using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IDeviceWarrantyRepository : IGenericRepository<DeviceWarranty>
    {
        Task<(List<GetDeviceWarrantyResponse> DeviceWarranties, int TotalCount)> GetAllDeviceWarrantiesAsync(
            Guid? deviceId = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<int> DeleteDeviceWarrantyAsync(Guid id);

        Task<List<DeviceWarrantyHistory>> GetDeviceWarrantyHistoryByDeviceIdAsync(Guid deviceId);
    }
}
