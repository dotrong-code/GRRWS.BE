using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class PushTokenRepository : GenericRepository<PushToken>, IPushTokenRepository
    {
        private readonly ILogger<PushTokenRepository> _logger;

        public PushTokenRepository(GRRWSContext context, ILogger<PushTokenRepository> logger) : base(context)
        {
            _logger = logger;
        }

        // Gets all active push tokens for a specific user
        public async Task<List<string>> GetActiveTokensByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.PushTokens
                    .AsNoTracking()
                    .Where(pt => pt.UserId == userId && pt.IsActive)
                    .Select(pt => pt.Token)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active push tokens for user {UserId}", userId);
                throw;
            }
        }

        // Gets a push token entity by user ID and token value
        public async Task<PushToken?> GetByUserIdAndTokenAsync(Guid userId, string token)
        {
            try
            {
                return await _context.PushTokens
                    .FirstOrDefaultAsync(pt => pt.UserId == userId && pt.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving push token for user {UserId} and token {Token}", userId, token);
                throw;
            }
        }

        // Gets a push token entity by token value
        public async Task<PushToken?> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.PushTokens
                    .FirstOrDefaultAsync(pt => pt.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving push token {Token}", token);
                throw;
            }
        }

        // Adds a new push token to the database
        public async Task AddAsync(PushToken pushToken)
        {
            try
            {
                await _context.PushTokens.AddAsync(pushToken);
                _logger.LogDebug("Push token added for user {UserId}", pushToken.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding push token for user {UserId}", pushToken.UserId);
                throw;
            }
        }

        // Updates an existing push token
        public async Task UpdateAsync(PushToken pushToken)
        {
            try
            {
                _context.PushTokens.Update(pushToken);
                _logger.LogDebug("Push token updated for user {UserId}", pushToken.UserId);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating push token for user {UserId}", pushToken.UserId);
                throw;
            }
        }

        // Deactivates a push token by setting IsActive to false
        public async Task DeactivateTokenAsync(string token)
        {
            try
            {
                var pushToken = await GetByTokenAsync(token);
                if (pushToken != null)
                {
                    pushToken.IsActive = false;
                    await UpdateAsync(pushToken);
                    _logger.LogInformation("Push token deactivated: {Token}", token);
                }
                else
                {
                    _logger.LogWarning("Push token not found for deactivation: {Token}", token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating push token {Token}", token);
                throw;
            }
        }

        // Gets all push tokens for users with a specific role
        public async Task<List<string>> GetTokensByRoleAsync(int role)
        {
            try
            {
                return await _context.PushTokens
                    .AsNoTracking()
                    .Include(pt => pt.User)
                    .Where(pt => pt.IsActive && pt.User.Role == role)
                    .Select(pt => pt.Token)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving push tokens for role {Role}", role);
                throw;
            }
        }

        // Saves changes to the database
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes to database");
                throw;
            }
        }
    }
}