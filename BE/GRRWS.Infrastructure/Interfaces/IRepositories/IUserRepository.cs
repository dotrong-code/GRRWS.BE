using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.User.Get;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<bool> IdExistsAsync(Guid id);
        Task<bool> UserNameExistsAsync(string userName);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<(List<GetUserResponse> Users, int TotalCount)> GetAllUsersAsync(
        string? fullName = null,
        string? email = null,
        string? phoneNumber = null,
        DateTime? dateOfBirth = null,
        int? role = null,
        int pageNumber = 1,
        int pageSize = 10);
        Task<User> GetUserByIdAsync(Guid id);
        // Task<List<Role>> GetAllRolesAsync();
        Task<int> DeleteUserAsync(Guid id);
        Task<List<User>> GetUsers();
        Task<List<User>> GetUsersByRole(int role);
        Task<List<User>> GetUsersByIdsBySearchNameAsync(List<Guid> ids, string? searchName = null); // New method
    }
}
