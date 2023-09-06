using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Notifications.Truck
{
    public class TruckProcessWarningNotification : INotificationMessage<TruckProcessStatusDTO>
    {
        private readonly TruckProcessStatusDTO _payload;
        private readonly IList<string> _messages;

        public string ChannelId => "MENSAJES_TRUCK";
        public int StatusCode => 200;
        public string StatusMessage => "Advertencia";
        public string Type => "ADVERTENCIAS";
        public string Username => _payload.Username;

        public TruckProcessStatusDTO Payload => _payload;

        public IList<string> Messages => _messages;


        public TruckProcessWarningNotification(int structureId, string structureName, DateTimeOffset validityFrom, string username, string message)
        {
            _payload = new TruckProcessStatusDTO
            {
                Structure = new TruckProcessStatusStructureDTO
                {
                    Id = structureId,
                    Name = structureName
                },

                ValidityFrom = validityFrom,
                Username = username
            };

            _messages = new List<string> { message };
        }
    }
}
