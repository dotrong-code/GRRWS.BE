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

        public async Task<Tasks> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks
                .Where(t => t.Id == taskId && !t.IsDeleted)
                .FirstOrDefaultAsync();
        }
    }
}
