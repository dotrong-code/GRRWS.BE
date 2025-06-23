using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

public interface IMechanicPerformanceHistoryRepository : IGenericRepository<MechanicPerformanceHistory>
    {
         Task<List<MechanicPerformanceHistory>> GetByMechanicIdAsync(Guid mechanicId, int? limit = null);
        Task<List<MechanicPerformanceHistory>> GetByMechanicAndDateRangeAsync(Guid mechanicId, DateTime startDate, DateTime endDate);
        Task<List<MechanicPerformanceHistory>> GetByTaskTypeAsync(Guid mechanicId, TaskType taskType);
        Task<MechanicPerformanceHistory?> GetByTaskIdAsync(Guid taskId);
        Task<List<MechanicPerformanceHistory>> GetRecentPerformanceAsync(Guid mechanicId, int days);
    }