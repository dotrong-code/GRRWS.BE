using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Mechanic;

namespace GRRWS.Application.Interface.IService
{
    public interface IMechanicPerformanceService
    {
        Task<MechanicPerformanceProfile> GetMechanicPerformanceAsync(Guid mechanicId);
        Task UpdateMechanicPerformanceAsync(Guid mechanicId, Guid completedTaskId);
        Task<List<MechanicRanking>> GetMechanicRankingsAsync();
        Task<bool> IsMechanicAvailableAsync(Guid mechanicId, DateTime startTime, DateTime endTime);
        Task<List<User>> GetAvailableMechanicsInShiftAsync(DateTime targetTime, TimeSpan duration);
        Task<List<MechanicPerformanceHistory>> GetMechanicPerformanceHistoryAsync(Guid mechanicId, int? limit = null);
        Task<MechanicPerformanceProfile> GetBestMechanicForTaskTypeAsync(TaskType taskType);
        Task<List<MechanicPerformanceProfile>> GetTopPerformersAsync(int count);
        Task MarkTaskAsRequiredReworkAsync(Guid taskId, string reason);
    }
}
