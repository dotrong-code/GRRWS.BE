using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;

namespace GRRWS.Application.Implement.Service
{
    public class TaskGroupService : ITaskGroupService
    {
        private readonly UnitOfWork _unitOfWork;

        public TaskGroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateOrGetTaskGroupAsync(Guid? taskGroupId, TaskType groupType, string deviceName, Guid userId)
        {
            if (taskGroupId.HasValue)
            {
                return taskGroupId.Value;
            }

            // Create new task group
            var taskGroup = new TaskGroup
            {
                Id = Guid.NewGuid(),
                GroupType = groupType,
                GroupName = $"{groupType} Group - {deviceName} - {DateTime.UtcNow:yyyy-MM-dd HH:mm}",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _unitOfWork.TaskGroupRepository.CreateAsync(taskGroup);
            return taskGroup.Id;
        }

        public async Task<int> GetNextOrderIndexAsync(Guid taskGroupId, TaskType taskType)
        {
            var taskGroup = await _unitOfWork.TaskGroupRepository.GetByIdAsync(taskGroupId);
            if (taskGroup == null)
                throw new Exception("Task group not found.");

            return taskGroup.GroupType switch
            {
                TaskType.Warranty => taskType switch
                {
                    TaskType.Uninstallation => 1,
                    TaskType.WarrantySubmission => 2,
                    TaskType.Installation => 3,
                    TaskType.WarrantyReturn => 4,
                    _ => 1
                },
                TaskType.Repair => taskType switch
                {
                    TaskType.Uninstallation => 1,
                    TaskType.Repair => 2,
                    TaskType.Installation => 3,
                    _ => 1
                },
                TaskType.Replacement => taskType switch
                {
                    TaskType.Uninstallation => 1,
                    TaskType.Installation => 2,
                    _ => 1
                },
                _ => 1
            };
        }

        public async Task UpdateExistingTasksOrderAsync(Guid taskGroupId, int fromOrderIndex, Guid userId)
        {
            var existingTasks = await _unitOfWork.TaskRepository.GetTasksByGroupIdAsync(taskGroupId);

            foreach (var task in existingTasks.Where(t => t.OrderIndex >= fromOrderIndex))
            {
                task.OrderIndex += 1;
                task.ModifiedDate = DateTime.UtcNow;
                task.ModifiedBy = userId;
            }

            if (existingTasks.Any())
            {
                await _unitOfWork.TaskRepository.UpdateRangeAsync(existingTasks);
            }
        }
    }
}
