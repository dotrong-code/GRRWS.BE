using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task CreateTaskAsync(Tasks task, List<Guid> errorIds, List<Guid> sparepartIds)
        {
            // Add task
            await _context.Tasks.AddAsync(task);

            // Link Errors via ErrorDetails
            if (errorIds != null && errorIds.Any())
            {
                // Assume ReportId is provided or fetched; here, we'll need a valid ReportId
                // For simplicity, assume a Report exists or is linked elsewhere
                var report = await _context.Reports.FirstOrDefaultAsync(); // Placeholder; adjust as needed
                if (report == null)
                    throw new Exception("No report found to link errors.");

                var errorDetails = errorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId,
                    TaskId = task.Id
                }).ToList();
                await _context.ErrorDetails.AddRangeAsync(errorDetails);
            }

            // Link Spareparts via RepairSpareparts
            if (sparepartIds != null && sparepartIds.Any())
            {
                var repairSpareparts = sparepartIds.Select(sparepartId => new RepairSparepart
                {
                    SpareId = sparepartId,
                    TaskId = task.Id
                }).ToList();
                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

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
    }
}
