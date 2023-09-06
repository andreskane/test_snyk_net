using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraRutas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class RoutingStructureLevel2
    {
        /// <summary>
        /// Codigo Forma Atencion Cliente
        /// </summary>
        /// <value>Codigo Forma Atencion Cliente</value>
        [DataMember(Name = "AtnCod")]
        public string AtnCod { get; set; }

        /// <summary>
        /// Id Ruta
        /// </summary>
        /// <value>Id Ruta</value>
        [DataMember(Name = "RutId")]
        public string RutId { get; set; }

        /// <summary>
        /// Local Cliente
        /// </summary>
        /// <value>Local Cliente</value>
        [DataMember(Name = "CliIdLoc")]
        public string CliIdLoc { get; set; }

        /// <summary>
        /// Rut Vta Val O
        /// </summary>
        /// <value>Rut Vta Val O</value>
        [DataMember(Name = "RutVtaValO")]
        public string RutVtaValO { get; set; }

        /// <summary>
        /// Tipo de Ruta
        /// </summary>
        /// <value>Tipo de Ruta</value>
        [DataMember(Name = "RutTpo")]
        public string RutTpo { get; set; }

        /// <summary>
        /// Día de Visita
        /// </summary>
        /// <value>Día de Visita</value>
        [DataMember(Name = "RutNroDia")]
        public string RutNroDia { get; set; }

        /// <summary>
        /// Nombre de Día
        /// </summary>
        /// <value>Nombre de Día</value>
        [DataMember(Name = "RutDiaTxt")]
        public string RutDiaTxt { get; set; }

    }
}
