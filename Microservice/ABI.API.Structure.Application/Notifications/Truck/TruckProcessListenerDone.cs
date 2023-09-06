using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Services.Extensions;
using ABI.API.Structure.Application.Services.Interfaces;
using Coravel.Events.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck
{
    public class TruckProcessListenerDone : IListener<TruckWritingEventDone>
    {
        private readonly INotificationStatusService _notificationService;
        private readonly IVersionedRepository _versionnedRepo;
        private readonly IVersionedLogRepository _versionedLogRepo;
        private readonly IVersionedLogStatusRepository _versionedLogStatusRepo;


        public TruckProcessListenerDone(
            INotificationStatusService notificationService,
            IVersionedRepository versionnedRepo,
            IVersionedLogRepository versionedLogRepository,
            IVersionedLogStatusRepository versionedLogStatusRepo)
        {
            _notificationService = notificationService;
            _versionnedRepo = versionnedRepo;
            _versionedLogRepo = versionedLogRepository;
            _versionedLogStatusRepo = versionedLogStatusRepo;
        }


        public async Task HandleAsync(TruckWritingEventDone broadcasted)
        {
            INotificationMessage<TruckProcessStatusDTO> notification;

            var impactLog = _versionnedRepo
                .Filter(w => w.StructureId == broadcasted.StructureId)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            var impactStatusLog = _versionedLogRepo
                .Filter(w => w.VersionedId == impactLog.Id)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            if (impactStatusLog.LogStatusId >= 100)
            {
                var status = await _versionedLogStatusRepo.GetAll();

                if (impactStatusLog.LogStatusId == 106)
                {
                    var json = impactStatusLog.Detaill;
                    var messages = new List<string> { "Truck no generó ningún detalle sobre el error" };

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        try
                        {
                            var parsedJson = JObject.Parse(json);

                            if (parsedJson.ContainsKey("msglog") &&
                                (parsedJson["msglog"] as JObject).ContainsKey("Level1"))
                            {

                                messages = parsedJson["msglog"]["Level1"]
                                    .Children()
                                    .Select(s => s["ECLogTxt"].Value<string>())
                                    .ToList();
                            }
                        }
                        catch { /* Devuelvo un mensaje por defecto */ }
                    }

                    notification = new TruckProcessFailNotification(
                        broadcasted.StructureId,
                        broadcasted.StructureName,
                        broadcasted.Date,
                        broadcasted.Username,
                        messages
                    );
                }
                else
                {
                    var message = status
                        .FirstOrDefault(f => f.Id == impactStatusLog.LogStatusId)
                        ?.Name ?? $"Código de error desconocido ({impactStatusLog.LogStatusId})";

                    notification = new TruckProcessFailNotification(
                        broadcasted.StructureId,
                        broadcasted.StructureName,
                        broadcasted.Date,
                        broadcasted.Username,
                        new List<string> { message }
                    );
                }
            }
            else
            {
                notification = new TruckProcessDoneNotification(
                    broadcasted.StructureId,
                    broadcasted.StructureName,
                    broadcasted.Date,
                    broadcasted.Username
                );
            }

            await _notificationService.Notify(notification);
        }
    }
}
