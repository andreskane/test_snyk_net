using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DataStructure
    {
        /// <summary>
        /// Empresa de Trabajo
        /// </summary>
        /// <value>Empresa de Trabajo</value>
        [DataMember(Name = "EmpId")]
        public string EmpId { get; set; }

        /// <summary>
        /// Gets or Sets Level1
        /// </summary>
        [DataMember(Name = "Level1")]
        public List<DataStructureLevel1> Level1 { get; set; }

    }
}
