using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IHttpContextAccessor _contextAccessor;

        public TaskController(ITaskService taskService, IHttpContextAccessor contextAccessor)
        {
            _taskService = taskService;
            _contextAccessor = contextAccessor;
        }
        [Authorize]
        [HttpPost("uninstall")]
        public async Task<IResult> CreateUninstallTask([FromBody] CreateUninstallTaskRequest request)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.CreateUninstallTask(request, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Uninstall task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPost("install")]
        public async Task<IResult> CreateInstallTask([FromBody] CreateInstallTaskRequest request)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.CreateInstallTask(request, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Install task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPost("warranty-task/submit")]
        public async Task<IResult> CreateWarrantyTask([FromBody] CreateWarrantyTaskRequest request)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.CreateWarrantyTask(request, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }


        [HttpGet("uninstall-task/{taskId}")]
        public async Task<IResult> GetUninstallTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetDetailUninstallTaskForMechanicByIdAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Uninstall task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("install-task/{taskId}")]
        public async Task<IResult> GetInstallTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetDetailInstallTaskForMechanicByIdAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Install task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpPost("repair-task")]
        public async Task<IResult> CreateRepairTask([FromBody] CreateRepairTaskRequest request)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.CreateRepairTask(request, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("warranty-task/submit/fill-infor")]
        public async Task<IResult> FillInWarrantyTask([FromBody] FillInWarrantyTask request)
        {
            var result = await _taskService.FillInWarrantyTask(request);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("warranty-task-submit/{taskId}")]
        public async Task<IResult> GetWarrantySubmitTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetGetDetailWarrantyTaskForMechanicByIdAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("warranty-task-return/{taskId}")]
        public async Task<IResult> GetWarrantyReturnTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetGetDetailWarrantyReturnTaskForMechanicByIdAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("repair-task/{taskId}")]
        public async Task<IResult> GetRepairTaskDetails(Guid taskId)
        {
            var result = await _taskService.GetDetailtRepairTaskForMechanicByIdAsync(taskId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Warranty task details retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut("status/{taskId}")]
        public async Task<IResult> UpdateTaskStatus(Guid taskId)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.UpdateTaskStatusAsync(taskId, c.UserId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Task status updated successfully")
                : ResultExtensions.ToProblemDetails(result);
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
        [Authorize]
        [HttpGet("mechanic")]
        public async Task<IResult> GetTasksForMechanic([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _taskService.GetTasksByMechanicIdAsync(c.UserId, pageNumber, pageSize);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Tasks retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("mechanicshift/suggest")]
        public async Task<IResult> GetTasksForMechanic()
        {
            var result = await _taskService.GetMechanicRecommendationAsync();
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Get suggest Mechanic successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("details/{taskId}")]
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


    }
}
