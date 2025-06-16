using GRRWS.Domain.Enum;

namespace GRRWS.Application.Interface.IService
{
    public interface ITaskGroupService
    {
        Task<Guid> CreateOrGetTaskGroupAsync(Guid? taskGroupId, TaskType groupType, string deviceName, Guid userId);
        Task<int> GetNextOrderIndexAsync(Guid taskGroupId, TaskType taskType);
        Task UpdateExistingTasksOrderAsync(Guid taskGroupId, int fromOrderIndex, Guid userId);
    }
}
