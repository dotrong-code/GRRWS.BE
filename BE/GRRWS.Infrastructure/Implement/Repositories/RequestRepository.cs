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
        public async Task<List<RequestIssue>> GetIssuesByRequestIdAsync(Guid requestId)
        {
            var request = await _context.Requests
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .ThenInclude(i => i.IssueErrors)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images)
                .FirstOrDefaultAsync(r => r.Id == requestId && !r.IsDeleted);

            if (request == null)
                return new List<RequestIssue>();

            return request.RequestIssues
                .Where(ri => !ri.Issue.IsDeleted)
                .ToList();
        }


        public async Task<List<RequestSummary>> GetRequestSummaryAsync()
        {
            return await _context.Requests
                .Where(r => !r.IsDeleted)
                .Select(r => new RequestSummary
                {
                    RequestId = r.Id,
                    RequestTitle = r.Description ?? "Untitled Request",
                    Priority = r.Priority ?? "Unknown",
                    Status = r.Status ?? "Unknown",
                    RequestDate = r.CreatedDate
                })
                .AsNoTracking() // Improves query performance by not tracking entities
                .ToListAsync();
        }

        public async Task<RequestDetailWeb?> GetRequestDetailWebByIdAsync(Guid requestId)
        {
            var request = await _context.Requests
               .AsNoTracking()
               .Where(r => r.Id == requestId && !r.IsDeleted)
               .Select(r => new RequestDetailWeb
               {
                   RequestId = r.Id,
                   RequestTitle = r.RequestTitle,
                   Priority = r.Priority,
                   Status = r.Status,
                   RequestDate = r.CreatedDate,
                   DeviceId = r.DeviceId,
                   DeviceName = r.Device.DeviceName,
                   Issues = r.RequestIssues.Select(ri => new IssueForRequestDetailWeb
                   {
                       IssueId = ri.Issue.Id,
                       DisplayName = ri.Issue.DisplayName,
                       Status = ri.Status,
                       Images = ri.Images.Select(img => img.ImageUrl).ToList()
                   }).ToList()
               })
               .FirstOrDefaultAsync();
            return request;
        }

        public async Task<List<ErrorForRequestDetailWeb>> GetErrorsForRequestDetailWebAsync(Guid requestId)
        {
            // Get the reportId from the request
            var reportId = await _context.Requests
                .Where(r => r.Id == requestId && !r.IsDeleted)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            if (reportId == null)
                return new List<ErrorForRequestDetailWeb>();

            // Get errors from ErrorDetails by reportId
            return await _context.ErrorDetails
                .AsNoTracking()
                .Where(ed => ed.ReportId == reportId)
                .Select(ed => new ErrorForRequestDetailWeb
                {
                    ErrorId = ed.Error.Id,
                    ErrorCode = ed.Error.ErrorCode,
                    Name = ed.Error.Name,
                    Severity = ed.Error.Severity,
                    Status = ed.TaskId == null ? "Unassigned" : "Assigned" // Fixed the incorrect reference to 'ErrorDetails'
                })
                .ToListAsync();
        }

        public async Task<List<TaskForRequestDetailWeb>> GetTasksForRequestDetailWebAsync(Guid requestId)
        {
            // Get the reportId from the request
            var reportId = await _context.Requests
                .Where(r => r.Id == requestId && !r.IsDeleted)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            if (reportId == null)
                return new List<TaskForRequestDetailWeb>();

            // Get all unique TaskIds from ErrorDetails for this report
            var taskIds = await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId && ed.TaskId != null)
                .Select(ed => ed.TaskId.Value)
                .Distinct()
                .ToListAsync();

            if (!taskIds.Any())
                return new List<TaskForRequestDetailWeb>();

            // Get tasks by those TaskIds
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => taskIds.Contains(t.Id) && !t.IsDeleted)
                .Select(t => new TaskForRequestDetailWeb
                {
                    TaskId = t.Id,
                    TaskType = t.TaskType,
                    Status = t.Status,
                    StartTime = t.StartTime,
                    AssigneeName = t.Assignee.UserName,
                    ExpectedTime = t.ExpectedTime,
                    NumberOfErrors = t.ErrorDetails.Count
                })
                .ToListAsync();
        }

        public async Task<List<TechnicalIssueForRequestDetailWeb>> GetTechnicalIssuesForRequestDetailWebAsync(Guid requestId)
        {
            // Get the reportId from the request
            var reportId = await _context.Requests
                .Where(r => r.Id == requestId && !r.IsDeleted)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            if (reportId == null)
                return new List<TechnicalIssueForRequestDetailWeb>();

            // Get technical issues from TechnicalSymptomReports by reportId
            return await _context.TechnicalSymptomReports
                .AsNoTracking()
                .Where(tsr => tsr.ReportId == reportId)
                .Select(tsr => new TechnicalIssueForRequestDetailWeb
                {
                    TechnicalIssueId = tsr.TechnicalSymptom.Id,
                    SymptomCode = tsr.TechnicalSymptom.SymptomCode,
                    Name = tsr.TechnicalSymptom.Name,
                    Description = tsr.TechnicalSymptom.Description,
                    IsCommon = tsr.TechnicalSymptom.IsCommon,
                    Status = tsr.TaskId == null ? "Unassigned" : "Assigned"
                })
                .ToListAsync();
        }
    }
}
