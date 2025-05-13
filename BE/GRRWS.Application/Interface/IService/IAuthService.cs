using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using GRRWS.Application.Common.Result;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.User.Login;
using GRRWS.Infrastructure.DTOs.User.Password;
using GRRWS.Infrastructure.DTOs.User.Register;

namespace GRRWS.Application.Interface.IService
{
    public interface IAuthService
    {
        Task<Result> SignIn(LoginRequest loginRequest);
        Task<Result> SignUp(RegisterRequest registerRequest);
        Task<Result> ConfirmEmail(Guid userId);
        public string GenerateJwtToken(string email, int Role, double expirationMinutes);
        public Task<User> FindOrCreateUser(GoogleJsonWebSignature.Payload payload);
        Task<Result> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest);
        Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest);
    }
}
