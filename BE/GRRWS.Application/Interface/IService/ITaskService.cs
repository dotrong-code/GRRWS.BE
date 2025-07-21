using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;

namespace GRRWS.Application.Interface.IService
{
    public interface ITaskService
    {
        Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, GetAllSingleTasksRequest request);

        Task<Result> GetTaskDetailsAsync(Guid taskId);
        Task<Result> CreateTaskReportAsync(CreateTaskReportRequest request);
        //CRUD
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize); // New

        Task<Result> GetTasksByReportIdAsync(Guid reportId);


        Task<Result> UpdateUninstallDeviceInTask(Guid taskId, Guid mechanicId);

        ///New versions of create task
        #region create task
        Task<Result> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId);
        Task<Result> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId);
        Task<Result> CreateInstallTask(CreateInstallTaskRequest request, Guid userId);
        Task<Result> CreateRepairTask(CreateRepairTaskRequest request, Guid userId);
        #endregion
        Task<Result> UpdateTaskStatusAsync(Guid taskId, Guid userId);
        Task<Result> UpdateIsInstallDevice(Guid taskId, Guid? NewDeviceId);
        Task<Result> FillInWarrantyTask(FillInWarrantyTask request, Guid userId);
        Task<Result> UpdateWarrantyClaim(UpdateWarrantyClaimRequest request, Guid userId);
        Task<Result> CreateWarrantyReturnTask(CreateWarrantyReturnTaskRequest request, Guid userId);
        Task<Result> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetGetDetailWarrantyReturnTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type);
        Task<Result> GetDetailUninstallTaskForMechanicByIdAsync(Guid taskId);
        Task<Result> GetDetailInstallTaskForMechanicByIdAsync(Guid taskId);
        // ...existing code...
        // Add to ITaskService interface

        Task<Result> GetAllSingleTasksAsync(GetAllSingleTasksRequest request);
        Task<Result> GetAllGroupTasksAsync(int pageNumber, int pageSize);
        Task<Result> GetAllGroupTasksByMechanicIdAsync(int pageNumber, int pageSize, Guid mechanicId);
        Task<Result> GetGroupTasksByRequestIdAsync(GetTasksByRequestIdRequest request);
        Task<Result> GetMechanicRecommendationAsync(int pageSize, int pageIndex);
        Task<Result> ApplySuggestedTaskGroupAssignmentsAsync(Guid taskGroupId);
        Task<Result> ApplySuggestedTaskAssignmentAsync(Guid taskId, Guid? mechanicId = null);
        Task<Result> GetSuggestedTasksByTaskGroupIdAsync(Guid taskGroupId);

        Task<Result> ReInstallOldDevice(Guid taskId);
        Task<Result> InstallDevice(Guid taskId, Guid deviceId);
        Task<Result> ConfirmTask(Guid taskId, Guid mechanicId, TaskConfirmationDTO confirmation);
        Task<Result> CreateCombinedRepairAndReplacementTasks(CreateCombinedTaskRequest request, Guid userId);
        Task<Result> UpdateTaskAssigneeAsync(Guid taskId, Guid newAssigneeId, Guid updatedByUserId);
    }
}
