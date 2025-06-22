using GRRWS.Application.Common.Result;
using GRRWS.Application.Interfaces;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Domain.Entities;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using GRRWS.Infrastructure.DTOs.Common.Message;

namespace GRRWS.Application.Implement.Service
{
    public class ExpoPushService : IExpoPushService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExpoPushService> _logger;
        private readonly string _expoApiUrl = "https://exp.host/--/api/v2/push/send";

        public ExpoPushService(HttpClient httpClient, IUnitOfWork unitOfWork, ILogger<ExpoPushService> logger)
        {
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> SendPushNotificationAsync(string pushToken, string title, string body, object data = null)
        {
            return await SendPushNotificationsAsync(new List<string> { pushToken }, title, body, data);
        }

        public async Task<Result> SendPushNotificationsAsync(List<string> pushTokens, string title, string body, object data = null)
        {
            try
            {
                if (pushTokens == null || !pushTokens.Any())
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Push tokens"));

                var validTokens = pushTokens.Where(token => IsValidExpoToken(token)).ToList();
                
                if (!validTokens.Any())
                {
                    _logger.LogWarning("No valid Expo push tokens provided");
                    return Result.Failure(NotificationErrorMessage.InvalidPlatform());
                }

                var messages = validTokens.Select(token => new
                {
                    to = token,
                    title = title,
                    body = body,
                    data = data,
                    sound = "default",
                    priority = "high"
                }).ToList();

                var json = JsonSerializer.Serialize(messages);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                var response = await _httpClient.PostAsync(_expoApiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully sent push notification to {TokenCount} tokens", validTokens.Count);
                    await HandleFailedTokens(validTokens, responseContent);
                    return Result.SuccessWithObject(new { sentCount = validTokens.Count });
                }
                else
                {
                    _logger.LogError("Failed to send push notification. Status: {StatusCode}, Response: {Response}", 
                        response.StatusCode, responseContent);
                    
                    await HandleFailedTokens(validTokens, responseContent);
                    return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending push notification");
                return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
            }
        }

        public async Task<Result> RegisterPushTokenAsync(Guid userId, string token, string platform)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Token"));

                if (string.IsNullOrEmpty(platform))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Platform"));

                var validPlatforms = new[] { "ios", "android" };
                if (!validPlatforms.Contains(platform.ToLower()))
                    return Result.Failure(NotificationErrorMessage.InvalidPlatform());

                var existingToken = await _unitOfWork.PushTokenRepository.GetByUserIdAndTokenAsync(userId, token);

                if (existingToken != null)
                {
                    existingToken.LastUsed = DateTime.UtcNow;
                    existingToken.IsActive = true;
                    existingToken.Platform = platform;
                    
                    await _unitOfWork.PushTokenRepository.UpdateAsync(existingToken);
                }
                else
                {
                    var pushToken = new PushToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Token = token,
                        Platform = platform,
                        IsActive = true,
                        LastUsed = DateTime.UtcNow,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _unitOfWork.PushTokenRepository.AddAsync(pushToken);
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Push token registered successfully for user {UserId}", userId);
                
                return Result.SuccessWithObject(new 
                { 
                    message = "Push token registered successfully",
                    userId = userId,
                    platform = platform.ToLower()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering push token for user {UserId}", userId);
                return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
            }
        }

        public async Task<Result> DeactivateTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return Result.Failure(NotificationErrorMessage.FieldIsEmpty("Token"));

                await _unitOfWork.PushTokenRepository.DeactivateTokenAsync(token);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Push token deactivated successfully");
                
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating push token {Token}", token);
                return Result.Failure(NotificationErrorMessage.PushTokenRegistrationFailed());
            }
        }

        public async Task<Result> GetUserPushTokensAsync(Guid userId)
        {
            try
            {
                var tokens = await _unitOfWork.PushTokenRepository.GetActiveTokensByUserIdAsync(userId);
                return Result.SuccessWithObject(tokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving push tokens for user {UserId}", userId);
                return Result.Failure(NotificationErrorMessage.NotificationRetrieveFailed());
            }
        }

        public async Task<Result> GetTokensByRoleAsync(int role)
        {
            try
            {
                var tokens = await _unitOfWork.PushTokenRepository.GetTokensByRoleAsync(role);
                return Result.SuccessWithObject(tokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving push tokens for role {Role}", role);
                return Result.Failure(NotificationErrorMessage.NotificationRetrieveFailed());
            }
        }

        private bool IsValidExpoToken(string token)
        {
            return !string.IsNullOrEmpty(token) && 
                   (token.StartsWith("ExponentPushToken[") || token.StartsWith("ExpoPushToken["));
        }

        private async Task HandleFailedTokens(List<string> tokens, string responseContent)
        {
            try
            {
                var response = JsonSerializer.Deserialize<JsonElement>(responseContent);
                if (response.TryGetProperty("data", out var dataArray))
                {
                    var dataList = dataArray.EnumerateArray().ToList();
                    for (int i = 0; i < Math.Min(tokens.Count, dataList.Count); i++)
                    {
                        var tokenResponse = dataList[i];
                        if (tokenResponse.TryGetProperty("status", out var status) && 
                            status.GetString() == "error")
                        {
                            if (tokenResponse.TryGetProperty("details", out var details) &&
                                details.TryGetProperty("error", out var error))
                            {
                                var errorType = error.GetString();
                                if (errorType == "DeviceNotRegistered" || errorType == "InvalidCredentials")
                                {
                                    await DeactivateTokenAsync(tokens[i]);
                                    _logger.LogInformation("Deactivated invalid token: {Token}", tokens[i]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling failed push tokens");
            }
        }
    }
}