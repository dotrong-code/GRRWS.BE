using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        
        [HttpGet("mechanic/{mechanicId}")]
        public async Task<IResult> GetTasksByMechanicId(Guid mechanicId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _taskService.GetTasksByMechanicIdAsync(mechanicId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Tasks retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpPost("start")]
        public async Task<IResult> StartTask([FromBody] StartTaskRequest request)
        {
            var result = await _taskService.StartTaskAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task started successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpGet("{taskId}")]
        public async Task<IResult> GetTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetTaskDetailsAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        
        [HttpPost("report")]
        public async Task<IResult> CreateTaskReport([FromBody] CreateTaskReportRequest request)
        {
            var result = await _taskService.CreateTaskReportAsync(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task report created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
