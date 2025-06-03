using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Google.Apis.Auth;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Common;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.EmailTemplate;
using GRRWS.Infrastructure.DTOs.User.Login;
using GRRWS.Infrastructure.DTOs.User.Password;
using GRRWS.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using GRRWS.Infrastructure.DTOs.User.Register;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;

namespace GRRWS.Application.Implement.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        private readonly IEmailTemplateService _emailTemplateService;
        public AuthService
            (
            IUnitOfWork unitOfWork,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<LoginRequest> loginRequestValidator,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IConfiguration configuration,

            IEmailTemplateService emailTemplateService
            )
        {
            _unitOfWork = unitOfWork;
            _registerRequestValidator = registerRequestValidator;
            _loginRequestValidator = loginRequestValidator;

            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configuration = configuration;

            _emailTemplateService = emailTemplateService;

        }

        public async Task<Result> SignIn(LoginRequest loginRequest)
        {
            var validate = await _loginRequestValidator.ValidateAsync(loginRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState)
                    .ToList();

                // Handle errors as needed, e.g., return them in a Result object
                return Result.Failures(errors);
            }

            var userLogin = await _unitOfWork.UserRepository.GetUserByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            if (userLogin == null)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }
            //if (!userLogin.IsEmailConfirmed)
            //{
            //    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Confirm", "Your email is not activated yet!!, please confirm your mail"));
            //}
            var role = userLogin.Role switch
            {
                1 => "HOD",
                2 => "HOT",
                3 => "Staff",
                4 => "SK",
                5 => "Admin",
                _ => throw new InvalidOperationException("Unknown role.")
            };
            var c = new CurrentUserObject
            {
                UserId = userLogin.Id,
                Fullname = userLogin.FullName,
                Email = userLogin.Email,
                PhoneNumber = userLogin.PhoneNumber,
                RoleName = role,
            };
            var token = await _tokenService.GenerateTokenAsync(c);
            var accessToken = await _tokenService.GenerateAccessTokenAsync(token);
            //var accessToken =  GenerateJwtToken(c.Email, c.RoleId, 180);
            var loginResponse = new LoginResponse
            {
                ReNewToken = "Test",
                AccessToken = accessToken,
            };

            return Result.SuccessWithObject(loginResponse);
        }

        public async Task<Result> SignUp(RegisterRequest registerRequest)
        {
            var validate = await _registerRequestValidator.ValidateAsync(registerRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Infrastructure.DTOs.Common.Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }
            string mainImageUrl = null;
            if (registerRequest.ProfilePictureUrl != null)
            {
                var imageRequest = new AddImageRequest(registerRequest.ProfilePictureUrl, "Users");
                var uploadImageResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest);
                if (!uploadImageResult.Success)
                {
                    return Result.Failure(uploadImageResult.Error);
                }
                mainImageUrl = uploadImageResult.FilePath;
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PasswordHash = registerRequest.Password,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber,
                DateOfBirth = registerRequest.DateOfBirth,
                ProfilePictureUrl = mainImageUrl,
                Role = registerRequest.Role

            };

            var createUsre = await _unitOfWork.UserRepository.CreateAsync(newUser);
            if (createUsre == 0)
            {
                return Result.Failure(UserErrorMessage.UserNoCreated());
            }


            //var activationLink = $"{CommonObject.Domain}/api/Auth/confirm?userId={newUser.Id}";

            //// Send activation email
            //var emailBodyResult = await _emailTemplateService.GenerateEmailWithActivationLink("VerifyEmail", activationLink);
            //if (emailBodyResult.IsFailure)
            //{
            //    return Result.Failure(Error.Failure("EmailGenerationFailed", "Failed to generate the email body."));
            //}
            //var emailBody = emailBodyResult.Object as string;

            //// Create and send the email
            //var mailObject = new MailObject
            //{
            //    ToMailIds = new List<string> { newUser.Email },
            //    Subject = "Confirm Your Email Address",
            //    Body = emailBody,
            //    IsBodyHtml = true
            //};

            //var sendResult = await _emailTemplateService.SendMail(mailObject);
            //if (!sendResult.IsSuccess)
            //{
            //    return Result.Failure(Error.None);
            //}
            return Result.SuccessWithObject(new { Message = "Create successfully!!!" });

        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<Result> ConfirmEmail(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("UserNotFound", "User not found."));
            }

            user.IsEmailConfirmed = true;
            await _unitOfWork.UserRepository.UpdateAsync(user);

            return Result.Success();
        }

        public string GenerateJwtToken(string email, int Role, double expirationMinutes)//them tham so role de phan quyen
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // add role to author
            var role = Role switch
            {
                1 => "HOD",
                2 => "HOT",
                3 => "Staff",
                4 => "SK",
                5 => "Admin",
            };
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)// claim role
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> FindOrCreateUser(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _unitOfWork.UserRepository.GetByAsync("Email", payload.Email);
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    FullName = payload.Name,
                };
                await _unitOfWork.UserRepository.CreateAsync(user);
            }
            return user;
        }


        public async Task<Result> ForgetPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            // Kiểm tra xem email có tồn tại không
            var user = await _unitOfWork.UserRepository.GetByAsync("Email", forgetPasswordRequest.Email);
            if (user == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("UserNotFound", "Email does not exist."));
            }

            // Tạo mã token hoặc liên kết đặt lại mật khẩu (tạm thời dùng Guid làm token)
            var resetToken = Guid.NewGuid().ToString();
            var resetLink = $"{CommonObject.Domain}/forgetpassword?token={resetToken}&email={user.Email}";

            user.ResetPasswordToken = resetToken;
            await _unitOfWork.UserRepository.UpdateAsync(user);

            // Tạo email với template "ChangePassword"
            var emailBodyResult = await _emailTemplateService.GenerateEmailWithActivationLink("ChangePassword", resetLink);
            if (emailBodyResult.IsFailure)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("EmailGenerationFailed", "Failed to generate the email body."));
            }
            var emailBody = emailBodyResult.Object as string;

            // Tạo đối tượng email
            var mailObject = new MailObject
            {
                ToMailIds = new List<string> { user.Email },
                Subject = "Reset Your Password",
                Body = emailBody,
                IsBodyHtml = true
            };

            // Gửi email
            var sendResult = await _emailTemplateService.SendMail(mailObject);
            if (!sendResult.IsSuccess)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("EmailSendFailed", "Failed to send reset password email."));
            }

            return Result.SuccessWithObject(new { Message = "Reset password link has been sent to your email!" });
        }
        public async Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var user = await _unitOfWork.UserRepository.GetByAsync("Email", resetPasswordRequest.Email);
            if (user == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("UserNotFound", "User not found."));
            }


            if (user.ResetPasswordToken != resetPasswordRequest.Token)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Token wrong", "Token not found."));
            }

            user.PasswordHash = resetPasswordRequest.NewPassword;
            user.ResetPasswordToken = null;
            await _unitOfWork.UserRepository.UpdateAsync(user);

            return Result.SuccessWithObject(new { Message = "Password has been reset successfully!" });
        }

    }
}