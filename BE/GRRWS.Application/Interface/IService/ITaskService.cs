using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Task;

namespace GRRWS.Application.Interface.IService
{
    public interface ITaskService
    {
        Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize);
        Task<Result> StartTaskAsync(StartTaskRequest request);
        Task<Result> GetTaskDetailsAsync(Guid taskId);
        Task<Result> CreateTaskReportAsync(CreateTaskReportRequest request);

        //CRUD
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateTaskDTO dto);
        Task<Result> UpdateAsync(Guid id, UpdateTaskDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> AssignTaskAsync(Guid taskId, AssignTaskDTO dto);

        Task<Result> CreateTaskAsync(CreateTaskRequest request); // New
        Task<Result> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize); // New
        Task<Result> UpdateTaskAsync(UpdateTaskRequest request); // New
        Task<Result> DeleteTaskAsync(Guid taskId); // New
        Task<Result> GetTasksByReportIdAsync(Guid reportId);
        Task<Result> CreateTaskWebAsync(CreateTaskWeb dto);

    }
}
