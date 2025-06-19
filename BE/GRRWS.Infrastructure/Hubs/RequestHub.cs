using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GRRWS.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GRRWS.Infrastructure.Hubs
{
    [Authorize]
    public class RequestHub : Hub
    {
        private readonly GRRWSContext _context;
        private readonly ILogger<RequestHub> _logger;

        public RequestHub(GRRWSContext context, ILogger<RequestHub> logger)
        {
            _context = context;
            _logger = logger;
        }
        //comenent
        // Khi client kết nối vào Hub
        public override async Task OnConnectedAsync()
        {
            // Lấy userId và role từ Claim sau khi đã xác thực
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // Thêm connection vào nhóm riêng theo user (dùng để gửi thông báo riêng)
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
                _logger.LogInformation($"User {userId} joined user group");
            }

            if (!string.IsNullOrEmpty(userRole))
            {
                // Thêm connection vào nhóm theo role (ví dụ để broadcast tới tất cả HOT chẳng hạn)
                await Groups.AddToGroupAsync(Context.ConnectionId, $"role:{userRole}");
                _logger.LogInformation($"User {userId} joined role group: {userRole}");
            }

            await base.OnConnectedAsync();
        }

        // Khi client ngắt kết nối khỏi Hub
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"User {userId} disconnected");

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation($"Connection {Context.ConnectionId} joined group {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation($"Connection {Context.ConnectionId} left group {groupName}");
        }
    }
}