using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
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
                Priority = request.Priority,
                Status = request.Status,
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
            task.Priority = request.Priority;
            task.Status = request.Status;
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

            if (task.Status != "Pending")
                return Result.Failure(TaskErrorMessage.InvalidStatusTransition());

            task.Status = "InProgress";
            task.StartTime = DateTime.UtcNow;

            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(task);
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

            if (task.Status != "InProgress")
                return Result.Failure(TaskErrorMessage.InvalidStatusTransition());

            if (task.DeviceReturnTime.HasValue)
                return Result.Failure(TaskErrorMessage.ReportAlreadyCreated());

            task.Status = "Completed";
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
                    Priority = t.Priority,
                    Status = t.Status,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee?.UserName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    CreatedDate = t.CreatedDate,
                    CreatedBy = (Guid)t.CreatedBy,
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
                Priority = task.Priority,
                Status = task.Status,
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeId = task.AssigneeId,
                AssigneeName = task.Assignee?.UserName,
                DeviceReturnTime = task.DeviceReturnTime,
                DeviceCondition = task.DeviceCondition,
                ReportNotes = task.ReportNotes,
                CreatedDate = task.CreatedDate,
                CreatedBy = (Guid)task.CreatedBy,
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

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskName = dto.TaskName,
                TaskDescription = dto.TaskDescription,
                TaskType = dto.TaskType,
                Priority = dto.Priority,
                Status = dto.Status ?? "Pending",
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
            await _unitOfWork.TaskRepository.CreateTaskAsync(task, dto.ErrorIds, dto.SparepartIds);

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
            task.Priority = dto.Priority;
            task.Status = dto.Status;
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


    }
}
