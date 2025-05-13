using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.User.Get;
using GRRWS.Infrastructure.DTOs.User.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GRRWS.Application.Implement.Service
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IValidator<UpdateUserRequest> _updateUserValidator;
        private readonly IFirebaseService _firebaseService;
        public UserService(UnitOfWork unitOfWork, IValidator<UpdateUserRequest> updateUserValidator, IFirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _updateUserValidator = updateUserValidator;
            _firebaseService = firebaseService;
        }

        public async Task<Result> DeleteUserAsync(Guid id)
        {
            var result = await _unitOfWork.UserRepository.DeleteUserAsync(id);
            if (result == 0)
            {
                return Result.Failure(UserErrorMessage.UserDeleteFailed());
            }
            return Result.SuccessWithObject(result);
        }

        //public async Task<Result> GetAllRolesAsync()
        //{
        //    var result = await _unitOfWork.UserRepository.GetAllRolesAsync();

        //    return Result.SuccessWithObject(result);
        //}

        public async Task<Result> GetAllUsersAsync(
        string? fullName = null,
        string? email = null,
        string? phoneNumber = null,
        DateTime? dateOfBirth = null,
        int? role = null,
        int pageNumber = 1,
        int pageSize = 10)
        {
            var (users, totalCount) = await _unitOfWork.UserRepository.GetAllUsersAsync(
                fullName, email, phoneNumber, dateOfBirth, role, pageNumber, pageSize);
            var userResponses = new List<GetUserResponse>();
            /*============================================lay anh==========================================================*/
            foreach (var user in users)
            {
                var getImgRequest = new GetImageRequest(user.ProfilePictureUrl ?? string.Empty);
                var userImg = await _unitOfWork.FirebaseRepository.GetImageAsync(getImgRequest);

                var userResponse = new GetUserResponse
                {
                    Id = user.Id,
                    FullName = user.FullName ?? "",
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    DateOfBirth = user.DateOfBirth,
                    Role = user.Role,
                    ProfilePictureUrl = userImg.ImageUrl ?? string.Empty
                };

                userResponses.Add(userResponse);
            }
            /*============================================lay anh==========================================================*/
            var response = new PagedResponse<GetUserResponse>
            {
                Data = userResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByAsync("Email", email);
            if (user == null)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }
            /*============================================lay anh==========================================================*/
            var getimg = new GetImageRequest(user.ProfilePictureUrl ?? string.Empty);
            var UserImg = await _unitOfWork.FirebaseRepository.GetImageAsync(getimg);
            /*============================================lay anh==========================================================*/

            var userInfor = new GetUserResponse
            {
                Id = user.Id,
                FullName = user.FullName ?? "",
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                DateOfBirth = user.DateOfBirth ?? DateTime.MinValue,
                ProfilePictureUrl = UserImg.ImageUrl ?? string.Empty,
                Role = user.Role

            };

            return Result.SuccessWithObject(userInfor);
        }

        public async Task<Result> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }
            /*============================================lay anh==========================================================*/
            var getimg = new GetImageRequest(user.ProfilePictureUrl ?? string.Empty);
            var UserImg = await _unitOfWork.FirebaseRepository.GetImageAsync(getimg);
            /*============================================lay anh==========================================================*/

            var response = new GetUserResponse
            {
                Id = user.Id,
                FullName = user.FullName ?? "",
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",

                PhoneNumber = user.PhoneNumber ?? "",
                ProfilePictureUrl = UserImg.ImageUrl ?? string.Empty,
                CreatedDate = user.CreatedDate,
                DateOfBirth = user.DateOfBirth ?? DateTime.MinValue,
                Role = user.Role
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdateUserAsync(UpdateUserRequest request)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }

            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.DateOfBirth = request.DateOfBirth;
            user.Role = request.Role;
            user.ModifiedDate = DateTime.UtcNow;

            var updateResult = await _unitOfWork.UserRepository.UpdateAsync(user);
            if (updateResult == 0)
            {
                return Result.Failure(UserErrorMessage.UserUpdateFailed());
            }
            
            return Result.SuccessWithObject(user);
        }
    }
}
