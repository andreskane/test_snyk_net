using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Services.Extensions;
using ABI.API.Structure.Application.Services.Interfaces;
using Coravel.Events.Interfaces;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck
{
    public class TruckProcessListenerError : IListener<TruckWritingEventError>
    {
        private readonly INotificationStatusService _notificationService;


        public TruckProcessListenerError(INotificationStatusService notificationService)
        {
            _notificationService = notificationService;
        }


        public async Task HandleAsync(TruckWritingEventError broadcasted)
        {
            var notification = new TruckProcessingErrorNotification(
                broadcasted.StructureId,
                broadcasted.StructureName,
                broadcasted.Date,
                broadcasted.Username
            );

            await _notificationService.Notify(notification);
        }
    }
}
