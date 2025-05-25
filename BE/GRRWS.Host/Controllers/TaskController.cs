using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Task;
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

        [HttpGet("ByReport/{reportId}")]
        public async Task<IResult> GetTasksByReportId(Guid reportId)
        {
            var result = await _taskService.GetTasksByReportIdAsync(reportId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Tasks retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
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


        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var result = await _taskService.GetAllAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved tasks")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var result = await _taskService.GetByIdAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved task")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateTaskDTO dto)
        {
            var result = await _taskService.CreateAsync(dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateTaskDTO dto)
        {
            var result = await _taskService.UpdateAsync(id, dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var result = await _taskService.DeleteAsync(id);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPut("{taskId}/assign")]
        public async Task<IResult> AssignTask(Guid taskId, [FromBody] AssignTaskDTO dto)
        {
            var result = await _taskService.AssignTaskAsync(taskId, dto);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task assigned successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost("create-task")]
        public async Task<IResult> CreateTaskWeb(CreateTaskWeb createTaskWeb)
        {
            var result = await _taskService.CreateTaskWebAsync(createTaskWeb);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task assigned successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

    }
}
