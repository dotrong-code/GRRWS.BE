using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class DeviceWarrantyRepository : GenericRepository<DeviceWarranty>, IDeviceWarrantyRepository
    {
        public DeviceWarrantyRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<GetDeviceWarrantyResponse> DeviceWarranties, int TotalCount)> GetAllDeviceWarrantiesAsync(Guid? deviceId, int pageNumber, int pageSize)
        {
            var query = _context.DeviceWarranties.AsQueryable();

            if (deviceId.HasValue)
                query = query.Where(dw => dw.DeviceId == deviceId.Value);

            query = query.Where(dw => !dw.IsDeleted);

            int totalCount = await query.CountAsync();

            var deviceWarranties = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(dw => new GetDeviceWarrantyResponse
                {
                    Id = dw.Id,
                    Status = dw.Status,
                    WarrantyType = dw.WarrantyType,
                    WarrantyReason = dw.WarrantyReason,
                    WarrantyStartDate = dw.WarrantyStartDate,
                    WarrantyEndDate = dw.WarrantyEndDate,
                    Provider = dw.Provider,
                    WarrantyCode = dw.WarrantyCode,
                    Notes = dw.Notes,
                    Cost = dw.Cost,
                    DocumentUrl = dw.DocumentUrl,
                    SparePartCode = dw.SparePartCode,
                    SparePartName = dw.SparePartName,
                    DeviceId = dw.DeviceId,
                    CreatedDate = dw.CreatedDate,
                    ModifiedDate = dw.ModifiedDate
                })
                .ToListAsync();

            return (deviceWarranties, totalCount);
        }

        public async Task<int> DeleteDeviceWarrantyAsync(Guid id)
        {
            var deviceWarranty = await _context.DeviceWarranties.FindAsync(id);
            if (deviceWarranty == null) return 0;

            deviceWarranty.IsDeleted = true;
            _context.DeviceWarranties.Update(deviceWarranty);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<DeviceWarrantyHistory>> GetDeviceWarrantyHistoryByDeviceIdAsync(Guid deviceId)
        {
            return await _context.DeviceWarrantyHistories
                .Where(dh => dh.DeviceId == deviceId)
                .ToListAsync();
        }
    }
}
