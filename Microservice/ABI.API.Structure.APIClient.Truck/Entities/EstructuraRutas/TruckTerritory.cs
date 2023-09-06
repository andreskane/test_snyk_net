using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraRutas
{
    [DataContract]
    public class TruckTerritory
    {
        [DataMember(Name = "ConsultationDate")]
        public DateTimeOffset ConsultationDate { get; set; }

        [DataMember(Name = "EstructuraRuteo")]
        public RoutingStructure RoutingStructure { get; set; }
    }
}
