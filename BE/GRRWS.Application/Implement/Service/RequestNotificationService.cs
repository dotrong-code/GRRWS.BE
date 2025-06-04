using GRRWS.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GRRWS.Application.Implement.Service
{
    public class RequestNotificationService
    {
        private readonly IHubContext<RequestHub> _hubContext;

        public RequestNotificationService(IHubContext<RequestHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAllAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("NewRequestCreated", message);
        }

        public async Task NotifyGroupAsync(string groupName, string message)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("NewRequestCreated", message);
        }
    }
}