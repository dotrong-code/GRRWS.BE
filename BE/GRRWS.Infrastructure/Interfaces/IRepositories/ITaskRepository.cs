using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.RequestDTO;
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

        //New versions of create task
        Task<Guid> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId);
        Task<Guid> CreateRepairTask(CreateRepairTaskRequest request, Guid userId);
        Task<Guid> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId);
        Task<Guid> CreateInstallTask(CreateInstallTaskRequest request, Guid userId);
        Task<GetDetailWarrantyTaskForMechanic> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId, TaskType type);
        Task<GetDetailtRepairTaskForMechanic> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId, string type);
        Task<GetDetailReplaceTaskForMechanic> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type);
        Task<GetDetailUninstallTaskForMechanic> GetDetailUninstallTaskForMechanicByIdAsync(Guid taskId);
        Task<GetDetailInstallTaskForMechanic> GetDetailInstallTaskForMechanicByIdAsync(Guid taskId);
        Task<Guid> FillInWarrantyTask(FillInWarrantyTask request);
        Task<bool> UpdateTaskStatusAsync(Guid taskId, Guid userId);
        Task<List<Tasks>> GetTasksByGroupIdAsync(Guid taskGroupId);
        Task<RequestInfoDto> GetRequestInfoAsync(Guid requestId);
        Task<string> GetDeviceInfoAsync(Guid deviceId);
        Task<Guid> CreateWarrantyTaskWithGroup(CreateWarrantyTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateUninstallTaskWithGroup(CreateUninstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateInstallTaskWithGroup(CreateInstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateRepairTaskWithGroup(CreateRepairTaskRequest request, Guid userId, Guid? taskGroupId, int orderIndex);
        Task<bool> IsTaskProcessingInReqestAsync(Guid requestId, TaskType taskType);

    }
}
