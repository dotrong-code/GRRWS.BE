namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class TaskErrorMessage
    {
        public static Error TaskNotExist() => Error.Validation("Task.NotExist", "Task does not exist.");
        public static Error TaskUpdateFailed(string message) => Error.Validation("Task.UpdateFail", $"{message}");
        public static Error UserNotExist() => Error.Validation("Task.UserNotExist", "User does not exist.");
        public static Error InvalidStatusTransition() => Error.Validation("Task.InvalidStatusTransition", "Cannot start task in current status.");
        public static Error InvalidTaskId() => Error.Validation("Task.InvalidTaskId", "Task ID is required.");
        public static Error InvalidDeviceCondition() => Error.Validation("Task.InvalidDeviceCondition", "Invalid device condition.");
        public static Error InvalidDeviceReturnTime() => Error.Validation("Task.InvalidDeviceReturnTime", "Device return time must be in the past or present.");
        public static Error ReportAlreadyCreated() => Error.Validation("Task.ReportAlreadyCreated", "Report already created for this task.");
        public static Error ReportNotExist() => Error.Validation("Task.ReportNotExist", "ReportNotExist.");
    }
}
