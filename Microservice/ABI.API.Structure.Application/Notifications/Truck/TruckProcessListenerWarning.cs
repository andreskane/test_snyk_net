using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.Application.Services.Extensions;
using ABI.API.Structure.Application.Services.Interfaces;
using Coravel.Events.Interfaces;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck
{
    public class TruckProcessListenerWarning : IListener<TruckWritingEventWarning>
    {
        private readonly INotificationStatusService _notificationService;


        public TruckProcessListenerWarning(INotificationStatusService notificationService)
        {
            _notificationService = notificationService;
        }


        public async Task HandleAsync(TruckWritingEventWarning broadcasted)
        {
            var notification = new TruckProcessWarningNotification(
                broadcasted.StructureId,
                broadcasted.StructureName,
                broadcasted.Date,
                broadcasted.Username,
                broadcasted.Message
            );

            await _notificationService.Notify(notification);
        }
    }
}
