using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CentroDeDespacho
{
    [DataContract]
    public class TruckOfficeCenter
    {
        /// <summary>
        /// Centro De Despacho
        /// </summary>
        /// <value>Centro De Despacho</value>
        [DataMember(Name = "CentroDeDespacho")]
        public OfficeCenter OfficeCenter { get; set; }
    }
}
