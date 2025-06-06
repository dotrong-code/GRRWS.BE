using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Task;

namespace GRRWS.Application.Implement.Service
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<StartTaskRequest> _startTaskValidator;
        private readonly IValidator<CreateTaskReportRequest> _createReportValidator;

        public TaskService(UnitOfWork unitOfWork,
            IValidator<StartTaskRequest> startTaskValidator,
            IValidator<CreateTaskReportRequest> createReportValidator)
        {
            _unitOfWork = unitOfWork;
            _startTaskValidator = startTaskValidator;
            _createReportValidator = createReportValidator;
        }

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

        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            var userExists = await _unitOfWork.UserRepository.GetByIdAsync(request.AssigneeId) != null;
            if (!userExists)
                return Result.Failure(TaskErrorMessage.UserNotExist());

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskName = request.TaskName,
                TaskDescription = request.TaskDescription,
                TaskType = request.TaskType,
                Priority = (Domain.Enum.Priority)request.Priority,
                Status = Status.Pending, // Use enum value
                ExpectedTime = request.ExpectedTime,
                AssigneeId = request.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _unitOfWork.TaskRepository.CreateAsync(task);
            await _unitOfWork.TaskRepository.UpdateErrorDetailsAsync(request.ErrorDetailIds, task.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(task);
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
            task.TaskType = request.TaskType;
            task.Priority = (Domain.Enum.Priority)request.Priority;
            // Convert string to enum for Status
            if (Enum.TryParse<Status>(request.Status, out var statusEnum))
                task.Status = statusEnum;
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

            var tasks = await _unitOfWork.TaskRepository.GetTasksByMechanicIdAsync(mechanicId, pageNumber, pageSize);
            if (!tasks.Any())
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            var response = new PagedResponse<GetTaskResponse>
            {
                Data = tasks,
                TotalCount = tasks.Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> StartTaskAsync(StartTaskRequest request)
        {
            var validationResult = await _startTaskValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(request.TaskId);
            if (task == null)
                return Result.Failure(TaskErrorMessage.TaskNotExist());

            if (task.Status != Status.Pending) // Use enum comparison
                return Result.Failure(TaskErrorMessage.InvalidStatusTransition());

            task.Status = Status.InProgress; // Use enum value
            task.StartTime = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(new { Message = "Task start successfully!" });
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

            if (task.Status != Status.InProgress) // Use enum comparison
                return Result.Failure(TaskErrorMessage.InvalidStatusTransition());

            if (task.DeviceReturnTime.HasValue)
                return Result.Failure(TaskErrorMessage.ReportAlreadyCreated());

            task.Status = Status.Completed; // Use enum value
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
                    TaskType = t.TaskType,
                    Priority = (int?)t.Priority, // Convert enum to int
                    Status = t.Status.ToString(), // Convert enum to string
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee?.UserName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    CreatedDate = t.CreatedDate,
                    CreatedBy = t.CreatedBy ?? Guid.Empty, // Handle nullable
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
                TaskType = task.TaskType,
                Priority = (int?)task.Priority, // Convert enum to int
                Status = task.Status.ToString(), // Convert enum to string
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeId = task.AssigneeId,
                AssigneeName = task.Assignee?.UserName,
                DeviceReturnTime = task.DeviceReturnTime,
                DeviceCondition = task.DeviceCondition,
                ReportNotes = task.ReportNotes,
                CreatedDate = task.CreatedDate,
                CreatedBy = task.CreatedBy ?? Guid.Empty, // Handle nullable
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

        public async Task<Result> CreateAsync(CreateTaskDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TaskName))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Task name cannot be empty.", 0));
            
            var assignee = await _unitOfWork.UserRepository.GetByIdAsync(dto.AssigneeId);
            if (assignee == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Assignee user does not exist.", 0));
            
            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskName = dto.TaskName,
                TaskDescription = dto.TaskDescription,
                TaskType = dto.TaskType,
                Priority = dto.Priority.HasValue ? (Priority)dto.Priority.Value : Priority.Low, // Convert int to enum
                Status = !string.IsNullOrEmpty(dto.Status) && Enum.TryParse<Status>(dto.Status, out var status) 
                    ? status : Status.Pending, // Convert string to enum
                StartTime = dto.StartTime,
                ExpectedTime = dto.ExpectedTime,
                AssigneeId = dto.AssigneeId,
                DeviceReturnTime = dto.DeviceReturnTime,
                DeviceCondition = dto.DeviceCondition,
                ReportNotes = dto.ReportNotes,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            // Link Errors and Spareparts
            await _unitOfWork.TaskRepository.CreateTaskAsync(task, dto.ReportId, dto.ErrorIds, dto.SparepartIds);

            return Result.SuccessWithObject(new { Message = "Task created successfully!", TaskId = task.Id });
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateTaskDTO dto)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(id);
            if (task == null || task.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Task not found.", 0));

            task.TaskName = dto.TaskName;
            task.TaskDescription = dto.TaskDescription;
            task.TaskType = dto.TaskType;
            task.Priority = dto.Priority.HasValue ? (Priority)dto.Priority.Value : Priority.Low; // Convert int to enum
            // Convert string to enum for Status
            if (!string.IsNullOrEmpty(dto.Status) && Enum.TryParse<Status>(dto.Status, out var status))
                task.Status = status;
            task.StartTime = dto.StartTime;
            task.ExpectedTime = dto.ExpectedTime;
            task.EndTime = dto.EndTime;
            task.AssigneeId = dto.AssigneeId;
            task.DeviceReturnTime = dto.DeviceReturnTime;
            task.DeviceCondition = dto.DeviceCondition;
            task.ReportNotes = dto.ReportNotes;
            task.ModifiedBy = dto.ModifiedBy;
            task.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateTaskAsync(task, dto.ErrorIds, dto.SparepartIds);

            return Result.SuccessWithObject(new { Message = "Task updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(id);
            if (task == null || task.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Task not found.", 0));

            task.IsDeleted = true;
            task.ModifiedDate = DateTime.UtcNow;
            await _unitOfWork.TaskRepository.UpdateAsync(task);

            return Result.SuccessWithObject(new { Message = "Task deleted successfully!" });
        }

        public async Task<Result> AssignTaskAsync(Guid taskId, AssignTaskDTO dto)
        {
            var task = await _unitOfWork.TaskRepository.GetTaskByIdAsync(taskId);
            if (task == null || task.IsDeleted)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Task not found.", 0));

            task.AssigneeId = dto.AssigneeId;
            task.ModifiedBy = dto.ModifiedBy;
            task.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateAsync(task);

            return Result.SuccessWithObject(new { Message = "Task assigned successfully!" });
        }

        public async Task<Result> CreateTaskWebAsync(CreateTaskWeb dto)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }

            var userExists = await _unitOfWork.UserRepository.IdExistsAsync(dto.AssigneeId);
            if (!userExists)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User not found"));
            }
            var missingErrors = await _unitOfWork.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(dto.ErrorIds);
            if (missingErrors.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));
            }
            var task = await _unitOfWork.TaskRepository.CreateTaskWebAsync(dto);
            return Result.SuccessWithObject(new { Message = "Task assigned successfully!" });
        }

        public async Task<Result> CreateSimpleTaskWebAsync(CreateSimpleTaskWeb dto)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }

            var userExists = await _unitOfWork.UserRepository.IdExistsAsync(dto.AssigneeId);
            if (!userExists)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User not found"));
            }

            var taskId = await _unitOfWork.TaskRepository.CreateSimpleTaskWebAsync(dto);
            return Result.SuccessWithObject(new { Message = "Simple task created successfully!", TaskId = taskId });
        }

        public async Task<Result> CreateTaskFromTechnicalIssueAsync(CreateTaskFromTechnicalIssueRequest request)
        {
            // Validate Request exists
            var requestEntity = await _unitOfWork.RequestRepository.GetByIdAsync(request.RequestId);
            if (requestEntity == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));
            }

            // Validate User exists
            var userExists = await _unitOfWork.UserRepository.IdExistsAsync(request.AssigneeId);
            if (!userExists)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User not found"));
            }

            // Validate Technical Issues exist
            if (request.TechnicalIssueIds == null || !request.TechnicalIssueIds.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not Found", "At least one technical issue must be specified"));
            }

            // Get reportId from request
            var reportId = await _unitOfWork.ErrorDetailRepository.GetReportIdByRequestIdAsync(request.RequestId);
            if (reportId == Guid.Empty)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Report not found for this request"));
            }

            var taskId = await _unitOfWork.TaskRepository.CreateTaskFromTechnicalIssueAsync(request);
            return Result.SuccessWithObject(new { Message = "Warranty task created from technical issue successfully!", TaskId = taskId });
        }

        public async Task<Result> CreateSimpleTaskAsync(CreateSimpleTaskRequest request)
        {
            // Validate Request exists
            var requestEntity = await _unitOfWork.RequestRepository.GetByIdAsync(request.RequestId);
            if (requestEntity == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));
            }

            // Validate User exists
            var userExists = await _unitOfWork.UserRepository.IdExistsAsync(request.AssigneeId);
            if (!userExists)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User not found"));
            }

            // Validate Device to Remove exists
            if (request.DeviceToRemoveId == Guid.Empty)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not Found", "Device to remove must be specified"));
            }

            var deviceExists = await _unitOfWork.DeviceRepository.GetByIdAsync(request.DeviceToRemoveId);
            if (deviceExists == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Device to remove not found"));
            }

            // Validate Replacement Device if provided
            if (request.ReplacementDeviceId.HasValue)
            {
                var replacementDeviceExists = await _unitOfWork.DeviceRepository.GetByIdAsync(request.ReplacementDeviceId.Value);
                if (replacementDeviceExists == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Replacement device not found"));
                }
            }

            // Validate Installation Location
            if (string.IsNullOrWhiteSpace(request.InstallationLocation))
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "Installation location is required"));
            }

            // Validate at least one action is selected
            if (!request.BringDeviceToRepairPlace && !request.SetupReplacementDevice)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one replacement action must be selected"));
            }

            var taskId = await _unitOfWork.TaskRepository.CreateSimpleTaskAsync(request);
            return Result.SuccessWithObject(new
            {
                Message = "Device replacement task created successfully!",
                TaskId = taskId,
                Actions = new
                {
                    RemoveDevice = request.BringDeviceToRepairPlace,
                    SetupReplacement = request.SetupReplacementDevice
                }
            });
        }

        public async Task<Result> CreateTaskFromErrorsAsync(CreateTaskFromErrorsRequest dto)
        {
            var request = await _unitOfWork.RequestRepository.GetByIdAsync(dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request is not exist"));
            }

            var userExists = await _unitOfWork.UserRepository.IdExistsAsync(dto.AssigneeId);
            if (!userExists)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User not found"));
            }
            var missingErrors = await _unitOfWork.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(dto.ErrorIds);
            if (missingErrors.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));
            }
            var task = await _unitOfWork.TaskRepository.CreateTaskFromErrorsAsync(dto);
            return Result.SuccessWithObject(new { Message = "Task assigned successfully!" });
        }
    }
}
