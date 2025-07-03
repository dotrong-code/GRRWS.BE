using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.User.Create;
using GRRWS.Infrastructure.DTOs.User.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        public UserController(IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _contextAccessor = contextAccessor;
            _userService = userService;
        }
        [Authorize]
        [HttpGet("user-infor")]
        public async Task<IResult> GetUserInfor()
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _userService.GetUserByEmail(c.Email);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Register successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [Authorize]
        [HttpGet("users/search")]
        public async Task<IResult> GetAllUsers(
        [FromQuery] string? fullName = null,
        [FromQuery] string? email = null,
        [FromQuery] string? phoneNumber = null,
        [FromQuery] DateTime? dateOfBirth = null,
        [FromQuery] int? role = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _userService.GetAllUsersAsync(
                fullName, email, phoneNumber, dateOfBirth, role, pageNumber, pageSize);

            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "All users retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet]
        public async Task<IResult> GetUserById([FromQuery] Guid requestId)
        {
            var result = await _userService.GetUserByIdAsync(requestId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "User retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPut]
        public async Task<IResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            var result = await _userService.UpdateUserAsync(updateUserRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "User updated successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpDelete]
        public async Task<IResult> DeleteUser([FromQuery] Guid requestId)
        {
            var result = await _userService.DeleteUserAsync(requestId);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "User deleted successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpGet("role")]
        public async Task<IResult> GetUsersByRole([FromQuery] int role)
        {
            var result = await _userService.GetUsersByRole(role);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Users by role retrieved successfully")
                : ResultExtensions.ToProblemDetails(result);
        }
        [HttpPost]
        public async Task<IResult> AddUser([FromBody] CreateUserRequest createUserRequest)
        {
            var result = await _userService.AddUserAsync(createUserRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "User created successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        //[HttpGet("roleList")]
        //public async Task<IResult> GetAllRoles()
        //{
        //    var result = await _userService.GetAllRolesAsync();
        //    return result.IsSuccess
        //       ? ResultExtensions.ToSuccessDetails(result, "All roles retrieved successfully")
        //       : ResultExtensions.ToProblemDetails(result);
        //}
        [HttpPost("import")]
        public async Task<IResult> ImportUser(IFormFile file)
        {
            var result = await _userService.ImportUsersAsync(file);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Users imported successfully!")
                : ResultExtensions.ToProblemDetails(result);
        }
    }
}
