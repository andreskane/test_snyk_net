using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Services.Extensions;
using ABI.API.Structure.Application.Services.Interfaces;
using Coravel.Events.Interfaces;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck
{
    public class TruckProcessListenerStart : IListener<TruckWritingEventStart>
    {
        private readonly INotificationStatusService _notificationService;


        public TruckProcessListenerStart(INotificationStatusService notificationService)
        {
            _notificationService = notificationService;
        }


        public async Task HandleAsync(TruckWritingEventStart broadcasted)
        {
            var notification = new TruckProcessingNotification(
                broadcasted.StructureId,
                broadcasted.StructureName,
                broadcasted.Date,
                broadcasted.Username
            );

            await _notificationService.Notify(notification);
        }
    }
}
