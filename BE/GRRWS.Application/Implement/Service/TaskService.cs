using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Paging;
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
                var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
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
                var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
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
    }
}
