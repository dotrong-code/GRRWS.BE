namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetTaskDetailBase
    {
        public Guid TaskId { get; set; }
        public Guid DeviceId { get; set; } // Device ID
        public string TaskType { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; } // e.g., Low, Medium, High
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; } // Deadline
        public DateTime? EndTime { get; set; }
        public string? AssigneeName { get; set; }
        public bool IsInstall { get; set; } // True if this is an install task, false if it's an uninstall task
        public bool IsSigned { get; set; } // True if this is an install task, false if it's an uninstall task
        public List<RequestMachineDetail> RequestMachines { get; set; } = new List<RequestMachineDetail>();
    }


    public class RequestMachineDetail
    {
        public Guid RequestMachineId { get; set; } // ID of the request machine
        public string? RequestMachineDescription { get; set; } // Description of the request machine
        public bool AssigneeConfirm { get; set; } // True if the mechanic has confirmed the task, false otherwise
        public bool StockKeeperConfirm { get; set; } // True if the stock keeper has confirmed the task, false otherwise
        public string RequestMachineReplacementType { get; set; } // Type of the request machine replacement (e.g., Replacement, Repair)

    }
}
