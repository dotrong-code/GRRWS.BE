using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.History;
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

        public async Task<DeviceWarranty> GetActiveWarrantyAsync(Guid deviceId)
        {
            var currentDate = DateTime.UtcNow;
            return await _context.DeviceWarranties
                .Where(dw => dw.DeviceId == deviceId
                             && !dw.IsDeleted
                             && (dw.Status == "Completed" || dw.Status == "Pending")
                             && dw.WarrantyStartDate <= currentDate
                             && dw.WarrantyEndDate >= currentDate)
                .OrderByDescending(dw => dw.WarrantyEndDate) // Prefer the longest-lasting warranty
                .FirstOrDefaultAsync();
        }


        public async Task<List<DeviceIssueHistoryResponse>> GetDeviceIssueHistoryByDeviceIdAsync(Guid deviceId)
        {
            return await _context.DeviceIssueHistories
                .Where(dih => dih.DeviceId == deviceId && !dih.IsDeleted)
                .OrderByDescending(dih => dih.OccurrenceCount)
                .Select(dih => new DeviceIssueHistoryResponse
                {
                    Id = dih.Id,
                    DeviceId = dih.DeviceId,
                    IssueId = dih.IssueId,
                    IssueCode = dih.Issue.IssueKey,
                    IssueDescription = dih.Issue.Description,
                    LastOccurredDate = dih.LastOccurredDate,
                    OccurrenceCount = dih.OccurrenceCount,
                    Notes = dih.Notes
                })
                .ToListAsync();
        }

        public async Task<List<MachineIssueHistoryResponse>> GetMachineIssueHistoryByMachineIdAsync(Guid machineId)
        {
            return await _context.MachineIssueHistories
                .Where(mih => mih.MachineId == machineId && !mih.IsDeleted)
                .OrderByDescending(mih => mih.OccurrenceCount)
                .Select(mih => new MachineIssueHistoryResponse
                {
                    Id = mih.Id,
                    MachineId = mih.MachineId,
                    IssueId = mih.IssueId,
                    IssueCode = mih.Issue.IssueKey,
                    IssueDescription = mih.Issue.Description,
                    LastOccurredDate = mih.LastOccurredDate,
                    OccurrenceCount = mih.OccurrenceCount,
                    Notes = mih.Notes
                })
                .ToListAsync();
        }

        public async Task<List<DeviceErrorHistoryResponse>> GetDeviceErrorHistoryByDeviceIdAsync(Guid deviceId)
        {
            return await _context.DeviceErrorHistories
                .Where(deh => deh.DeviceId == deviceId && !deh.IsDeleted)
                .OrderByDescending(deh => deh.OccurrenceCount)
                .Select(deh => new DeviceErrorHistoryResponse
                {
                    Id = deh.Id,
                    DeviceId = deh.DeviceId,
                    ErrorId = deh.ErrorId,
                    ErrorCode = deh.Error.ErrorCode,
                    ErrorDescription = deh.Error.Description,
                    LastOccurredDate = deh.LastOccurredDate,
                    OccurrenceCount = deh.OccurrenceCount,
                    Notes = deh.Notes
                })
                .ToListAsync();
        }

        public async Task<List<MachineErrorHistoryResponse>> GetMachineErrorHistoryByMachineIdAsync(Guid machineId)
        {
            return await _context.MachineErrorHistories
                .Where(meh => meh.MachineId == machineId && !meh.IsDeleted)
                .OrderByDescending(meh => meh.OccurrenceCount)
                .Select(meh => new MachineErrorHistoryResponse
                {
                    Id = meh.Id,
                    MachineId = meh.MachineId,
                    ErrorId = meh.ErrorId,
                    ErrorCode = meh.Error.ErrorCode,
                    ErrorDescription = meh.Error.Description,
                    LastOccurredDate = meh.LastOccurredDate,
                    OccurrenceCount = meh.OccurrenceCount,
                    Notes = meh.Notes
                })
                .ToListAsync();
        }

    }
}
