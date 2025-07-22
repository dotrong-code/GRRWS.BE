using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.RequestTakeSparePartUsage;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class RequestTakeSparePartUsageRepository : GenericRepository<RequestTakeSparePartUsage>, IRequestTakeSparePartUsageRepository
    {
        public RequestTakeSparePartUsageRepository(GRRWSContext context) : base(context) { }
        public async Task<(List<RequestTakeSparePartUsageViewDTO> items, int totalCount)> GetAllAsync(
int pageNumber,
int pageSize,
string? status = null,
string? sortBy = null,
bool isAscending = true)
        {
            var query = _context.Set<RequestTakeSparePartUsageViewDTO>()
                .AsQueryable();

            // Apply status filter if status is provided
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<SparePartRequestStatus>(status, true, out var statusEnum))
            {
                query = query.Where(r => r.Status == statusEnum);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "requestcode":
                        query = isAscending
                            ? query.OrderBy(r => r.RequestCode)
                            : query.OrderByDescending(r => r.RequestCode);
                        break;
                    case "requestdate":
                        query = isAscending
                            ? query.OrderBy(r => r.RequestDate)
                            : query.OrderByDescending(r => r.RequestDate);
                        break;

                    case "status":
                        query = isAscending
                            ? query.OrderBy(r => r.Status)
                            : query.OrderByDescending(r => r.Status);
                        break;
                    case "confirmeddate":
                        query = isAscending
                            ? query.OrderBy(r => r.ConfirmedDate)
                            : query.OrderByDescending(r => r.ConfirmedDate);
                        break;
                    default:
                        query = isAscending
                            ? query.OrderBy(r => r.RequestDate)
                            : query.OrderByDescending(r => r.RequestDate);
                        break;
                }
            }
            else
            {
                // Default sorting by RequestDate if no sortBy specified
                query = isAscending
                    ? query.OrderBy(r => r.RequestDate)
                    : query.OrderByDescending(r => r.RequestDate);
            }

            // Apply pagination
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            var items = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task<RequestTakeSparePartUsage> GetByIdAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<RequestTakeSparePartUsage> GetByIdIncludeSparePartUsagesAsync(Guid id)
        {
            return await _context.RequestTakeSparePartUsages
                .Where(rtspu => rtspu.Id == id)
                .Include(rtspu => rtspu.SparePartUsages)
                .FirstOrDefaultAsync();
        }
    }
}