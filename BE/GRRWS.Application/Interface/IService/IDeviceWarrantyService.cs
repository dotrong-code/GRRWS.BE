using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;

namespace GRRWS.Application.Interface.IService
{
    public interface IDeviceWarrantyService
    {
        Task<Result> CreateDeviceWarrantyAsync(CreateDeviceWarrantyRequest request);
        Task<Result> GetDeviceWarrantyByIdAsync(Guid id);
        Task<Result> GetAllDeviceWarrantiesAsync(Guid? deviceId, int pageNumber, int pageSize);
        Task<Result> UpdateDeviceWarrantyAsync(UpdateDeviceWarrantyRequest request);
        Task<Result> DeleteDeviceWarrantyAsync(Guid id);
        Task<Result> GetDeviceWarrantyHistoryAsync(Guid deviceId);

        Task<Result> GetWarrantyStatusAsync(Guid deviceId);
        Task<Result> GetAllWarrantiesByDeviceIdAsync(Guid deviceId); 
    }
}
