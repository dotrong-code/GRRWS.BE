using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Task;

namespace GRRWS.Application.Interface.IService
{
    public interface ITaskService
    {
        Task<Result> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize);
        Task<Result> StartTaskAsync(StartTaskRequest request);
        Task<Result> GetTaskDetailsAsync(Guid taskId);
        Task<Result> CreateTaskReportAsync(CreateTaskReportRequest request);
    }
}
