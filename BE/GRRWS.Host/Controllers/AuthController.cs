using Google.Apis.Auth;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Common;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.User.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GRRWS.Infrastructure.DTOs.User.Login;
using GRRWS.Infrastructure.DTOs.User.Register;

namespace GRRWS.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("sign-in")]
        public async Task<IResult> SignInForUser(LoginRequest loginRequest)
        {
            Result result = await _authService.SignIn(loginRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Login successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("sign-up")]
        public async Task<IResult> RegisterForUser([FromBody] RegisterRequest registerRequest)
        {
            Result result = await _authService.SignUp(registerRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Register successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(Guid userId)
        {
            Result result = await _authService.ConfirmEmail(userId);

            if (result.IsSuccess)
            {
                return Redirect($"{CommonObject.Domain}/login");
            }

            return (IActionResult)ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("reset-password")]
        public async Task<IResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            Result result = await _authService.ResetPassword(resetPasswordRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Password reset successfully")
                : ResultExtensions.ToProblemDetails(result);
        }

        [HttpPost("forget-password")]
        public async Task<IResult> ForgetPassword([FromBody] ForgetPasswordRequest forgetPasswordRequest)
        {
            Result result = await _authService.ForgetPassword(forgetPasswordRequest);
            return result.IsSuccess
                ? ResultExtensions.ToSuccessDetails(result, "Reset password email sent successfully")
                : ResultExtensions.ToProblemDetails(result);
        }


    }
}
