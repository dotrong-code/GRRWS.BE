using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

public interface IMechanicPerformanceRepository : IGenericRepository<MechanicPerformance>
    {
       Task<MechanicPerformance?> GetByMechanicIdAsync(Guid mechanicId);
        Task<List<MechanicPerformance>> GetTopPerformersAsync(int count);
        Task<List<MechanicPerformance>> GetPerformanceByTaskTypeAsync(TaskType taskType);
        Task CreateOrUpdateAsync(MechanicPerformance performance);
        Task<List<MechanicPerformance>> GetMechanicsWithMinimumTasksAsync(int minimumTasks);
        Task<MechanicPerformance?> GetBestPerformerByTaskTypeAsync(TaskType taskType);
    }