using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    [DataContract]
    public class TruckStructure
    {

        [DataMember(Name = "ConsultationDate")]
        public DateTimeOffset ConsultationDate { get; set; }

        [DataMember(Name = "EstructuraDatos")]
        public DataStructure DataStructure { get; set; }
    }
}
