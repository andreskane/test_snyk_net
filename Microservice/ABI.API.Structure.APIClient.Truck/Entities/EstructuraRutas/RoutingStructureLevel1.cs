using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.EstructuraRutas
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class RoutingStructureLevel1
    {
        /// <summary>
        /// Territorio
        /// </summary>
        /// <value>Territorio</value>
        [DataMember(Name = "CliTrrId")]
        public string CliTrrId { get; set; }

        /// <summary>
        /// Código de Cliente
        /// </summary>
        /// <value>Código de Cliente</value>
        [DataMember(Name = "CliId")]
        public string CliId { get; set; }

        /// <summary>
        /// Nombre completo - Razón Social
        /// </summary>
        /// <value>Nombre completo - Razón Social</value>
        [DataMember(Name = "CliNom")]
        public string CliNom { get; set; }

        /// <summary>
        /// CLIABV
        /// </summary>
        /// <value>CLIABV</value>
        [DataMember(Name = "CliAbv")]
        public string CliAbv { get; set; }

        /// <summary>
        /// CLIDOM
        /// </summary>
        /// <value>CLIDOM</value>
        [DataMember(Name = "CliDom")]
        public string CliDom { get; set; }

        /// <summary>
        /// CLINRODOM
        /// </summary>
        /// <value>CLINRODOM</value>
        [DataMember(Name = "CliNroDom")]
        public string CliNroDom { get; set; }

        /// <summary>
        /// Status del Cliente
        /// </summary>
        /// <value>Status del Cliente</value>
        [DataMember(Name = "CliSts")]
        public string CliSts { get; set; }

        /// <summary>
        /// Frecuencia Visita
        /// </summary>
        /// <value>Frecuencia Visita</value>
        [DataMember(Name = "CliTrrValF")]
        public string CliTrrValF { get; set; }

        /// <summary>
        /// Dia de visita
        /// </summary>
        /// <value>Dia de visita</value>
        [DataMember(Name = "CliTrrNroD")]
        public string CliTrrNroD { get; set; }

        /// <summary>
        /// Porcentaje Crédito
        /// </summary>
        /// <value>Porcentaje Crédito</value>
        [DataMember(Name = "CliTrrPrjC")]
        public string CliTrrPrjC { get; set; }

        /// <summary>
        /// Saldo Cuenta
        /// </summary>
        /// <value>Saldo Cuenta</value>
        [DataMember(Name = "CliTrrSdoC")]
        public string CliTrrSdoC { get; set; }

        /// <summary>
        /// Centro de Despacho
        /// </summary>
        /// <value>Centro de Despacho</value>
        [DataMember(Name = "CliTrrIdCd")]
        public string CliTrrIdCd { get; set; }

        /// <summary>
        /// Descrip.del Centro Despacho
        /// </summary>
        /// <value>Descrip.del Centro Despacho</value>
        [DataMember(Name = "CdpAbv")]
        public string CdpAbv { get; set; }

        /// <summary>
        /// Orden Visita
        /// </summary>
        /// <value>Orden Visita</value>
        [DataMember(Name = "CliTrrOrdV")]
        public string CliTrrOrdV { get; set; }

        /// <summary>
        /// Canal Venta
        /// </summary>
        /// <value>Canal Venta</value>
        [DataMember(Name = "CnlId")]
        public string CnlId { get; set; }

        /// <summary>
        /// Descripción Venta
        /// </summary>
        /// <value>Descripción Venta</value>
        [DataMember(Name = "CnlAbv")]
        public string CnlAbv { get; set; }

        /// <summary>
        /// Gets or Sets Level2
        /// </summary>
        [DataMember(Name = "Level2")]
        public List<RoutingStructureLevel2> Level2 { get; set; }

        /// <summary>
        /// Gets or sets the zon ent identifier.
        /// </summary>
        /// <value>
        /// The zon ent identifier.
        /// </value>
        [DataMember(Name = "ZonEntId")]
        public string ZonEntId { get; set; }


    }
}
