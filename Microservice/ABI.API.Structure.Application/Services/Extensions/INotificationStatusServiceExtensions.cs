using ABI.API.Structure.Application.Services.Interfaces;
using ABI.Framework.MS.Helpers.Response;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Services.Extensions
{
    public static class INotificationStatusServiceExtensions
    {
        public static async Task Notify<TNotification>(this INotificationStatusService service, TNotification notification)
            where TNotification : INotificationMessage<object>
        {
            var response = new GenericResponse();
            response.StatusResponse.Code = notification.StatusCode;
            response.StatusResponse.Message = notification.StatusMessage;
            response.Type = notification.Type;
            response.Result = notification.Payload;

            if (notification.Messages.Any())
                foreach (var item in notification.Messages)
                    response.AddMessage(item);

            await service.Notify(notification.ChannelId, response);
        }
    }
}
