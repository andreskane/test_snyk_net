using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using Newtonsoft.Json;

namespace ABI.API.Structure.ACL.Truck.Application.Extensions.TruckStep
{
    public static class TruckWritingEvents
    {
        public static TruckWritingEventStart ToStartEvent(this TruckWritingPayload input)
        {
            return new TruckWritingEventStart
            {
                StructureId = input.StructureId,
                StructureName = input.StructureName,
                Date = input.Date,
                Username = input.Username
            };
        }

        public static TruckWritingEventDone ToDoneEvent(this TruckWritingPayload input)
        {
            return new TruckWritingEventDone
            {
                StructureId = input.StructureId,
                StructureName = input.StructureName,
                Date = input.Date,
                Username = input.Username
            };
        }

        public static TruckWritingEventError ToErrorEvent(this TruckWritingPayload input)
        {
            return new TruckWritingEventError
            {
                StructureId = input.StructureId,
                StructureName = input.StructureName,
                Date = input.Date,
                Username = input.Username
            };
        }

        public static TruckWritingEventWarning ToWarningEvent(this TruckWritingPayload input, string menssage)
        {
            return new TruckWritingEventWarning
            {
                StructureId = input.StructureId,
                StructureName = input.StructureName,
                Date = input.Date,
                Username = input.Username,
                Message = menssage
            };
        }

        public static TruckWritingEventWarning ToWarningEvent(this TruckWritingPayload input, object menssageObject)
        {
            return new TruckWritingEventWarning
            {
                StructureId = input.StructureId,
                StructureName = input.StructureName,
                Date = input.Date,
                Username = input.Username,
                Message = JsonConvert.SerializeObject(menssageObject)
            };
        }
    }
}
