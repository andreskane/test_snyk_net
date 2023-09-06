using ABI.API.Structure.Application.Services.Interfaces;
using ABI.Framework.MS.Helpers.Response;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Notification
{
    public class NotificationStatusService : INotificationStatusService
    {
        private readonly ILogger<NotificationStatusService> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationStatusService(ILogger<NotificationStatusService> logger, IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _logger = logger;
        }


        public async Task Notify(string ChannelId, GenericResponse response)
        {
            _logger.LogInformation(response.StatusResponse.Message ?? string.Empty);
            await _hubContext.Clients.Group(ChannelId).SendAsync("result", response);
        }
    }
}
