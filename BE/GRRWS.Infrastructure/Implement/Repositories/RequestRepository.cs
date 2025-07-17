using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
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
        public async Task<(List<Request> Items, int TotalCount)> GetAllRequestAsync(int pageNumber, int pageSize, string? search, string? searchType)
        {
            var query = _context.Set<Request>()
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                .ThenInclude(ri => ri.Images)
                .Where(r => !r.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                if (string.Equals(searchType, "status", StringComparison.OrdinalIgnoreCase))
                {
                    if (Enum.TryParse<Status>(search, true, out var status))
                    {
                        if (status == Status.InProgress)
                        {
                            // Include both InProgress and Approved statuses
                            query = query.Where(r => r.Status == Status.InProgress || r.Status == Status.Approved);
                        }
                        else
                        {
                            query = query.Where(r => r.Status == status);
                        }
                    }
                    else
                    {
                        query = query.Where(r => false); // Return empty if status is invalid
                    }
                }
                else if (string.Equals(searchType, "title", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(r => r.RequestTitle != null && r.RequestTitle.ToLower().Contains(search.ToLower()));
                }
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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
        public async Task<(List<Request> Items, int TotalCount)> GetRequestByUserIdAsync(
    Guid userId,
    int pageNumber,
    int pageSize,
    string? search,
    string? searchType)
        {
            // 1. Fetch all matching requests (no pagination in the DB query)
            var query = _context.Requests
                .Include(r => r.Device)
                    .ThenInclude(d => d.Position)
                    .ThenInclude(p => p.Zone)
                    .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                    .ThenInclude(ri => ri.Issue)
                .Include(r => r.RequestIssues)
                    .ThenInclude(ri => ri.Images)
                .Where(r => r.RequestedById == userId && !r.IsDeleted);

            // 2. Apply filters
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                if (string.Equals(searchType, "status", StringComparison.OrdinalIgnoreCase))
                {
                    if (Enum.TryParse<Status>(search, true, out var status))
                    {
                        // If status == InProgress, include Approved
                        if (status == Status.InProgress)
                        {
                            query = query.Where(r => r.Status == Status.InProgress || r.Status == Status.Approved);
                        }
                        else
                        {
                            query = query.Where(r => r.Status == status);
                        }
                    }
                    else
                    {
                        // Invalid status => no results
                        query = query.Where(r => false);
                    }
                }
                else
                {
                    // Default to title search
                    query = query.Where(r =>
                        r.RequestTitle != null &&
                        r.RequestTitle.ToLower().Contains(search.ToLower()));
                }
            }

            // 3. Get all results
            var allRequests = await query.ToListAsync();

            // 4. Group in memory by ZoneName (null => "Unknown Zone"), sort requests by CreatedDate desc
            var groupedByZone = allRequests
                .GroupBy(r => r.Device?.Position?.Zone?.ZoneName ?? "Unknown Zone")
                .Select(g => g.OrderByDescending(r => r.CreatedDate).ToList())
                .ToList();

            // 5. Flatten groups in alphabetical order of zone or any custom ordering
            //    If you want zone name ordering, do .OrderBy(g => g.Key).  If you want to keep them 
            //    in the order they appear, skip re-ordering. Here we just concatenate in the existing grouping order.
            var flatList = new List<Request>();
            foreach (var group in groupedByZone)
            {
                flatList.AddRange(group);
            }

            // 6. Now apply pagination on the flat list
            var totalCount = flatList.Count;
            var pagedRequests = flatList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (pagedRequests, totalCount);
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
                    RequestTitle = r.RequestTitle ?? "Untitled Request",
                    Priority = r.Priority.ToString(),
                    Status = r.Status.ToString(),
                    RequestDate = r.CreatedDate
                })
                .AsNoTracking()
                .OrderBy(r => r.RequestDate)
                .ToListAsync();
        }

        public async Task<RequestDetailWeb?> GetRequestDetailWebByIdAsync(Guid requestId)
        {
            var request = await _context.Requests
               .AsNoTracking()
               .Where(r => r.Id == requestId && !r.IsDeleted)
               .Include(r => r.Device)
                   .ThenInclude(d => d.Position)
                       .ThenInclude(p => p.Zone)
                           .ThenInclude(z => z.Area)
               .Include(r => r.Report)
               .Include(r => r.RequestIssues)
                   .ThenInclude(ri => ri.Issue)
               .Include(r => r.RequestIssues)
                   .ThenInclude(ri => ri.Images)
               .Select(r => new RequestDetailWeb
               {
                   RequestId = r.Id,
                   RequestTitle = r.RequestTitle,
                   Priority = r.Priority.ToString(), // Convert enum to string
                   Status = r.Status.ToString(), // Convert enum to string
                   RequestDate = r.CreatedDate,
                   IsWarranty = r.Report != null, // Simplified check since Report doesn't have Status
                   RemainingWarratyDate = 0,
                   DeviceId = r.DeviceId,
                   DeviceName = r.Device.DeviceName,
                   Location = r.Device.Position != null && r.Device.Position.Zone != null && r.Device.Position.Zone.Area != null
                       ? $"{r.Device.Position.Zone.Area.AreaName} - {r.Device.Position.Zone.ZoneName} - {r.Device.Position.Index}"
                       : "Location not available",
                   Issues = r.RequestIssues.Select(ri => new IssueForRequestDetailWeb
                   {
                       IssueId = ri.Issue.Id,
                       DisplayName = ri.Issue.DisplayName,
                       Images = ri.Images.Select(i => i.ImageUrl).ToList(),
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
            var reportId = await _context.Requests
                .Where(r => r.Id == requestId && !r.IsDeleted)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            if (reportId == null)
                return new List<TaskForRequestDetailWeb>();

            // Get task IDs from both ErrorDetails and TechnicalSymptomReports
            var errorTaskIds = await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId && ed.TaskId != null)
                .Select(ed => ed.TaskId.Value)
                .ToListAsync();

            var technicalTaskIds = await _context.TechnicalSymptomReports
                .Where(tsr => tsr.ReportId == reportId && tsr.TaskId != null)
                .Select(tsr => tsr.TaskId.Value)
                .ToListAsync();

            // Combine and get distinct task IDs
            var allTaskIds = errorTaskIds.Concat(technicalTaskIds).Distinct().ToList();

            if (!allTaskIds.Any())
                return new List<TaskForRequestDetailWeb>();

            return await _context.Tasks
                .AsNoTracking()
                .Where(t => allTaskIds.Contains(t.Id) && !t.IsDeleted)
                .Include(t => t.Assignee)
                .Select(t => new TaskForRequestDetailWeb
                {
                    TaskId = t.Id,
                    TaskType = t.TaskType.ToString(),
                    Status = t.Status.ToString(),
                    StartTime = t.StartTime,
                    AssigneeName = t.Assignee != null ? t.Assignee.UserName : "Unassigned",
                    ExpectedTime = t.ExpectedTime,
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

        public async Task<Request?> GetByTaskIdAsync(Guid taskId)
        {
            var request = await _context.Tasks
                .Where(t => t.Id == taskId && !t.IsDeleted)
                .Select(t => t.TaskGroup)
                .Where(tg => tg != null)
                .Select(tg => tg.Report)
                .Where(rp => rp != null)
                .Select(rp => rp.Request)
                .Where(r => r != null && !r.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return request;
        }
        public async Task<Request> GetActiveRequestByPositionIdAsync(Guid positionId)
        {
            return await _context.Requests
                .Include(r => r.Device)
                .Where(r => r.Device.PositionId == positionId && !r.IsDeleted && r.Status != Status.Completed)
                .OrderByDescending(r => r.CreatedDate)
                .FirstOrDefaultAsync();
        }

        public Task<Request> GetByTaskIdInclueDeviceAsync(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
