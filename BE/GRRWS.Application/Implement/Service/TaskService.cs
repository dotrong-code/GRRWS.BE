using FluentValidation;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Common.Validator.Task;
using GRRWS.Application.Interface.IService;

using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;
using Microsoft.Extensions.Logging;

namespace GRRWS.Application.Implement.Service
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ITaskGroupService _taskGroupService;
        private readonly IValidator<StartTaskRequest> _startTaskValidator;
        private readonly IValidator<CreateTaskReportRequest> _createReportValidator;
        private readonly CheckIsExist _checkIsExist;
        private readonly ILogger<TaskService> _logger;
        public TaskService(UnitOfWork unitOfWork,
            ITaskGroupService taskGroupService,
            IValidator<StartTaskRequest> startTaskValidator,
            IValidator<CreateTaskReportRequest> createReportValidator,
            CheckIsExist checkIsExist, ILogger<TaskService> logger)
        {
            _unitOfWork = unitOfWork;
            _taskGroupService = taskGroupService;
            _startTaskValidator = startTaskValidator;
            _createReportValidator = createReportValidator;
            _checkIsExist = checkIsExist;
            _logger = logger;
        }

        #region
        public async Task<Result> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId)
        {
            var requestCheck = await _checkIsExist.Request(request.RequestId);
            if (!requestCheck.IsSuccess) return requestCheck;

            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            var assigneeCheck = await _checkIsExist.User(request.AssigneeId, allowNull: true);
            if (!assigneeCheck.IsSuccess) return assigneeCheck;
            var isTaskProcessing = await IsTaskCompletedInRequestAsync(request.RequestId, TaskType.WarrantySubmission);
            if (!isTaskProcessing)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Conflict", "A warranty task is already being processed for this request."));

            try
            {
                var requestInfo = await _unitOfWork.TaskRepository.GetRequestInfoAsync(request.RequestId);
                if (requestInfo == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

                // Always create new warranty task group
                var taskGroupId = await _taskGroupService.CreateOrGetTaskGroupAsync(
                    null, // Always null to force creation
                    TaskType.Warranty,
                    requestInfo.DeviceName,
                    requestInfo.ReportId, // Use ReportId from RequestInfo
                    userId);

                var orderIndex = 1; // First task in new warranty group (WarrantySubmission)

                var taskId = await _unitOfWork.TaskRepository.CreateWarrantyTaskWithGroup(request, userId, taskGroupId, orderIndex);

                //Create mechanic shift
                var now = DateTime.Now;
                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

                var createdMechanicShift = await _unitOfWork.MechanicShiftRepository.CreateMechanicShift(userId, taskId, currentShift.Id);

                if (!createdMechanicShift)
                {
                    _logger.LogError("Failed to create mechanic shift for task {TaskId} at {Time}", taskId, now);
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Failed to create mechanic shift for the task.", 0));
                }

                return Result.SuccessWithObject(new
                {
                    Message = "Warranty task created successfully!",
                    TaskId = taskId,
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Error", ex.Message));
            }
        }
        public async Task<Result> CreateRepairTask(CreateRepairTaskRequest request, Guid userId)
        {
            var requestCheck = await _checkIsExist.Request(request.RequestId);
            if (!requestCheck.IsSuccess) return requestCheck;

            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            var assigneeCheck = await _checkIsExist.User(request.AssigneeId, allowNull: true);
            if (!assigneeCheck.IsSuccess) return assigneeCheck;
            var isTaskProcessing = await IsTaskCompletedInRequestAsync(request.RequestId, TaskType.Repair);
            if (!isTaskProcessing)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Conflict", "A repair task is already being processed for this request."));

            try
            {
                var requestInfo = await _unitOfWork.TaskRepository.GetRequestInfoAsync(request.RequestId);
                if (requestInfo == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

                // Always create new repair task group
                var taskGroupId = await _taskGroupService.CreateOrGetTaskGroupAsync(
                    null, // Always null to force creation
                    TaskType.Repair,
                    requestInfo.DeviceName,
                    requestInfo.ReportId, // Use ReportId from RequestInfo
                    userId);

                var orderIndex = 1; // First task in new repair group

                var taskId = await _unitOfWork.TaskRepository.CreateRepairTaskWithGroup(request, userId, taskGroupId, orderIndex);

                //Create mechanic shift
                var now = DateTime.Now;
                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

                var createdMechanicShift = await _unitOfWork.MechanicShiftRepository.CreateMechanicShift(userId, taskId, currentShift.Id);

                if (!createdMechanicShift)
                {
                    _logger.LogError("Failed to create mechanic shift for task {TaskId} at {Time}", taskId, now);
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Failed to create mechanic shift for the task.", 0));
                }

                return Result.SuccessWithObject(new
                {
                    Message = "Repair task created successfully!",
                    TaskId = taskId,
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Error", ex.Message));
            }
        }
        public async Task<Result> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId)
        {
            var requestCheck = await _checkIsExist.Request(request.RequestId);
            if (!requestCheck.IsSuccess) return requestCheck;

            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            var assigneeCheck = await _checkIsExist.User(request.AssigneeId, allowNull: true);
            if (!assigneeCheck.IsSuccess) return assigneeCheck;

            var taskGroupCheck = await _checkIsExist.TaskGroup(request.TaskGroupId, allowNull: true);
            if (!taskGroupCheck.IsSuccess) return taskGroupCheck;
            var isTaskProcessing = await IsTaskCompletedInRequestAsync(request.RequestId, TaskType.Uninstallation);
            if (!isTaskProcessing)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Conflict", "An uninstallation task is already being processed for this request."));
            try
            {
                var requestInfo = await _unitOfWork.TaskRepository.GetRequestInfoAsync(request.RequestId);
                if (requestInfo == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

                Guid taskGroupId;
                int orderIndex;

                if (request.TaskGroupId.HasValue)
                {
                    // Use existing task group
                    taskGroupId = request.TaskGroupId.Value;
                    var groupType = await GetExistingGroupTypeAsync(request.TaskGroupId.Value);

                    // For Warranty and Repair groups, Uninstall should always be OrderIndex 1
                    if (groupType == TaskType.Warranty || groupType == TaskType.Repair)
                    {
                        orderIndex = 1;
                        // Push all existing tasks down by 1
                        await _taskGroupService.UpdateExistingTasksOrderAsync(taskGroupId, 1, userId);
                    }
                    else // Replacement group
                    {
                        orderIndex = await _taskGroupService.GetNextOrderIndexAsync(taskGroupId, TaskType.Uninstallation);
                    }
                }
                else
                {
                    // Create new replacement task group
                    taskGroupId = await _taskGroupService.CreateOrGetTaskGroupAsync(
                        null,
                        TaskType.Replacement,
                        requestInfo.DeviceName,
                        requestInfo.ReportId, // Use ReportId from RequestInfo
                        userId);
                    orderIndex = 1; // First task in new replacement group
                }

                var taskId = await _unitOfWork.TaskRepository.CreateUninstallTaskWithGroup(request, userId, taskGroupId, orderIndex);

                //Create mechanic shift
                var now = DateTime.Now;
                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

                var createdMechanicShift = await _unitOfWork.MechanicShiftRepository.CreateMechanicShift(userId, taskId, currentShift.Id);

                if (!createdMechanicShift)
                {
                    _logger.LogError("Failed to create mechanic shift for task {TaskId} at {Time}", taskId, now);
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Failed to create mechanic shift for the task.", 0));
                }

                return Result.SuccessWithObject(new
                {
                    Message = "Uninstall task created successfully!",
                    TaskId = taskId,
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Error", ex.Message));
            }
        }
        public async Task<Result> CreateInstallTask(CreateInstallTaskRequest request, Guid userId)
        {
            var requestCheck = await _checkIsExist.Request(request.RequestId);
            if (!requestCheck.IsSuccess) return requestCheck;

            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            var assigneeCheck = await _checkIsExist.User(request.AssigneeId, allowNull: true);
            if (!assigneeCheck.IsSuccess) return assigneeCheck;

            var taskGroupCheck = await _checkIsExist.TaskGroup(request.TaskGroupId, allowNull: true);
            if (!taskGroupCheck.IsSuccess) return taskGroupCheck;

            var newDeviceCheck = await _checkIsExist.Device(request.NewDeviceId, allowNull: true);
            if (!newDeviceCheck.IsSuccess) return newDeviceCheck;
            var isTaskProcessing = await IsTaskCompletedInRequestAsync(request.RequestId, TaskType.Installation);
            if (!isTaskProcessing)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Conflict", "An installation task is already being processed for this request."));
            try
            {
                var requestInfo = await _unitOfWork.TaskRepository.GetRequestInfoAsync(request.RequestId);
                if (requestInfo == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

                var deviceInfo = request.NewDeviceId.HasValue
                    ? await _unitOfWork.TaskRepository.GetDeviceInfoAsync(request.NewDeviceId.Value)
                    : "Unknown Device";

                Guid taskGroupId;
                int orderIndex;

                if (request.TaskGroupId.HasValue)
                {
                    // Use existing task group
                    taskGroupId = request.TaskGroupId.Value;
                    orderIndex = await _taskGroupService.GetNextOrderIndexAsync(taskGroupId, TaskType.Installation);
                }
                else
                {
                    // Create new replacement task group
                    taskGroupId = await _taskGroupService.CreateOrGetTaskGroupAsync(
                        null,
                        TaskType.Replacement,
                        deviceInfo,
                        requestInfo.ReportId, // Use ReportId from RequestInfo
                        userId);
                    orderIndex = 1; // First task in new replacement group
                }

                var taskId = await _unitOfWork.TaskRepository.CreateInstallTaskWithGroup(request, userId, taskGroupId, orderIndex);

                //Create mechanic shift
                var now = DateTime.Now;
                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

                var createdMechanicShift = await _unitOfWork.MechanicShiftRepository.CreateMechanicShift(userId, taskId, currentShift.Id);

                if (!createdMechanicShift)
                {
                    _logger.LogError("Failed to create mechanic shift for task {TaskId} at {Time}", taskId, now);
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Failed to create mechanic shift for the task.", 0));
                }

                return Result.SuccessWithObject(new
                {
                    Message = "Install task created successfully!",
                    TaskId = taskId,
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Error", ex.Message));
            }
        }
        public async Task<Result> FillInWarrantyTask(FillInWarrantyTask request)
        {
            try
            {
                var taskId = await _unitOfWork.TaskRepository.FillInWarrantyTask(request);
                return Result.SuccessWithObject(new
                {
                    Message = "Warranty task completed successfully!",
                    TaskId = taskId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(TaskErrorMessage.TaskUpdateFailed(ex.Message));
            }
        }
        public async Task<Result> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetGetDetailWarrantyTaskForMechanicByIdAsync(taskId, TaskType.WarrantySubmission);
            if (task == null)
            {
                return Result.Failure(TaskErrorMessage.TaskNotExist());
            }
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> GetGetDetailWarrantyReturnTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetGetDetailWarrantyTaskForMechanicByIdAsync(taskId, TaskType.WarrantyReturn);
            if (task == null)
            {
                return Result.Failure(TaskErrorMessage.TaskNotExist());
            }
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetDetailtRepairTaskForMechanicByIdAsync(taskId, "Repair");
            if (task == null)
            {
                return Result.Failure(TaskErrorMessage.TaskNotExist());
            }
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> GetDetailUninstallTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetDetailUninstallTaskForMechanicByIdAsync(taskId);
            if (task == null)
            {
                return Result.Failure(TaskErrorMessage.TaskNotExist());
            }
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> GetDetailInstallTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetDetailInstallTaskForMechanicByIdAsync(taskId);
            if (task == null)
            {
                return Result.Failure(TaskErrorMessage.TaskNotExist());
            }
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> UpdateTaskStatusAsync(Guid taskId, Guid userId)

        {
            var taskCheck = await _checkIsExist.Task(taskId);
            if (!taskCheck.IsSuccess) return taskCheck;
            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;
            var isUpdated = await _unitOfWork.TaskRepository.UpdateTaskStatusAsync(taskId, userId);
            if (!isUpdated)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Fail", "Task status could not be updated."));
            return Result.SuccessWithObject(new
            {
                Message = "Task status updated successfully!",
                TaskId = taskId,
                UpdatedAt = DateTime.UtcNow
            });
        }

        // Add to TaskService implementation
        public async Task<Result> GetAllSingleTasksAsync(GetAllSingleTasksRequest request)
        {
            var validator = new GetAllSingleTasksValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e =>
                    Infrastructure.DTOs.Common.Error.Validation("ValidationError", e.ErrorMessage)).ToList();
                return Result.Failures(errors);
            }

            try
            {
                var (tasks, totalCount) = await _unitOfWork.TaskRepository.GetAllSingleTasksAsync(
                    request.TaskType,
                    request.Status,
                    request.Priority,
                    request.Order,
                    request.PageNumber,
                    request.PageSize);

                if (!tasks.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No single tasks found."));
                }

                var response = new PagedResponse<GetSingleTaskResponse>
                {
                    Data = tasks,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }

        public async Task<Result> GetAllGroupTasksAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page number must be greater than 0"));
            }

            if (pageSize <= 0 || pageSize > 100)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page size must be between 1 and 100"));
            }

            try
            {
                var (groups, totalCount) = await _unitOfWork.TaskRepository.GetAllGroupTasksAsync(pageNumber, pageSize);

                if (!groups.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No task groups found."));
                }

                var response = new PagedResponse<GetGroupTaskResponse>
                {
                    Data = groups,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }

        public async Task<Result> GetGroupTasksByRequestIdAsync(GetTasksByRequestIdRequest request)
        {
            var validator = new GetTasksByRequestIdValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e =>
                    Infrastructure.DTOs.Common.Error.Validation("ValidationError", e.ErrorMessage)).ToList();
                return Result.Failures(errors);
            }

            // Check if request exists
            var requestCheck = await _checkIsExist.Request(request.RequestId);
            if (!requestCheck.IsSuccess) return requestCheck;

            try
            {
                var (groups, totalCount) = await _unitOfWork.TaskRepository.GetGroupTasksByRequestIdAsync(
                    request.RequestId,
                    request.PageNumber,
                    request.PageSize);

                if (!groups.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No task groups found for this request."));
                }

                var response = new PagedResponse<GetGroupTaskResponse>
                {
                    Data = groups,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };

                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }

        #endregion
        #region old methods
        public async Task<Result> GetTasksByReportIdAsync(Guid reportId)
        {
            var reportExists = await _unitOfWork.ReportRepository.GetByIdAsync(reportId) != null;
            if (!reportExists)
                return Result.Failure(TaskErrorMessage.ReportNotExist());

            var tasks = await _unitOfWork.TaskRepository.GetTasksByReportIdAsync(reportId);
            if (!tasks.Any())
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            return Result.SuccessWithObject(tasks);
        }
        public async Task<Result> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize)
        {
            var (tasks, totalCount) = await _unitOfWork.TaskRepository.GetAllTasksAsync(taskType, status, priority, pageNumber, pageSize);
            if (!tasks.Any())
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            var response = new PagedResponse<GetTaskResponse>
            {
                Data = tasks,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }
        public async Task<Result> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(request.Id);
            if (task == null)
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            var userExists = await _unitOfWork.UserRepository.GetByIdAsync(request.AssigneeId) != null;
            if (!userExists)
                return Result.Failure(TaskErrorMessage.UserNotExist());

            task.TaskName = request.TaskName;
            task.TaskDescription = request.TaskDescription;
            //task.TaskType = request.TaskType;
            task.ExpectedTime = request.ExpectedTime;
            task.AssigneeId = request.AssigneeId;
            task.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> DeleteTaskAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(taskId);
            if (task == null)
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            task.IsDeleted = true;
            task.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(true);
        }
        public async Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize)
        {
            var userExists = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId) != null;
            if (!userExists)
                return Result.Failure(TaskErrorMessage.UserNotExist());

            //var tasks = await _unitOfWork.TaskRepository.GetTasksByMechanicIdAsync(mechanicId, pageNumber, pageSize);
            var tasks = await _unitOfWork.TaskRepository.GetTasksByMechanicIdAsync2(mechanicId, pageNumber, pageSize);
            var response = new PagedResponse<GetTaskForMechanic>
            {
                Data = tasks,
                TotalCount = tasks.Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetTaskDetailsAsync(Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskDetailsAsync(taskId);
            if (task == null)
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            return Result.SuccessWithObject(task);
        }
        public async Task<Result> CreateTaskReportAsync(CreateTaskReportRequest request)
        {
            var validationResult = await _createReportValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(request.TaskId);
            if (task == null)
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            return Result.Failure(TaskErrorMessage.InvalidStatusTransition());

            if (task.DeviceReturnTime.HasValue)
                return Result.Failure(TaskErrorMessage.ReportAlreadyCreated());

            task.EndTime = DateTime.UtcNow;
            task.DeviceReturnTime = request.DeviceReturnTime;
            task.DeviceCondition = request.DeviceCondition;
            task.ReportNotes = request.ReportNotes;

            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(task);
        }
        public async Task<Result> GetAllAsync()
        {
            var tasks = await _unitOfWork.TaskRepository.GetAllTasksAsync();
            var dtos = tasks
                .Where(t => !t.IsDeleted)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType.ToString(),
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee?.UserName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    CreatedDate = t.CreatedDate,
                    ModifiedDate = t.ModifiedDate,
                    ModifiedBy = t.ModifiedBy,
                    Errors = t.ErrorDetails?.Select(ed => new ErrorSimpleDTO
                    {
                        Id = ed.Error.Id,
                        Name = ed.Error.Name
                    }).ToList(),
                    Spareparts = t.RepairSpareparts?.Select(rs => new SparepartSimpleDTO
                    {
                        Id = rs.Sparepart.Id,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList()
                }).ToList();

            return Result.SuccessWithObject(dtos);
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(id);
            if (task == null || task.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Task not found.", 0));

            var dto = new TaskDTO
            {
                Id = task.Id,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskType = task.TaskType.ToString(),
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeId = task.AssigneeId,
                AssigneeName = task.Assignee?.UserName,
                DeviceReturnTime = task.DeviceReturnTime,
                DeviceCondition = task.DeviceCondition,
                ReportNotes = task.ReportNotes,
                CreatedDate = task.CreatedDate,
                ModifiedDate = task.ModifiedDate,
                ModifiedBy = task.ModifiedBy,
                Errors = task.ErrorDetails?.Select(ed => new ErrorSimpleDTO
                {
                    Id = ed.Error.Id,
                    Name = ed.Error.Name
                }).ToList(),
                Spareparts = task.RepairSpareparts?.Select(rs => new SparepartSimpleDTO
                {
                    Id = rs.Sparepart.Id,
                    SparepartName = rs.Sparepart.SparepartName
                }).ToList()
            };

            return Result.SuccessWithObject(dto);
        }
        #endregion
        #region private methods
        // You can add any private methods here if needed for internal logic

        private async Task<TaskType> GetExistingGroupTypeAsync(Guid taskGroupId)
        {
            var taskGroup = await _unitOfWork.TaskGroupRepository.GetByIdAsync(taskGroupId);
            return taskGroup?.GroupType ?? TaskType.Replacement;
        }
        private static bool ShouldUpdateExistingTasks(TaskType groupType, TaskType taskType)
        {
            // This method is no longer needed since we handle the logic directly in CreateUninstallTask
            // But keeping it for backward compatibility if used elsewhere
            return (groupType == TaskType.Warranty && taskType == TaskType.Uninstallation) ||
                   (groupType == TaskType.Repair && taskType == TaskType.Uninstallation);
        }
        public Task<Result> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> IsTaskCompletedInRequestAsync(Guid requestId, TaskType taskType)
        {
            return await _unitOfWork.TaskRepository.IsTaskCompletedInReqestAsync(requestId, taskType);
        }
        public async Task<Result> GetMechanicRecommendationAsync(int pageSize, int pageIndex)
        {
            try
            {
                _logger.LogInformation("Fetching recommended mechanics for current time");

                var now = DateTime.Now;

                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

                if (currentShift == null)
                {
                    _logger.LogWarning("No shift found for current time");
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No shift is active at the current time.", 0));
                }

                var recommendations = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(now, currentShift.Id, pageIndex, pageSize);

                if (!recommendations.Any())
                {
                    _logger.LogWarning("No available mechanics found for shift {ShiftId} on {Date}", currentShift.Id, now.Date);
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No available mechanics found for the current shift.", 0));
                }

                return Result.SuccessWithObject(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching recommended mechanics for current time");
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "An error occurred while fetching mechanic recommendations.", 0));
            }
        }
        #endregion

    }
}