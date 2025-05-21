using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ITaskRepository : IGenericRepository<Tasks>
    {
        Task<List<GetTaskResponse>> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize);
        Task<GetTaskResponse> GetTaskDetailsAsync(Guid taskId);
        Task<Tasks> GetTaskByIdAsync(Guid taskId);
        Task UpdateErrorDetailsAsync(List<Guid> errorDetailIds, Guid taskId);
        Task<(List<GetTaskResponse> Tasks, int TotalCount)> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize);

        Task<List<TaskByReportResponse>> GetTasksByReportIdAsync(Guid reportId);
    }
}
