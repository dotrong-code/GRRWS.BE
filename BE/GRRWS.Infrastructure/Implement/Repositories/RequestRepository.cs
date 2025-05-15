using GRRWS.Domain.Entities;
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
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(GRRWSContext context) : base(context) { }
        public async Task<List<Request>> GetAllRequestAsync()
        {
            return await _context.Set<Request>().Include(r => r.RequestIssues).ThenInclude(ri => ri.Issue).ToListAsync();
        }
        public async Task<Request> GetRequestByIdAsync(Guid id)
        {
            return await _context.Requests
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .FirstOrDefaultAsync(r => r.Id == id);
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
    }
}
