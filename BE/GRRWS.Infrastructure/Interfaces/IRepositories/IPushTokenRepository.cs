using GRRWS.Domain.Entities;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IPushTokenRepository
    {
        Task<List<string>> GetActiveTokensByUserIdAsync(Guid userId);
        Task<PushToken?> GetByUserIdAndTokenAsync(Guid userId, string token);
        Task<PushToken?> GetByTokenAsync(string token);
        Task AddAsync(PushToken pushToken);
        Task UpdateAsync(PushToken pushToken);
        Task DeactivateTokenAsync(string token);
        Task<List<string>> GetTokensByRoleAsync(int role);
        Task<int> SaveChangesAsync();
    }
}