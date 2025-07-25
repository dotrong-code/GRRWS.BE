using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MachineActionConfirmationRepository : GenericRepository<MachineActionConfirmation>, IMachineActionConfirmationRepository
    {
        public MachineActionConfirmationRepository(GRRWSContext context) : base(context) { }
        public async Task<MachineActionConfirmation> GetByTaskIdAndTypeAsync(Guid? taskId, MachineActionType actionType)
        {
            if (!taskId.HasValue)
                return null;

            return await _context.Set<MachineActionConfirmation>()
                .Where(mac => mac.TaskId == taskId && mac.ActionType == actionType && !mac.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<MachineActionConfirmation> items, int totalCount)> GetAllAsync(
            int pageNumber, int pageSize, string? status, string? sortBy, bool isAscending)
        {
            var query = _context.Set<MachineActionConfirmation>()
                .Where(mac => !mac.IsDeleted)
                .AsQueryable();

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<MachineActionStatus>(status, true, out var statusEnum))
            {
                query = query.Where(mac => mac.Status == statusEnum);
            }

            // Calculate total count before paging
            var totalCount = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "startdate":
                        query = isAscending ? query.OrderBy(mac => mac.StartDate) : query.OrderByDescending(mac => mac.StartDate);
                        break;
                    case "confirmationcode":
                        query = isAscending ? query.OrderBy(mac => mac.ConfirmationCode) : query.OrderByDescending(mac => mac.ConfirmationCode);
                        break;
                    case "status":
                        query = isAscending ? query.OrderBy(mac => mac.Status) : query.OrderByDescending(mac => mac.Status);
                        break;
                    case "actiontype":
                        query = isAscending ? query.OrderBy(mac => mac.ActionType) : query.OrderByDescending(mac => mac.ActionType);
                        break;
                    default:
                        query = isAscending ? query.OrderBy(mac => mac.CreatedDate) : query.OrderByDescending(mac => mac.CreatedDate);
                        break;
                }
            }
            else
            {
                query = isAscending ? query.OrderBy(mac => mac.CreatedDate) : query.OrderByDescending(mac => mac.CreatedDate);
            }

            // Apply paging
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Include navigation properties if needed (optional)
            query = query.Include(mac => mac.RequestedBy)
                         .Include(mac => mac.Assignee)
                         .Include(mac => mac.ApprovedBy)
                         .Include(mac => mac.Device)
                         .Include(mac => mac.Machine)
                         .Include(mac => mac.Task);

            var items = await query.ToListAsync();

            return (items, totalCount);
        }
        public async Task<MachineActionConfirmation> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Set<MachineActionConfirmation>()
                .Include(mac => mac.RequestedBy)
                .Include(mac => mac.Assignee)
                .Include(mac => mac.ApprovedBy)
                .Include(mac => mac.Signer)
                .Include(mac => mac.Device)
                .Include(mac => mac.Machine)
                .Include(mac => mac.Task)
                .FirstOrDefaultAsync(mac => mac.Id == id && !mac.IsDeleted);
        }
    }
}
