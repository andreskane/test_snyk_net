using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.Responsables
{
    [DataContract]
    public class ResponsibleLevel1
    {
        /// <summary>
        /// Vdr Cod
        /// </summary>
        /// <value>Vdr Cod</value>
        [DataMember(Name = "VdrCod")]
        public string VdrCod { get; set; }

        /// <summary>
        /// Nombre Vendedor Actual
        /// </summary>
        /// <value>Nombre Vendedor Actual</value>
        [DataMember(Name = "VdrNom")]
        public string VdrNom { get; set; }

        /// <summary>
        /// Categora del Personal
        /// </summary>
        /// <value>Categora del Personal</value>
        [DataMember(Name = "VdrTpoCat")]
        public string VdrTpoCat { get; set; }

        /// <summary>
        /// Tipo ComisiÃ³n
        /// </summary>
        /// <value>Tipo ComisiÃ³n</value>
        [DataMember(Name = "VdrTpoLpr")]
        public string VdrTpoLpr { get; set; }

        /// <summary>
        /// Codigo Externo
        /// </summary>
        /// <value>Codigo Externo</value>
        [DataMember(Name = "VdrCodExt")]
        public string VdrCodExt { get; set; }

        /// <summary>
        /// Punto de Emision de Venta
        /// </summary>
        /// <value>Punto de Emision de Venta</value>
        [DataMember(Name = "VdrIdPtoEm")]
        public string VdrIdPtoEm { get; set; }

        /// <summary>
        /// Codigo de Legajo
        /// </summary>
        /// <value>Codigo de Legajo</value>
        [DataMember(Name = "VdrCodLega")]
        public string VdrCodLega { get; set; }
    }
}
