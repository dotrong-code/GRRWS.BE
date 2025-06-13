namespace GRRWS.Infrastructure.DTOs.Task
{
    public class GetTaskResponse
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskType { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public DateTime? DeviceReturnTime { get; set; }
        public string DeviceCondition { get; set; }
        public string ReportNotes { get; set; }
        public List<RepairSparepartDto> RepairSpareparts { get; set; }
        public List<ErrorDetailDto> ErrorDetails { get; set; }
    }
}
