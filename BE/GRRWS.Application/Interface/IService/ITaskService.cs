using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;

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
        Task<Result> CreateSimpleTaskWebAsync(CreateSimpleTaskWeb dto); // Add this line

        // New task creation methods
        Task<Result> CreateTaskFromErrorsAsync(CreateTaskFromErrorsRequest request);
        Task<Result> CreateTaskFromTechnicalIssueAsync(CreateTaskFromTechnicalIssueRequest request);
        Task<Result> CreateSimpleTaskAsync(CreateSimpleTaskRequest request);

        ///New versions of create task
        Task<Result> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId);
        Task<Result> FillInWarrantyTask(FillInWarrantyTask request);
        Task<Result> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type);

        Task<Result> CreateRepairTask(CreateRepairTaskRequest request, Guid userId);

        Task<Result> UpdateTaskStatusToCompleted(Guid taskId, Guid userId);
        Task<Result> GetMechanicRecommendationAsync();
    }
}
