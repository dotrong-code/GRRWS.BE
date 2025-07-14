using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class RequestMachineReplacementRepository : GenericRepository<Domain.Entities.RequestMachineReplacement>, IRequestMachineReplacementRepository
    {
        public RequestMachineReplacementRepository(GRRWSContext context) : base(context)
        {
        }

        public async Task<(List<RequestMachineReplacement> items, int totalCount)> GetAllAsync(
    int pageNumber,
    int pageSize,
    string? status = null,
    string? sortBy = null,
    bool isAscending = true)
        {
            var query = _context.Set<RequestMachineReplacement>()
                .Include(r => r.NewDevice)
                .Include(r => r.OldDevice)
                .Include(r => r.Machine)
                .Include(r => r.Assignee)
                .Include(r => r.ApprovedBy)
                .Where(r => !r.IsDeleted)
                .AsQueryable();

            // Apply status filter if status is provided
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<MachineReplacementStatus>(status, true, out var statusEnum))
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
                    case "approveddate":
                        query = isAscending
                            ? query.OrderBy(r => r.ApprovedDate)
                            : query.OrderByDescending(r => r.ApprovedDate);
                        break;
                    case "completeddate":
                        query = isAscending
                            ? query.OrderBy(r => r.CompletedDate)
                            : query.OrderByDescending(r => r.CompletedDate);
                        break;
                    default:
                        query = isAscending
                            ? query.OrderBy(r => r.Id)
                            : query.OrderByDescending(r => r.Id);
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
        public async Task<RequestMachineReplacement> GetByTaskGroupIdAsync(Guid taskGroupId)
        {
            return await _context.RequestMachineReplacements
                .Include(rm => rm.OldDevice)
                .Include(rm => rm.NewDevice)
                .Include(rm => rm.Machine)
                .Include(rm => rm.Assignee)
                .Where(rm => !rm.IsDeleted && rm.TaskId != null && _context.Tasks.Any(t => t.Id == rm.TaskId && t.TaskGroupId == taskGroupId && !t.IsDeleted))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<RequestMachineReplacement> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.RequestMachineReplacements
                .Where(rm => !rm.IsDeleted && rm.TaskId == taskId)

                .FirstOrDefaultAsync();
        }

    }
}
