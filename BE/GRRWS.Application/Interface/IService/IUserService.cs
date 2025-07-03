using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.User.Create;
using GRRWS.Infrastructure.DTOs.User.Update;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Interface.IService
{
    public interface IUserService
    {
        Task<Result> GetUserByEmail(string email);
        Task<Result> GetAllUsersAsync(
        string? fullName = null,
        string? email = null,
        string? phoneNumber = null,
        DateTime? dateOfBirth = null,
        int? role = null,
        int pageNumber = 1,
        int pageSize = 10);
        Task<Result> GetUserByIdAsync(Guid id);
        Task<Result> UpdateUserAsync(UpdateUserRequest updateUserRequest);
        Task<Result> DeleteUserAsync(Guid id);
        Task<Result> GetUsersByRole(int role);
        Task<Result> AddUserAsync(CreateUserRequest createUserRequest);
        //Task<Result> GetAllRolesAsync();
        Task<Result> ImportUsersAsync(IFormFile file);
    }
}
