﻿using GRRWS.Domain.Entities;
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
        Task<List<GetTaskForMechanic>> GetTasksByMechanicIdAsync2(Guid mechanicId, GetAllSingleTasksRequest request);
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
        Task<Guid> FillInWarrantyTask(FillInWarrantyTask request, List<WarrantyClaimDocument> documents);
        Task UpdateWarrantyClaimAsync(UpdateWarrantyClaimRequest request, List<WarrantyClaimDocument> documents, Guid userId);
        Task<bool> UpdateTaskStatusAsync(Guid taskId, Guid userId);
        Task<List<Tasks>> GetTasksByGroupIdAsync(Guid taskGroupId);
        Task<RequestInfoDto> GetRequestInfoAsync(Guid requestId);
        Task<string> GetDeviceInfoAsync(Guid deviceId);
        Task<Guid> CreateWarrantyTaskWithGroup(CreateWarrantyTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateUninstallTaskWithGroup(CreateUninstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateInstallTaskWithGroup(CreateInstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);
        Task<Guid> CreateRepairTaskWithGroup(CreateRepairTaskRequest request, Guid userId, Guid? taskGroupId, int orderIndex);

        Task<Guid> UpdateUninstallDeviceInTask(Guid taskId, Guid mechanicId);


        Task<bool> IsTaskCompletedInReqestAsync(Guid requestId, TaskType taskType);
        // Add to ITaskRepository interface
        Task<(List<GetSingleTaskResponse> Tasks, int TotalCount)> GetAllSingleTasksAsync(string? taskType, string? status, string? priority, string? order, int pageNumber, int pageSize);
        Task<(List<GetGroupTaskResponse> Groups, int TotalCount)> GetAllGroupTasksAsync(int pageNumber, int pageSize);
        Task<(List<GetGroupTaskResponse> Groups, int TotalCount)> GetGroupTasksByRequestIdAsync(Guid requestId, int pageNumber, int pageSize);




        Task<List<Tasks>> GetTasksByMechanicInTimeRangeAsync(Guid mechanicId, DateTime startTime, DateTime endTime);
        Task<List<Tasks>> GetTasksByTaskGroupIdAsync(Guid taskGroupId);
        Task<List<Tasks>> GetSuggestedTasksByTaskGroupIdAsync(Guid taskGroupId);





        // Add new methods
        Task<List<Tasks>> GetTasksByWarrantyClaimIdAsync(Guid warrantyClaimId, TaskType taskType);
        Task<WarrantyClaim> GetWarrantyClaimAsync(Guid warrantyClaimId);
        Task<Guid> CreateWarrantyReturnTask(CreateWarrantyReturnTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex);

    }
}
