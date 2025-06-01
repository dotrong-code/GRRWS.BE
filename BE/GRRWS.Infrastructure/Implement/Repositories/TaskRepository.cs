using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class TaskRepository : GenericRepository<Tasks>, ITaskRepository
    {
        public TaskRepository(GRRWSContext context) : base(context) { }
        public async Task<List<TaskByReportResponse>> GetTasksByReportIdAsync(Guid reportId)
        {
            return await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId && ed.TaskId.HasValue && !ed.Task.IsDeleted)
                .OrderByDescending(ed => ed.Task.Priority)
                .ThenBy(ed => ed.Task.StartTime ?? DateTime.MaxValue)
                .Select(ed => new TaskByReportResponse
                {
                    TaskId = ed.TaskId.Value,
                    TaskType = ed.Task.TaskType,
                    Priority = ed.Task.Priority.Value,
                    Status = ed.Task.Status,
                    AssigneeName = ed.Task.Assignee.FullName,
                    StartTime = ed.Task.StartTime
                })
                .ToListAsync();
        }
        public async Task<List<GetTaskResponse>> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Where(t => t.AssigneeId == mechanicId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedDate);

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    Priority = t.Priority,
                    Status = t.Status,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .ToListAsync();

            return tasks;
        }

        public async Task<GetTaskResponse> GetTaskDetailsAsync(Guid taskId)
        {
            return await _context.Tasks
                .Where(t => t.Id == taskId && !t.IsDeleted)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    Priority = t.Priority,
                    Status = t.Status,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }



        public async Task<List<Tasks>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.ErrorDetails).ThenInclude(ed => ed.Error)
                .Include(t => t.RepairSpareparts).ThenInclude(rs => rs.Sparepart)
                .ToListAsync();
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.ErrorDetails).ThenInclude(ed => ed.Error)
                .Include(t => t.RepairSpareparts).ThenInclude(rs => rs.Sparepart)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateTaskAsync(Tasks task, Guid reportId, List<Guid> errorIds, List<Guid> sparepartIds)
        {
            // Add task
            await _context.Tasks.AddAsync(task);

            // Link Errors via ErrorDetails
            if (errorIds != null && errorIds.Any())
            {
                // Fetch the report
                var report = await _context.Reports
                    .FirstOrDefaultAsync(r => r.Id == reportId);
                if (report == null)
                    throw new Exception("No report found to link errors.");

                // Deduplicate errorIds to prevent duplicate ErrorDetail entries
                var uniqueErrorIds = errorIds.Distinct().ToList();

                // Check for existing ErrorDetails to avoid duplicates
                var existingErrorDetails = await _context.ErrorDetails
                    .Where(ed => ed.ReportId == reportId && uniqueErrorIds.Contains(ed.ErrorId))
                    .Select(ed => ed.ErrorId)
                    .ToListAsync();

                // Filter out errorIds that already exist
                var newErrorIds = uniqueErrorIds.Except(existingErrorDetails).ToList();

                // Create new ErrorDetail records only for non-existing pairs
                var errorDetails = newErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId,
                    TaskId = task.Id
                }).ToList();

                if (errorDetails.Any())
                {
                    await _context.ErrorDetails.AddRangeAsync(errorDetails);
                }
            }

            // Link Spareparts via RepairSpareparts
            if (sparepartIds != null && sparepartIds.Any())
            {
                // Deduplicate sparepartIds to prevent duplicate RepairSparepart entries
                var uniqueSparepartIds = sparepartIds.Distinct().ToList();

                var repairSpareparts = uniqueSparepartIds.Select(sparepartId => new RepairSparepart
                {
                    SpareId = sparepartId,
                    TaskId = task.Id
                }).ToList();
                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            // Save all changes
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Tasks task, List<Guid> errorIds, List<Guid> sparepartIds)
        {
            var existingTask = await _context.Tasks
                .Include(t => t.ErrorDetails)
                .Include(t => t.RepairSpareparts)
                .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (existingTask == null)
                throw new Exception("Task not found.");

            // Update task properties
            _context.Entry(existingTask).CurrentValues.SetValues(task);

            // Update ErrorDetails
            var existingErrorDetails = existingTask.ErrorDetails.ToList();
            _context.ErrorDetails.RemoveRange(existingErrorDetails);

            if (errorIds != null && errorIds.Any())
            {
                var report = await _context.Reports.FirstOrDefaultAsync(); // Placeholder; adjust as needed
                if (report == null)
                    throw new Exception("No report found to link errors.");

                var newErrorDetails = errorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId,
                    TaskId = task.Id
                }).ToList();
                await _context.ErrorDetails.AddRangeAsync(newErrorDetails);
            }

            // Update RepairSpareparts
            var existingRepairSpareparts = existingTask.RepairSpareparts.ToList();
            _context.RepairSpareparts.RemoveRange(existingRepairSpareparts);

            if (sparepartIds != null && sparepartIds.Any())
            {
                var newRepairSpareparts = sparepartIds.Select(sparepartId => new RepairSparepart
                {
                    SpareId = sparepartId,
                    TaskId = task.Id
                }).ToList();
                await _context.RepairSpareparts.AddRangeAsync(newRepairSpareparts);
            }

            await _context.SaveChangesAsync();
        }
        public async Task UpdateErrorDetailsAsync(List<Guid> errorDetailIds, Guid taskId)
        {
            if (errorDetailIds == null || !errorDetailIds.Any())
                return;

            var errorDetails = await _context.ErrorDetails
                .Where(ed => errorDetailIds.Contains(ed.ErrorId) && !ed.TaskId.HasValue)
                .ToListAsync();

            foreach (var errorDetail in errorDetails)
            {
                errorDetail.TaskId = taskId;
            }

            _context.ErrorDetails.UpdateRange(errorDetails);
            await _context.SaveChangesAsync();
        }
        public async Task<(List<GetTaskResponse> Tasks, int TotalCount)> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Where(t => !t.IsDeleted);

            if (!string.IsNullOrWhiteSpace(taskType))
                query = query.Where(t => t.TaskType == taskType);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(t => t.Status == status);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            query = query.OrderByDescending(t => t.CreatedDate);

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    Priority = t.Priority,
                    Status = t.Status,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .ToListAsync();

            return (tasks, totalCount);
        }

        public async Task<Guid> CreateTaskWebAsync(CreateTaskWeb dto)
        {
            // Get the reportId for the request in a single query
            var reportId = await _context.Requests
                .Where(r => r.Id == dto.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            // Create the task entity
            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = dto.TaskType,
                StartTime = dto.StartDate,
                Status = "Pending",
                TaskDescription = "This is description",
                AssigneeId = dto.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            // Prepare ErrorDetails updates/creations in bulk
            if (dto.ErrorIds != null && dto.ErrorIds.Count > 0)
            {
                // Get all ErrorDetails for this report and error ids in one query
                var existingErrorDetails = await _context.ErrorDetails
                    .Where(ed => ed.ReportId == reportId && dto.ErrorIds.Contains(ed.ErrorId))
                    .ToListAsync();

                var existingErrorIds = existingErrorDetails.Select(ed => ed.ErrorId).ToHashSet();

                // Update existing ErrorDetails
                foreach (var ed in existingErrorDetails)
                {
                    ed.TaskId = task.Id;
                }
                if (existingErrorDetails.Count > 0)
                    _context.ErrorDetails.UpdateRange(existingErrorDetails); // <-- Ensure EF tracks the update

                // Add new ErrorDetails if not exist
                var newErrorDetails = dto.ErrorIds
                    .Where(eid => !existingErrorIds.Contains(eid))
                    .Select(eid => new ErrorDetail
                    {
                        ReportId = (Guid)reportId,
                        ErrorId = eid,
                        TaskId = task.Id
                    }).ToList();

                if (newErrorDetails.Count > 0)
                    await _context.ErrorDetails.AddRangeAsync(newErrorDetails);
            }

            // Prepare RepairSparepart links in bulk
            if (dto.SparepartIds != null && dto.SparepartIds.Count > 0)
            {
                var repairSpareparts = dto.SparepartIds
                    .Select(spid => new RepairSparepart
                    {
                        TaskId = task.Id,
                        SpareId = spid
                    }).ToList();

                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<Guid> CreateSimpleTaskWebAsync(CreateSimpleTaskWeb dto)
        {
            // Get the reportId for the request in a single query
            var reportId = await _context.Requests
                .Where(r => r.Id == dto.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            // Create the task entity
            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = dto.TaskType,
                StartTime = dto.StartDate,
                Status = "Pending",
                TaskDescription = "This is description",
                AssigneeId = dto.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            // Handle spareparts if provided
            if (dto.SparepartIds != null && dto.SparepartIds.Count > 0)
            {
                var repairSpareparts = dto.SparepartIds.Select(sparepartId => new RepairSparepart
                {
                    TaskId = task.Id,
                    SpareId = sparepartId
                }).ToList();

                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }
    }
}
