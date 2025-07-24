using GRRWS.Infrastructure.DTOs.Task.Get.SubObject;

namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetDetailtRepairTaskForMechanic : GetTaskDetailBase
    {
        public List<ErrorDetailOfTask> ErrorDetails { get; set; } = new List<ErrorDetailOfTask>();
        public List<TaskConfirmationResponseDTO>? TaskConfirmations { get; set; }
    }
}
