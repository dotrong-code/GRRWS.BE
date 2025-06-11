using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;

namespace GRRWS.Application.Interface.IService
{
    public interface ITaskService
    {
        Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize);

        Task<Result> GetTaskDetailsAsync(Guid taskId);
        Task<Result> CreateTaskReportAsync(CreateTaskReportRequest request);
        //CRUD
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize); // New

        Task<Result> GetTasksByReportIdAsync(Guid reportId);


        // New task creation methods
        Task<Result> CreateTaskFromErrorsAsync(CreateTaskFromErrorsRequest request);
        Task<Result> CreateTaskFromTechnicalIssueAsync(CreateTaskFromTechnicalIssueRequest request);
        Task<Result> CreateSimpleTaskAsync(CreateSimpleTaskRequest request);

        ///New versions of create task
        #region create task
        Task<Result> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId);
        Task<Result> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId);
        Task<Result> CreateInstallTask(CreateInstallTaskRequest request, Guid userId);
        Task<Result> CreateRepairTask(CreateRepairTaskRequest request, Guid userId);
        #endregion
        Task<Result> UpdateTaskStatusAsync(Guid taskId, Guid userId);
        Task<Result> FillInWarrantyTask(FillInWarrantyTask request);
        Task<Result> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type);


    }
}
