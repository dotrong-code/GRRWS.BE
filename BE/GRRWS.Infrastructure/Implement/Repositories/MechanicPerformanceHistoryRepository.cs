using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;

using GRRWS.Infrastructure.Implement.Repositories.Generic;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class MechanicPerformanceHistoryRepository : GenericRepository<MechanicPerformanceHistory>, IMechanicPerformanceHistoryRepository
    {
        public MechanicPerformanceHistoryRepository(GRRWSContext context) : base(context)
        {
        }

        public Task<List<MechanicPerformanceHistory>> GetByMechanicAndDateRangeAsync(Guid mechanicId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<MechanicPerformanceHistory>> GetByMechanicIdAsync(Guid mechanicId, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<MechanicPerformanceHistory?> GetByTaskIdAsync(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MechanicPerformanceHistory>> GetByTaskTypeAsync(Guid mechanicId, TaskType taskType)
        {
            throw new NotImplementedException();
        }

        public Task<List<MechanicPerformanceHistory>> GetRecentPerformanceAsync(Guid mechanicId, int days)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<MechanicPerformanceHistory>> GetByMechanicIdAsync(Guid mechanicId, int? limit = null)
        //{
        //    IQueryable<MechanicPerformanceHistory> query = _context.MechanicPerformanceHistories
        //        .AsNoTracking()
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && !mph.IsDeleted)
        //        .OrderByDescending(mph => mph.RecordedDate);

        //    if (limit.HasValue)
        //    {
        //        query = query.Take(limit.Value);
        //    }

        //    return await query.ToListAsync();
        //}

        //public async Task<List<MechanicPerformanceHistory>> GetByMechanicAndDateRangeAsync(Guid mechanicId, DateTime startDate, DateTime endDate)
        //{
        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && 
        //                     !mph.IsDeleted &&
        //                     mph.RecordedDate >= startDate && 
        //                     mph.RecordedDate <= endDate)
        //        .OrderByDescending(mph => mph.RecordedDate)
        //        .ToListAsync();
        //}

        //public async Task<List<MechanicPerformanceHistory>> GetByTaskTypeAsync(Guid mechanicId, TaskType taskType)
        //{
        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && 
        //                     mph.TaskType == taskType && 
        //                     !mph.IsDeleted)
        //        .OrderByDescending(mph => mph.RecordedDate)
        //        .ToListAsync();
        //}

        //public async Task<MechanicPerformanceHistory?> GetByTaskIdAsync(Guid taskId)
        //{
        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .FirstOrDefaultAsync(mph => mph.TaskId == taskId && !mph.IsDeleted);
        //}

        //public async Task<List<MechanicPerformanceHistory>> GetRecentPerformanceAsync(Guid mechanicId, int days)
        //{
        //    var cutoffDate = DateTime.Now.AddDays(-days);

        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && 
        //                     !mph.IsDeleted &&
        //                     mph.RecordedDate >= cutoffDate)
        //        .OrderByDescending(mph => mph.RecordedDate)
        //        .ToListAsync();
        //}

        //public async Task<List<MechanicPerformanceHistory>> GetTasksRequiringReworkAsync(Guid mechanicId)
        //{
        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && 
        //                     mph.RequiredRework && 
        //                     !mph.IsDeleted)
        //        .OrderByDescending(mph => mph.RecordedDate)
        //        .ToListAsync();
        //}

        //public async Task<double> GetAverageQualityScoreAsync(Guid mechanicId, TaskType? taskType = null)
        //{
        //    var query = _context.MechanicPerformanceHistories
        //        .Where(mph => mph.MechanicId == mechanicId && !mph.IsDeleted);

        //    if (taskType.HasValue)
        //    {
        //        query = query.Where(mph => mph.TaskType == taskType.Value);
        //    }

        //    var scores = await query.Select(mph => mph.QualityScore).ToListAsync();

        //    return scores.Any() ? scores.Average() : 0;
        //}

        //public async Task<List<MechanicPerformanceHistory>> GetPerformanceTrendAsync(Guid mechanicId, int days)
        //{
        //    var cutoffDate = DateTime.Now.AddDays(-days);

        //    return await _context.MechanicPerformanceHistories
        //        .Include(mph => mph.Mechanic)
        //        .Include(mph => mph.Task)
        //        .Where(mph => mph.MechanicId == mechanicId && 
        //                     !mph.IsDeleted &&
        //                     mph.RecordedDate >= cutoffDate)
        //        .OrderBy(mph => mph.RecordedDate)
        //        .ToListAsync();
        //}

        //public async Task<Dictionary<TaskType, double>> GetAverageTimeByTaskTypeAsync(Guid mechanicId)
        //{
        //    var history = await _context.MechanicPerformanceHistories
        //        .Where(mph => mph.MechanicId == mechanicId && !mph.IsDeleted)
        //        .GroupBy(mph => mph.TaskType)
        //        .Select(g => new 
        //        {
        //            TaskType = g.Key,
        //            AverageTime = g.Average(mph => mph.ActualDurationMinutes)
        //        })
        //        .ToListAsync();

        //    return history.ToDictionary(h => h.TaskType, h => h.AverageTime);
        //}
    }
}