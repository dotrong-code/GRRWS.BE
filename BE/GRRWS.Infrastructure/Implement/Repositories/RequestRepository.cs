using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(GRRWSContext context) : base(context) { }
        public async Task<List<Request>> GetAllRequestAsync()
        {
            return await _context.Set<Request>()
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images).ToListAsync();
        }
        public async Task<Request> GetRequestByIdAsync(Guid id)
        {
            return await _context.Requests
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Request>> GetRequestByDeviceIdAsync(Guid id)
        {
            return await _context.Requests
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images)
                .Where(r => r.DeviceId == id).ToListAsync();
        }
        public async Task<List<Request>> GetRequestByUserIdAsync(Guid userId)
        {
            return await _context.Requests
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images)
                .Where(r => r.RequestedById == userId).ToListAsync();
        }
        public async Task UpdateRequestAsync(Request request, List<Guid> newIssueIds)
        {
            var existingRequest = await _context.Requests
                .Include(r => r.RequestIssues)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (existingRequest == null)
                throw new Exception("Request not found.");
            existingRequest.RequestTitle = request.RequestTitle;
            existingRequest.Description = request.Description;
            existingRequest.Status = request.Status;
            existingRequest.DueDate = request.DueDate;
            existingRequest.Priority = request.Priority;
            existingRequest.ModifiedBy = request.ModifiedBy;
            existingRequest.ModifiedDate = request.ModifiedDate;

            var oldRequestIssues = await _context.RequestIssues
                .Where(ri => ri.RequestId == request.Id)
                .ToListAsync();

            _context.RequestIssues.RemoveRange(oldRequestIssues);

            var newRequestIssues = newIssueIds.Select(issueId => new RequestIssue
            {
                RequestId = request.Id,
                IssueId = issueId
            }).ToList();

            await _context.RequestIssues.AddRangeAsync(newRequestIssues);

            await _context.SaveChangesAsync();
        }
        public async Task<List<IssueSimpleDTO>> GetIssuesByRequestIdAsync(Guid requestId)
        {
            var request = await _context.Requests
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .FirstOrDefaultAsync(r => r.Id == requestId && !r.IsDeleted);

            if (request == null)
                return new List<IssueSimpleDTO>();

            var issues = request.RequestIssues
                .Where(ri => !ri.Issue.IsDeleted)
                .Select(ri => new IssueSimpleDTO
                {
                    Id = ri.Issue.Id,
                    DisplayName = ri.Issue.DisplayName
                })
                .ToList();

            return issues;
        }

        public async Task<List<RequestSummary>> GetRequestSummaryAsync()
        {
            return await _context.Requests
                .Where(r => !r.IsDeleted)
                .Select(r => new RequestSummary
                {
                    RequestId = r.Id,
                    RequestTitle = r.RequestTitle ?? "Untitled Request",
                    Piority = r.Priority ?? "Unknown",
                    Status = r.Status ?? "Unknown",
                    RequestDate = r.CreatedDate
                })
                .AsNoTracking() // Improves query performance by not tracking entities
                .ToListAsync();
        }
    }
}
