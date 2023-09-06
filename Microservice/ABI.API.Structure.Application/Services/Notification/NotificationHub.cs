using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Notification
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        public async Task AssociateHub(string ChannelID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, ChannelID);
        }

        public async Task IsProcessing()
        {
            await Task.CompletedTask;
        }
    }   
}
