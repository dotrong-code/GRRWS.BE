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

        public async Task<Guid> CreateOrGetTaskGroupAsync(Guid? taskGroupId, TaskType groupType, string deviceName, Guid reportId, Guid userId)
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
                GroupName = $"{groupType} Group - {deviceName} - {TimeHelper.GetHoChiMinhTime():yyyy-MM-dd HH:mm}",

                CreatedDate = TimeHelper.GetHoChiMinhTime(),

                ReportId = reportId,
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

            // Get the current highest order index in the group
            var existingTasks = await _unitOfWork.TaskRepository.GetTasksByGroupIdAsync(taskGroupId);
            var maxOrderIndex = existingTasks.Any() ? existingTasks.Max(t => t.OrderIndex) : 0;

            return taskGroup.GroupType switch
            {
                TaskType.Warranty => taskType switch
                {
                    TaskType.Uninstallation => 1, // Always first
                    TaskType.WarrantySubmission => 2,
                    TaskType.Installation => 3,
                    TaskType.WarrantyReturn => 4,
                    _ => (maxOrderIndex ?? 0) + 1
                },
                TaskType.Repair => taskType switch
                {
                    TaskType.Uninstallation => 1, // Always first
                    TaskType.Repair => 2,
                    TaskType.Installation => 3,
                    _ => (maxOrderIndex ?? 0) + 1
                },
                TaskType.Replacement => taskType switch
                {
                    TaskType.Uninstallation => (maxOrderIndex ?? 0) + 1, // Next available for replacement
                    TaskType.Installation => (maxOrderIndex ?? 0) + 1,   // Next available for replacement
                    _ => (maxOrderIndex ?? 0) + 1
                },
                _ => (maxOrderIndex ?? 0) + 1
            };
        }

        public async Task UpdateExistingTasksOrderAsync(Guid taskGroupId, int fromOrderIndex, Guid userId)
        {
            var existingTasks = await _unitOfWork.TaskRepository.GetTasksByGroupIdAsync(taskGroupId);

            foreach (var task in existingTasks.Where(t => t.OrderIndex >= fromOrderIndex))
            {
                task.OrderIndex += 1;
                task.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                task.ModifiedBy = userId;
            }

            if (existingTasks.Any())
            {
                await _unitOfWork.TaskRepository.UpdateRangeAsync(existingTasks);
            }
        }
    }
}
