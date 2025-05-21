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

        public async Task<Tasks> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks
                .Where(t => t.Id == taskId && !t.IsDeleted)
                .FirstOrDefaultAsync();
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
    }
}
