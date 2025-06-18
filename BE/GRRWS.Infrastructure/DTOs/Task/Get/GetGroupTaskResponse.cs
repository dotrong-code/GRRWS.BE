namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetGroupTaskResponse
    {
        public Guid? TaskGroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? RequestId { get; set; }
        public List<TaskInGroupResponse>? Tasks { get; set; } = new List<TaskInGroupResponse>();
    }

    public class TaskInGroupResponse
    {
        public Guid? TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskType { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public int? OrderIndex { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? AssigneeName { get; set; }
        public Guid? AssigneeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}