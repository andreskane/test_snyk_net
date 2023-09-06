using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.CentroDeDespacho
{
    [DataContract]
    public class OfficeCenterLevel1
    {
        /// <summary>
        /// Emp Id
        /// </summary>
        /// <value>Emp Id</value>
        [DataMember(Name = "EmpId")]
        public string EmpId { get; set; }

        /// <summary>
        /// CDPID
        /// </summary>
        /// <value>CDPID</value>
        [DataMember(Name = "CdpId")]
        public string CdpId { get; set; }

        /// <summary>
        /// Descrip.del Centro Despacho
        /// </summary>
        /// <value>Descrip.del Centro Despacho</value>
        [DataMember(Name = "CdpAbv")]
        public string CdpAbv { get; set; }

        /// <summary>
        /// Cdp Id Dps Id Df
        /// </summary>
        /// <value>Cdp Id Dps Id Df</value>
        [DataMember(Name = "CdpIdDpsIdDf")]
        public string CdpIdDpsIdDf { get; set; }

        /// <summary>
        /// DescripciÃ³n de DepÃ³sito
        /// </summary>
        /// <value>Descripción de Depósito</value>
        [DataMember(Name = "CdpDpsTxt")]
        public string CdpDpsTxt { get; set; }

        /// <summary>
        /// CDPFLGCODD
        /// </summary>
        /// <value>CDPFLGCODD</value>
        [DataMember(Name = "CdpFlgCodD")]
        public string CdpFlgCodD { get; set; }

        /// <summary>
        /// CDPTPOCTLM
        /// </summary>
        /// <value>CDPTPOCTLM</value>
        [DataMember(Name = "CdpTpoCtlM")]
        public string CdpTpoCtlM { get; set; }

        /// <summary>
        /// Cdp Val Min
        /// </summary>
        /// <value>Cdp Val Min</value>
        [DataMember(Name = "CdpValMin")]
        public string CdpValMin { get; set; }
    }
}
