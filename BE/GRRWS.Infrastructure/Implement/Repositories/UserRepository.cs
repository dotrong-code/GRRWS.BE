using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.User.Get;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private static readonly string[] CompletionTimeSeparators = { " ", "giờ", "phút" };
        public UserRepository(GRRWSContext context) : base(context) { }
        #region bool
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            // Check if any user exists with the specified username
            return await _context.Users.AnyAsync(x => x.UserName == userName);
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

            if (role.HasValue)
                query = query.Where(u => u.Role == role.Value);

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
                    UserName = u.UserName ?? "", // Chỉnh sửa tên thuộc tính thành chữ hoa
                    Email = u.Email ?? "",
                    PhoneNumber = u.PhoneNumber ?? "",
                    ProfilePictureUrl = u.ProfilePictureUrl ?? "",
                    DateOfBirth = u.DateOfBirth ?? DateTime.MinValue,
                    CreatedDate = u.CreatedDate,
                    Role = u.Role
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
                .Where(u => u.Role == role && !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> IdExistsAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().AnyAsync(d => d.Id == id && !d.IsDeleted);
        }
        public async Task<List<User>> GetUsersByIdsBySearchNameAsync(List<Guid> ids, string? searchName = null)
        {
            var query = _context.Users
                .Where(u => ids.Contains(u.Id) && !u.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(u => u.FullName.Contains(searchName));
            }

            return await query.ToListAsync();
        }
        //public async Task<List<GetMechanicRecommendation>> GetRecommendedMechanicsAsync(DateTime currentTime, Guid shiftId)
        //{
        //    var availableMechanics = await _context.Users
        //                    .Where(u => u.Role == 3)
        //                    .GroupJoin(_context.MechanicShifts
        //                        .Where(ms => ms.Date.Date == currentTime.Date && ms.ShiftId == shiftId),
        //                        u => u.Id,
        //                        ms => ms.MechanicId,
        //                        (u, ms) => new { User = u, MechanicShifts = ms })
        //                    .SelectMany(
        //                        x => x.MechanicShifts.DefaultIfEmpty(),
        //                        (x, ms) => new { x.User, MechanicShift = ms })
        //                    .Where(x => x.MechanicShift == null || x.MechanicShift.IsAvailable == true)
        //                    .GroupBy(x => x.User.Id)
        //                    .Select(g => g.First().User)
        //                    .ToListAsync();

        //    var recommendations = new List<GetMechanicRecommendation>();

        //    foreach (var user in availableMechanics)
        //    {
        //        var completedTasks = await _context.Tasks
        //            .Where(t => t.AssigneeId == user.Id &&
        //                        t.Status == GRRWS.Domain.Enum.Status.Completed &&
        //                        t.EndTime != null &&
        //                        t.StartTime != null)
        //            .ToListAsync();

        //        double avgCompletionTime = completedTasks.Any()
        //            ? completedTasks.Average(t => (t.EndTime.Value - t.StartTime.Value).TotalMinutes)
        //            : -1.00;

        //        var shift = await _context.Shifts.FindAsync(shiftId);

        //        recommendations.Add(new GetMechanicRecommendation
        //        {
        //            MechanicId = user.Id,
        //            FullName = user.FullName ?? "Unknown",
        //            AverageCompletionTime = Math.Round(avgCompletionTime, 2),
        //            ShiftName = shift?.ShiftName ?? "Unknown",
        //            Date = currentTime
        //        });
        //    }

        //    return recommendations.OrderBy(r => r.AverageCompletionTime < 0 ? 1 : 0)
        //        .ThenBy(r => r.AverageCompletionTime)
        //        .ToList();
        //}
        //public async Task<List<GetMechanicRecommendation>> GetRecommendedMechanicsAsync(DateTime currentTime, Guid shiftId, int pageIndex, int pageSize)
        //{
        //    var availableMechanics = await _context.Users
        //        .Where(u => u.Role == 3)
        //        .GroupJoin(_context.MechanicShifts
        //            .Where(ms => ms.Date.Date == currentTime.Date && ms.ShiftId == shiftId),
        //            u => u.Id,
        //            ms => ms.MechanicId,
        //            (u, ms) => new { User = u, MechanicShifts = ms })
        //        .SelectMany(
        //            x => x.MechanicShifts.DefaultIfEmpty(),
        //            (x, ms) => new { x.User, MechanicShift = ms })
        //        .GroupBy(x => x.User.Id)
        //        .Select(g => g.First().User)
        //        .ToListAsync();

        //    var recommendations = new List<GetMechanicRecommendation>();

        //    foreach (var user in availableMechanics)
        //    {
        //        var completedTasks = await _context.Tasks
        //            .Where(t => t.AssigneeId == user.Id &&
        //                        t.Status == GRRWS.Domain.Enum.Status.Completed &&
        //                        t.EndTime != null &&
        //                        t.StartTime != null)
        //            .ToListAsync();

        //        double avgCompletionTime = completedTasks.Any()
        //            ? completedTasks.Average(t => (t.EndTime.Value - t.StartTime.Value).TotalMinutes)
        //            : -1.00;

        //        var shift = await _context.Shifts.FindAsync(shiftId);
        //        var mechanicShift = await _context.MechanicShifts
        //            .FirstOrDefaultAsync(ms => ms.MechanicId == user.Id && ms.ShiftId == shiftId && ms.Date.Date == currentTime.Date);

        //        DateTime expectedTime = currentTime; 
        //        if (mechanicShift != null && !mechanicShift.IsAvailable)
        //        {
        //            var activeTask = await _context.Tasks
        //                .FirstOrDefaultAsync(t => t.AssigneeId == user.Id &&
        //                                       t.MechanicShifts.Any(ms => ms.MechanicId == user.Id && ms.ShiftId == shiftId && !ms.IsAvailable) &&
        //                                       t.Status != GRRWS.Domain.Enum.Status.Completed);
        //            if (activeTask != null && activeTask.ExpectedTime.HasValue)
        //            {
        //                expectedTime = currentTime.Add(activeTask.ExpectedTime.Value - currentTime.Date); 
        //            }
        //        }

        //        recommendations.Add(new GetMechanicRecommendation
        //        {
        //            MechanicId = user.Id,
        //            FullName = user.FullName ?? "Unknown",
        //            AverageCompletionTime = Math.Round(avgCompletionTime, 2),
        //            ShiftName = shift?.ShiftName ?? "Unknown",
        //            ExpectedTime = expectedTime,
        //            Message = mechanicShift != null && !mechanicShift.IsAvailable
        //                ? "Chú ý: Thợ máy này đã có việc! Nếu gán cho Thợ máy này thì sẽ tự động gán cho ca làm sau!"
        //                : "Đề xuất"
        //        });
        //    }

        //    return recommendations.OrderBy(r => r.AverageCompletionTime < 0 ? 1 : 0)
        //        .ThenBy(r => r.AverageCompletionTime)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}
        public async Task<List<GetMechanicRecommendation>> GetRecommendedMechanicsAsync(DateTime currentTime, int pageIndex, int pageSize)
        {
            var availableMechanics = await _context.Users
                .Where(u => u.Role == 3)
                .ToListAsync();

            var recommendations = new List<GetMechanicRecommendation>();
            var shifts = await _context.Shifts.OrderBy(s => s.StartTime).ToListAsync();
            if (!shifts.Any()) return recommendations; // Trả về danh sách rỗng nếu không có ca

            foreach (var user in availableMechanics)
            {
                var completedTasks = await _context.Tasks
                    .Where(t => t.AssigneeId == user.Id &&
                                t.Status == GRRWS.Domain.Enum.Status.Completed &&
                                t.EndTime != null &&
                                t.StartTime != null)
                    .ToListAsync();

                double avgCompletionTimeMinutes = completedTasks.Any()
                    ? completedTasks.Average(t => (t.EndTime.Value - t.StartTime.Value).TotalMinutes)
                    : -1.00;

                string avgCompletionTimeString = FormatAverageCompletionTime(avgCompletionTimeMinutes);

                var currentTimeOfDay = currentTime.TimeOfDay;
                var shift = shifts.FirstOrDefault(s => s.StartTime <= currentTimeOfDay && s.EndTime >= currentTimeOfDay);
                var mechanicShift = await _context.MechanicShifts
                    .FirstOrDefaultAsync(ms => ms.MechanicId == user.Id && ms.StartTime <= currentTime && (ms.EndTime == null || ms.EndTime > currentTime));

                DateTime expectedTime = currentTime;
                Guid shiftId = shift?.Id ?? Guid.Empty;
                if (mechanicShift != null && !mechanicShift.IsAvailable)
                {
                    var activeTask = await _context.Tasks
                        .FirstOrDefaultAsync(t => t.AssigneeId == user.Id &&
                                               t.MechanicShifts.Any(ms => ms.MechanicId == user.Id &&
                                                                          ms.ShiftId == shiftId &&
                                                                          !ms.IsAvailable) &&
                                               t.Status != GRRWS.Domain.Enum.Status.Completed);
                    if (activeTask != null && activeTask.ExpectedTime.HasValue)
                    {
                        expectedTime = activeTask.ExpectedTime.Value;
                    }
                    else
                    {
                        var currentShiftIndex = shift != null ? shifts.IndexOf(shift) : -1;
                        int daysToCheck = 0;
                        while (daysToCheck <= 2)
                        {
                            var checkDate = currentTime.Date.AddDays(daysToCheck);
                            for (int i = currentShiftIndex + 1; i < shifts.Count || (daysToCheck > 0 && i == 0); i++)
                            {
                                if (i >= shifts.Count) { i = 0; daysToCheck++; checkDate = currentTime.Date.AddDays(daysToCheck); if (daysToCheck > 2) break; }
                                var shiftStart = checkDate.Add(shifts[i].StartTime);
                                if (shiftStart > currentTime)
                                {
                                    var nextMechanicShift = await _context.MechanicShifts
                                        .FirstOrDefaultAsync(ms => ms.MechanicId == user.Id && ms.ShiftId == shifts[i].Id && ms.StartTime == shiftStart);
                                    if (nextMechanicShift == null || nextMechanicShift.IsAvailable)
                                    {
                                        expectedTime = shiftStart;
                                        goto FoundShift;
                                    }
                                }
                            }
                            if (daysToCheck == 2 && currentShiftIndex == -1) break;
                        }
                    FoundShift:
                        if (expectedTime == currentTime)
                        {
                            var nextDay = currentTime.Date.AddDays(2);
                            expectedTime = nextDay.Add(shifts[0].StartTime);
                        }
                    }
                }
                else
                {
                    expectedTime = currentTime;

                    var currentShiftIndex = shift != null ? shifts.IndexOf(shift) : -1;
                    if (currentShiftIndex >= 0)
                    {
                        var shiftStart = currentTime.Date.Add(shifts[currentShiftIndex].StartTime);
                        if (shiftStart <= currentTime && shifts.Count > currentShiftIndex + 1)
                        {
                            var nextShiftStart = currentTime.Date.Add(shifts[currentShiftIndex + 1].StartTime);
                            if (nextShiftStart <= currentTime)
                            {
                                expectedTime = currentTime.Date.AddDays(1).Add(shifts[0].StartTime);
                            }
                        }
                    }
                    else
                    {
                        var nearestShiftIndex = 0;
                        int daysToCheck = 0;
                        while (daysToCheck <= 2)
                        {
                            var checkDate = currentTime.Date.AddDays(daysToCheck);
                            for (int i = nearestShiftIndex; i < shifts.Count; i++)
                            {
                                var shiftStart = checkDate.Add(shifts[i].StartTime);
                                if (shiftStart > currentTime)
                                {
                                    var nextMechanicShift = await _context.MechanicShifts
                                        .FirstOrDefaultAsync(ms => ms.MechanicId == user.Id && ms.ShiftId == shifts[i].Id && ms.StartTime == shiftStart);
                                    if (nextMechanicShift == null || nextMechanicShift.IsAvailable)
                                    {
                                        expectedTime = shiftStart;
                                        goto FoundNearestShift;
                                    }
                                }
                            }
                            daysToCheck++;
                            nearestShiftIndex = 0;
                        }
                    FoundNearestShift:
                        if (expectedTime == currentTime)
                        {
                            var nextDay = currentTime.Date.AddDays(2);
                            expectedTime = nextDay.Add(shifts[0].StartTime);
                        }
                    }
                }

                recommendations.Add(new GetMechanicRecommendation
                {
                    MechanicId = user.Id,
                    FullName = user.FullName ?? "Unknown",
                    AverageCompletionTime = avgCompletionTimeString,
                    ExpectedTime = expectedTime,
                    Message = mechanicShift != null && !mechanicShift.IsAvailable
                        ? "Chú ý: Thợ máy này đã có việc cho ca này!"
                        : "Đề xuất"
                });
            }
            return recommendations
                .OrderBy(r => ParseCompletionTime(r.AverageCompletionTime))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        private string FormatAverageCompletionTime(double minutes)
        {
            if (minutes < 0) return "Chưa có do chưa hoàn thành công việc nào!";
            if (minutes < 60)
            {
                return $"{(int)minutes} phút";
            }
            else
            {
                int hours = (int)minutes / 60;
                int remainingMinutes = (int)minutes % 60;
                return $"{hours} giờ {(remainingMinutes > 0 ? $"{remainingMinutes} phút" : "")}".Trim();
            }
        }

        private double ParseCompletionTime(string completionTime)
        {
            if (string.IsNullOrEmpty(completionTime) || completionTime.Contains("Chưa có")) return double.MaxValue;
            if (completionTime.Contains("phút"))
            {
                var minutesStr = completionTime.Replace(" phút", "").Trim();
                if (double.TryParse(minutesStr, out double minutes))
                    return minutes;
            }
            if (completionTime.Contains("giờ"))
            {
                var parts = completionTime.Split(new[] { " ", "giờ", "phút" }, StringSplitOptions.RemoveEmptyEntries);
                double totalMinutes = 0;
                for (int i = 0; i < parts.Length; i++)
                {
                    if (int.TryParse(parts[i], out int value))
                    {
                        totalMinutes += i % 2 == 0 ? value * 60 : value;
                    }
                }
                return totalMinutes > 0 ? totalMinutes : double.MaxValue;
            }
            return double.MaxValue; // Giá trị mặc định cho các trường hợp không hợp lệ
        }
        public async Task<List<Guid>> GetAllUserIdsAsync()
        {
            try
            {
                return await _context.Users
                    .AsNoTracking()
                    .Where(u => !u.IsDeleted)
                    .Select(u => u.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<User>> GetUsersByAreaIdAsync(Guid areaId)
        {
            return await _context.Users
                .Where(u => u.AreaId == areaId && !u.IsDeleted)
                .ToListAsync();
        }
    }
}

