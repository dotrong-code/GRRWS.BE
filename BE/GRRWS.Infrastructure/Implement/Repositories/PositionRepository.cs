using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.Position;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(GRRWSContext context) : base(context) { }

        public async Task<(List<GetPositionResponse> Positions, int TotalCount)> GetAllPositionsAsync(
            Guid? zoneId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Positions.AsQueryable();

            if (zoneId.HasValue)
                query = query.Where(p => p.ZoneId == zoneId.Value);

            query = query.Where(p => !p.IsDeleted);

            int totalCount = await query.CountAsync();

            var positions = await query
                .Include(p => p.Device) // Eagerly load the Device navigation property
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new GetPositionResponse
                {
                    Id = p.Id,
                    Index = p.Index,
                    ZoneId = p.ZoneId,
                    DeviceId = p.DeviceId,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate,
                    Device = p.Device != null ? new GetDeviceResponse
                    {
                        Id = p.Device.Id,
                        DeviceName = p.Device.DeviceName,
                        DeviceCode = p.Device.DeviceCode,
                        SerialNumber = p.Device.SerialNumber,
                        Model = p.Device.Model,
                        Manufacturer = p.Device.Manufacturer,
                        ManufactureDate = p.Device.ManufactureDate,
                        InstallationDate = p.Device.InstallationDate,
                        Description = p.Device.Description,
                        PhotoUrl = p.Device.PhotoUrl,
                        Status = p.Device.Status,
                        IsUnderWarranty = p.Device.IsUnderWarranty,
                        Specifications = p.Device.Specifications,
                        PurchasePrice = p.Device.PurchasePrice,
                        Supplier = p.Device.Supplier,
                        MachineId = p.Device.MachineId,
                        PositionId = p.Device.PositionId,
                        CreatedDate = p.Device.CreatedDate,
                        ModifiedDate = p.Device.ModifiedDate
                    } : null
                })
                .ToListAsync();

            return (positions, totalCount);
        }

        public async Task<int> DeletePositionAsync(Guid id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null) return 0;

            position.IsDeleted = true;
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync();
        }
    }
}
