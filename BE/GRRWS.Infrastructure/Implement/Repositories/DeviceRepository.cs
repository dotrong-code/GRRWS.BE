using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(GRRWSContext context) : base(context) { }

        public async Task<bool> DeviceCodeExistsAsync(string deviceCode)
        {
            return await _context.Devices.AnyAsync(d => d.DeviceCode == deviceCode && !d.IsDeleted);
        }
        public async Task<Device> GetDeviceByIdAsync(Guid id)
        {
            return await _context.Devices
                .Include(d => d.Machine)
                .Include(d => d.Position)
                .ThenInclude(dv => dv.Zone)
                .ThenInclude(dv => dv.Area)
                .Where(d => d.Id == id && !d.IsDeleted)
                .FirstOrDefaultAsync();
        }
        public async Task<(List<GetDeviceResponse> Devices, int TotalCount)> GetAllDevicesAsync(
            string? deviceName, string? deviceCode, string? status, Guid? positionId, int pageNumber, int pageSize)
        {
            var query = _context.Devices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(deviceName))
                query = query.Where(d => d.DeviceName.Contains(deviceName));

            if (!string.IsNullOrWhiteSpace(deviceCode))
                query = query.Where(d => d.DeviceCode.Contains(deviceCode));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(d => d.Status == status);

            if (positionId.HasValue)
                query = query.Where(d => d.PositionId == positionId.Value);

            query = query.Where(d => !d.IsDeleted);

            int totalCount = await query.CountAsync();

            var devices = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new GetDeviceResponse
                {
                    Id = d.Id,
                    DeviceName = d.DeviceName,
                    DeviceCode = d.DeviceCode,
                    SerialNumber = d.SerialNumber,
                    Model = d.Model,
                    Manufacturer = d.Manufacturer,
                    ManufactureDate = d.ManufactureDate,
                    InstallationDate = d.InstallationDate,
                    Description = d.Description,
                    PhotoUrl = d.PhotoUrl,
                    Status = d.Status,
                    IsUnderWarranty = d.IsUnderWarranty,
                    Specifications = d.Specifications,
                    PurchasePrice = d.PurchasePrice,
                    Supplier = d.Supplier,
                    MachineId = d.MachineId,
                    PositionId = d.PositionId,
                    CreatedDate = d.CreatedDate,
                    ModifiedDate = d.ModifiedDate
                })
                .ToListAsync();

            return (devices, totalCount);
        }

        public async Task<int> DeleteDeviceAsync(Guid id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return 0;

            device.IsDeleted = true;
            _context.Devices.Update(device);
            return await _context.SaveChangesAsync();
        }
    }
}
