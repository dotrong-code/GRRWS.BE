using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.User.Get;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GRRWSContext context) : base(context) { }
        #region bool
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            // Check if any user exists with the specified username
            return await _context.Users.AnyAsync(x => x.FullName == userName);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            // Check if any user exists with the specified email
            var result = await _context.Users.AnyAsync(x => x.Email == email);
            return result;
        }
        #endregion
        // Get user by email and password
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.PasswordHash.Equals(password));
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Where(u => u.Id == id && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }
        public async Task<(List<GetUserResponse> Users, int TotalCount)> GetAllUsersAsync(
        string? fullName = null,
        string? email = null,
        string? phoneNumber = null,
        DateTime? dateOfBirth = null,
        int? role = null,
        int pageNumber = 1,
        int pageSize = 10)
        {
            var query = _context.Users.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(fullName))
                query = query.Where(u => u.FullName.Contains(fullName));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(u => u.Email.Contains(email));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                query = query.Where(u => u.PhoneNumber.Contains(phoneNumber));

            if (dateOfBirth.HasValue)
                query = query.Where(u => u.DateOfBirth == dateOfBirth.Value.Date);

            //if (role.HasValue)
            //    query = query.Where(u => u.Role == role.Value);

            query = query.Where(u => !u.IsDeleted);

            // Total count before pagination
            int totalCount = await query.CountAsync();

            // Apply paging
            // Apply paging
            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new GetUserResponse
                {
                    Id = u.Id,
                    FullName = u.FullName ?? "",
                    Email = u.Email ?? "",
                    PhoneNumber = u.PhoneNumber ?? "",
                    ProfilePictureUrl = u.ProfilePictureUrl ?? "",
                    DateOfBirth = u.DateOfBirth ?? DateTime.MinValue,
                    CreatedDate = u.CreatedDate,
                    //Role = u.Role
                })
                .ToListAsync();

            return (users, totalCount);
        }
        //public async Task<List<Role>> GetAllRolesAsync()
        //{
        //    return await _context.Roles.ToListAsync();
        //}
        public async Task<int> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return 0;

            user.IsDeleted = true;
            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersByRole(int role)
        {
            return await _context.Users
                //.Where(u => u.Role == role && !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> IdExistsAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().AnyAsync(d => d.Id == id && !d.IsDeleted);
        }
    }
}
