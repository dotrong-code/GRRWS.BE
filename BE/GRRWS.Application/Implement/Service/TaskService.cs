using FluentValidation;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Common.Validator.Task;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
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
        private readonly IMechanicShiftService _mechanicShiftService;
        public TaskService(UnitOfWork unitOfWork,
            ITaskGroupService taskGroupService,
            IValidator<StartTaskRequest> startTaskValidator,
            IValidator<CreateTaskReportRequest> createReportValidator,
            CheckIsExist checkIsExist, ILogger<TaskService> logger, IMechanicShiftService mechanicShiftService)
        {
            _unitOfWork = unitOfWork;
            _taskGroupService = taskGroupService;
            _startTaskValidator = startTaskValidator;
            _createReportValidator = createReportValidator;
            _checkIsExist = checkIsExist;
            _logger = logger;
            _mechanicShiftService = mechanicShiftService;
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
                request.StartDate = TimeHelper.GetHoChiMinhTime(); // Set start date to current time
                var taskId = await _unitOfWork.TaskRepository.CreateWarrantyTaskWithGroup(request, userId, taskGroupId, orderIndex);

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
        public async Task<Result> FillInWarrantyTask(FillInWarrantyTask request,Guid UserID)
        {
            // Prepare documents for upload
            var documents = new List<WarrantyClaimDocument>();
            if (request.DocumentFiles != null && request.DocumentFiles.Any())
            {
                foreach (var file in request.DocumentFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Validate file size (e.g., max 5MB)
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", $"File {file.FileName} exceeds 5MB limit."));
                        }

                        var imageRequest = new AddImageRequest(file, "WarrantyClaimDocuments");
                        var uploadResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
                        if (!uploadResult.Success)
                        {
                            return Result.Failure(Infrastructure.DTOs.Common.Error.Failure($"Failed to upload document","Failed"));
                        }

                        var document = new WarrantyClaimDocument
                        {
                            Id = Guid.NewGuid(),
                            DocumentType = "Photos", // Assuming photos for appointment slips
                            DocumentName = file.FileName,
                            DocumentUrl = uploadResult.FilePath,
                            Description = request.DocumentDescription,
                            UploadDate = TimeHelper.GetHoChiMinhTime(),
                            WarrantyClaimId = request.TaskId, // Will be updated later with correct WarrantyClaimId
                            UploadedByUserId = UserID, // Use task creator
                            IsDeleted = false
                        };

                        documents.Add(document);
                    }
                }
            }
            try
            {
                var taskId = await _unitOfWork.TaskRepository.FillInWarrantyTask(request,documents);
                return Result.SuccessWithObject(new
                {
                    Message = "Warranty task information filled successfully!",
                    TaskId = taskId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(TaskErrorMessage.TaskUpdateFailed(ex.Message));
            }
        }
        public async Task<Result> UpdateWarrantyClaim(UpdateWarrantyClaimRequest request, Guid userId)
        {
            

            // Validate user existence
            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            // Handle document uploads
            var documents = new List<WarrantyClaimDocument>();
            if (request.DocumentFiles != null && request.DocumentFiles.Any())
            {
                foreach (var file in request.DocumentFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        var imageRequest = new AddImageRequest(file, "WarrantyClaimDocuments");
                        var uploadResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
                        if (!uploadResult.Success)
                        {
                            return Result.Failure(Infrastructure.DTOs.Common.Error.Failure($"Failed to upload document", "Failed"));
                        }

                        var document = new WarrantyClaimDocument
                        {
                            Id = Guid.NewGuid(),
                            DocumentType = "Photos",
                            DocumentName = file.FileName,
                            DocumentUrl = uploadResult.FilePath,
                            Description = request.DocumentDescription,
                            UploadDate = TimeHelper.GetHoChiMinhTime(),
                            WarrantyClaimId = request.WarrantyClaimId,
                            UploadedByUserId = userId,
                            IsDeleted = false
                        };

                        documents.Add(document);
                    }
                }
            }

            try
            {
                await _unitOfWork.TaskRepository.UpdateWarrantyClaimAsync(request, documents, userId);
                return Result.SuccessWithObject(new
                {
                    Message = "Warranty claim updated successfully!",
                    WarrantyClaimId = request.WarrantyClaimId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating warranty claim for WarrantyClaimId: {WarrantyClaimId}", request.WarrantyClaimId);
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("UpdateFailed", ex.Message));
            }
        }
        public async Task<Result> CreateWarrantyReturnTask(CreateWarrantyReturnTaskRequest request, Guid userId)
        {
            

            // Validate user existence
            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            // Validate assignee existence (if provided)
            var assigneeCheck = await _checkIsExist.User(request.AssigneeId, allowNull: true);
            if (!assigneeCheck.IsSuccess) return assigneeCheck;

            // Check if a warranty return task already exists for this claim
            var existingReturnTask = await _unitOfWork.TaskRepository.GetTasksByWarrantyClaimIdAsync(request.WarrantyClaimId, TaskType.WarrantyReturn);
            if (existingReturnTask.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Conflict", "A warranty return task already exists for this warranty claim."));
            }

            // Validate ActualReturnDate for early return
            if (request.IsEarlyReturn && !request.ActualReturnDate.HasValue)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "ActualReturnDate is required when IsEarlyReturn is true."));
            }

            try
            {
                // Get warranty claim details
                var warrantyClaim = await _unitOfWork.TaskRepository.GetWarrantyClaimAsync(request.WarrantyClaimId);
                if (warrantyClaim == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Warranty claim not found."));
                }

                // Validate ActualReturnDate against ExpectedReturnDate for early return
                if (request.IsEarlyReturn && warrantyClaim.ExpectedReturnDate.HasValue && request.ActualReturnDate > warrantyClaim.ExpectedReturnDate)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Actual return date cannot be later than expected return date for early return."));
                }

                // Get task group associated with the warranty submission task
                var submissionTask = await _unitOfWork.TaskRepository.GetTaskByIdAsync(warrantyClaim.SubmittedByTaskId ?? Guid.Empty);
                if (submissionTask == null || !submissionTask.TaskGroupId.HasValue)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No associated warranty submission task or task group found."));
                }

                var taskGroupId = submissionTask.TaskGroupId.Value;
                var orderIndex = await _taskGroupService.GetNextOrderIndexAsync(taskGroupId, TaskType.WarrantyReturn);

                // Determine ActualReturnDate for WarrantyClaim update
                DateTime actualReturnDate = request.IsEarlyReturn
                    ? request.ActualReturnDate.Value // Use provided ActualReturnDate for early return
                    : (request.ActualReturnDate ?? warrantyClaim.ExpectedReturnDate ?? TimeHelper.GetHoChiMinhTime()); // Fallback to ExpectedReturnDate or current time

                // Create the warranty return task
                var taskId = await _unitOfWork.TaskRepository.CreateWarrantyReturnTask(request, userId, taskGroupId, orderIndex);

                // Update WarrantyClaim
                warrantyClaim.ActualReturnDate = actualReturnDate;
                warrantyClaim.WarrantyNotes = request.WarrantyNotes ?? warrantyClaim.WarrantyNotes;         
                warrantyClaim.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                warrantyClaim.ModifiedBy = userId;
                warrantyClaim.ReturnTaskId = taskId;

                await _unitOfWork.WarrantyClaimRepository.UpdateAsync(warrantyClaim);
                await _unitOfWork.SaveChangesAsync();

                return Result.SuccessWithObject(new
                {
                    Message = "Warranty return task created successfully!",
                    TaskId = taskId,
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating warranty return task for WarrantyClaimId: {WarrantyClaimId}", request.WarrantyClaimId);
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict("Error", ex.Message));
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
        public async Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, GetAllSingleTasksRequest request)
        {
            var userExists = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId) != null;
            if (!userExists)
                return Result.Failure(TaskErrorMessage.UserNotExist());

            //var tasks = await _unitOfWork.TaskRepository.GetTasksByMechanicIdAsync(mechanicId, pageNumber, pageSize);
            var tasks = await _unitOfWork.TaskRepository.GetTasksByMechanicIdAsync2(mechanicId, request);
            var response = new PagedResponse<GetTaskForMechanic>
            {
                Data = tasks,
                TotalCount = tasks.Count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
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
        //public async Task<Result> GetMechanicRecommendationAsync(int pageSize, int pageIndex)
        //{
        //    if (pageSize <= 0 || pageSize > 100)
        //    {
        //        return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page size must be between 1 and 100"));
        //    }
        //    if (pageIndex < 0)
        //    {
        //        return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page index must be 0 or greater"));
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Fetching recommended mechanics for current time");

        //        var now = DateTime.Now;

        //        var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(now);

        //        if (currentShift == null)
        //        {
        //            _logger.LogWarning("No shift found for current time");
        //            return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No shift is active at the current time."));
        //        }

        //        var recommendations = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(now, currentShift.Id, pageIndex, pageSize);

        //        if (!recommendations.Any())
        //        {
        //            _logger.LogWarning("No available mechanics found for shift {ShiftId} on {Date}", currentShift.Id, now.Date);
        //            return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "No available mechanics found for the current shift."));
        //        }

        //        return Result.SuccessWithObject(recommendations);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching recommended mechanics for current time");
        //        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "An error occurred while fetching mechanic recommendations.", 0));
        //    }
        //}
        public async Task<Result> GetMechanicRecommendationAsync(int pageSize, int pageIndex)
        {
            if (pageSize <= 0 || pageSize > 100)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page size must be between 1 and 100"));
            }
            if (pageIndex < 0)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page index must be 0 or greater"));
            }

            try
            {
                _logger.LogInformation("Fetching recommended mechanics for current time");

                var now = DateTime.Now;

                var recommendations = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(now, pageIndex, pageSize);

                return Result.SuccessWithObject(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching recommended mechanics for current time");
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "An error occurred while fetching mechanic recommendations.", 0));
            }
        }
        public async Task<Result> ApplySuggestedTaskGroupAssignmentsAsync(Guid taskGroupId)
        {
            try
            {
                // Get all suggested tasks in the task group
                var suggestedTasks = await _unitOfWork.TaskRepository.GetTasksByTaskGroupIdAsync(taskGroupId);
                var tasksToApply = suggestedTasks.Where(t => t.Status == Status.Suggested).ToList();

                if (!tasksToApply.Any())
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoSuggestedTasks", "No suggested tasks found in this task group.", 0));
                }

                // Get current shift for assignment
                var currentTime = DateTime.Now;
                var currentShift = await _unitOfWork.ShiftRepository.GetCurrentShiftAsync(currentTime);
                if (currentShift == null)
                {
                    currentShift = await _unitOfWork.ShiftRepository.GetNearestShiftAsync(currentTime);
                }

                if (currentShift == null)
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoShiftAvailable", "No shift available for assignment.", 0));
                }

                // Get available mechanics using the existing recommendation system
                var availableMechanics = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 10);

                if (!availableMechanics.Any())
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoAvailableMechanics", "No available mechanics for assignment.", 0));
                }

                // Apply the same assignment logic as AutoAssignedTask
                var uninstallTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.Uninstallation);
                var warrantyTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.WarrantySubmission);
                var installTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.Installation);

                // Select mechanics based on availability and performance
                var primaryMechanic = availableMechanics.First(); // Best available mechanic
                var secondaryMechanic = availableMechanics.Count > 1 ? availableMechanics[1] : primaryMechanic;

                var uninstallWarrantyMechanicId = primaryMechanic.MechanicId;
                var installMechanicId = secondaryMechanic.MechanicId;

                var appliedTasks = new List<object>();
                _unitOfWork.ClearChangeTracker();
                // Apply assignments to Uninstall and Warranty tasks (same mechanic)
                if (uninstallTask != null)
                {
                    uninstallTask.AssigneeId = uninstallWarrantyMechanicId;
                    uninstallTask.Status = Status.Pending;
                    uninstallTask.ModifiedDate = DateTime.Now;
                    uninstallTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unitOfWork.TaskRepository.UpdateAsync(uninstallTask);

                    // Create mechanic shift for uninstall task
                    var uninstallShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, uninstallTask.Id);

                    appliedTasks.Add(new { TaskId = uninstallTask.Id, TaskType = "Uninstallation", MechanicId = uninstallWarrantyMechanicId });
                }
                _unitOfWork.ClearChangeTracker();
                if (warrantyTask != null)
                {
                    warrantyTask.AssigneeId = uninstallWarrantyMechanicId;
                    warrantyTask.Status = Status.Pending;
                    warrantyTask.ModifiedDate = DateTime.Now;
                    warrantyTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unitOfWork.TaskRepository.UpdateAsync(warrantyTask);

                    // Create mechanic shift for warranty task
                    var warrantyShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, warrantyTask.Id);

                    appliedTasks.Add(new { TaskId = warrantyTask.Id, TaskType = "WarrantySubmission", MechanicId = uninstallWarrantyMechanicId });
                }
                _unitOfWork.ClearChangeTracker();
                // Apply assignment to Install task (different mechanic)
                if (installTask != null)
                {
                    installTask.AssigneeId = installMechanicId;
                    installTask.Status = Status.Pending;
                    installTask.ModifiedDate = DateTime.Now;
                    installTask.ExpectedTime = secondaryMechanic.ExpectedTime;
                    await _unitOfWork.TaskRepository.UpdateAsync(installTask);

                    // Create mechanic shift for install task
                    var installShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(installMechanicId, installTask.Id);

                    appliedTasks.Add(new { TaskId = installTask.Id, TaskType = "Installation", MechanicId = installMechanicId });
                }

                await _unitOfWork.SaveChangesAsync();

                return Result.SuccessWithObject(new
                {
                    Message = "Suggested task assignments applied successfully!",
                    TaskGroupId = taskGroupId,
                    AppliedTasks = appliedTasks,
                    PrimaryMechanicId = uninstallWarrantyMechanicId,
                    SecondaryMechanicId = installMechanicId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("AssignmentError", $"Failed to apply suggested assignments: {ex.Message}", 0));
            }
        }
        public async Task<Result> ApplySuggestedTaskAssignmentAsync(Guid taskId, Guid? mechanicId = null)
        {
            try
            {
                // Get the suggested task
                var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("TaskNotFound", "Task not found.", 0));
                }

                if (task.Status != Status.Suggested)
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("InvalidStatus", "Task is not in suggested status.", 0));
                }

                Guid assignedMechanicId;

                if (mechanicId.HasValue)
                {
                    // Manual assignment - verify mechanic exists and is available
                    var mechanic = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId.Value);
                    if (mechanic == null)
                    {
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("MechanicNotFound", "Mechanic not found.", 0));
                    }

                    // Check if mechanic is available at the current time
                    var currentTime = DateTime.Now;
                    var mechanicShift = await _unitOfWork.MechanicShiftRepository.GetCurrentShiftAsync(mechanicId.Value, currentTime);
                    if (mechanicShift != null && !mechanicShift.IsAvailable)
                    {
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("MechanicUnavailable", "Mechanic is not available at this time.", 0));
                    }

                    assignedMechanicId = mechanicId.Value;
                }
                else
                {
                    // Auto assignment - get best available mechanic
                    var currentTime = DateTime.Now;
                    var availableMechanics = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 1);

                    if (!availableMechanics.Any())
                    {
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("NoAvailableMechanics", "No available mechanics for assignment.", 0));
                    }

                    assignedMechanicId = availableMechanics.First().MechanicId;
                }

                // Apply the assignment
                task.AssigneeId = assignedMechanicId;
                task.Status = Status.Pending;
                task.ModifiedDate = DateTime.Now;
                await _unitOfWork.TaskRepository.UpdateAsync(task);

                // Create mechanic shift for the task
                var shiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(assignedMechanicId, taskId);

                await _unitOfWork.SaveChangesAsync();

                return Result.SuccessWithObject(new
                {
                    Message = "Suggested task assignment applied successfully!",
                    TaskId = taskId,
                    AssignedMechanicId = assignedMechanicId,
                    AssignmentType = mechanicId.HasValue ? "Manual" : "Auto"
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("AssignmentError", $"Failed to apply suggested assignment: {ex.Message}", 0));
            }
        }
        public async Task<Result> GetSuggestedTasksByTaskGroupIdAsync(Guid taskGroupId)
        {
            try
            {
                var suggestedTasks = await _unitOfWork.TaskRepository.GetSuggestedTasksByTaskGroupIdAsync(taskGroupId);

                var tasksResponse = suggestedTasks.Select(t => new
                {
                    TaskId = t.Id,
                    TaskName = t.TaskName,
                    TaskType = t.TaskType.ToString(),
                    TaskDescription = t.TaskDescription,
                    Priority = t.Priority.ToString(),
                    Status = t.Status.ToString(),
                    ExpectedTime = t.ExpectedTime,
                    CreatedDate = t.CreatedDate,
                    OrderIndex = t.OrderIndex
                }).ToList();

                return Result.SuccessWithObject(new
                {
                    TaskGroupId = taskGroupId,
                    SuggestedTasks = tasksResponse,
                    TotalCount = tasksResponse.Count
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("RetrievalError", $"Failed to retrieve suggested tasks: {ex.Message}", 0));
            }
        }
        #endregion
    }
}