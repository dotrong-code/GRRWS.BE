using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface ITaskRepository : IGenericRepository<Tasks>
    {
        Task<List<GetTaskResponse>> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize);
        Task<List<GetTaskForMechanic>> GetTasksByMechanicIdAsync2(Guid mechanicId, int pageNumber, int pageSize);
        Task<GetTaskResponse> GetTaskDetailsAsync(Guid taskId);
        Task<Tasks> GetTaskByIdAsync(Guid taskId);
        Task<List<Tasks>> GetAllTasksAsync();
        Task CreateTaskAsync(Tasks task, Guid reportId, List<Guid> errorIds, List<Guid> sparepartIds);
        Task UpdateTaskAsync(Tasks task, List<Guid> errorIds, List<Guid> sparepartIds);

        Task UpdateErrorDetailsAsync(List<Guid> errorDetailIds, Guid taskId);
        Task<(List<GetTaskResponse> Tasks, int TotalCount)> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize);
        Task<List<TaskByReportResponse>> GetTasksByReportIdAsync(Guid reportId);
        Task<Guid> CreateTaskWebAsync(CreateTaskWeb dto);
        Task<Guid> CreateSimpleTaskWebAsync(CreateSimpleTaskWeb dto);

        // New task creation methods
        Task<Guid> CreateTaskFromErrorsAsync(CreateTaskFromErrorsRequest request);
        Task<Guid> CreateTaskFromTechnicalIssueAsync(CreateTaskFromTechnicalIssueRequest request);
        Task<Guid> CreateSimpleTaskAsync(CreateSimpleTaskRequest request);

        //New versions of create task
        Task<Guid> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId);
        Task<Guid> CreateRepairTask(CreateRepairTaskRequest request, Guid userId);
        Task<Guid> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId);
        Task<Guid> CreateInstallTask(CreateInstallTaskRequest request, Guid userId);
        Task<GetDetailWarrantyTaskForMechanic> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId, string type);
        Task<GetDetailtRepairTaskForMechanic> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId, string type);
        Task<GetDetailReplaceTaskForMechanic> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type);

        Task<Guid> FillInWarrantyTask(FillInWarrantyTask request);

        Task<bool> UpdateTaskStatusToCompleted(Guid taskId, Guid userId);
        Task<bool> UpdateTaskStatusAsync(Guid taskId, Guid userId);
    }
}
