namespace GRRWS.Infrastructure.DTOs.Task.ActionTask
{
    public class CreateInstallTaskRequest
    {
        public Guid RequestId { get; set; } // The ID of the request for which the uninstallation task is being created
        public Guid? AssigneeId { get; set; } // The ID of the user to whom the task is assigned
        public DateTime? StartDate { get; set; } // The start date of the task
        public Guid? TaskGroupId { get; set; } // Nếu null thì task này không thuộc nhóm nào tự tạo nhóm Replacement, nếu có thì task này thuộc nhóm đó
        public Guid? NewDeviceId { get; set; } // The ID of the new device to be installed
    }
}
