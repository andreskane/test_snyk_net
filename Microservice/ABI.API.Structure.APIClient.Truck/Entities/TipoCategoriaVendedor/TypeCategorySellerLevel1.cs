using System.Runtime.Serialization;

namespace ABI.API.Structure.APIClient.Truck.Entities.TipoCategoriaVendedor
{
    [DataContract]
    public class TypeCategorySellerLevel1
    {
        /// <summary>
        /// Id Categorí­a de Personal
        /// </summary>
        /// <value>Id Categorí­a de Personal</value>
        [DataMember(Name = "CatResID")]
        public string CatResID { get; set; }

        /// <summary>
        /// Descripción de Categorí­a
        /// </summary>
        /// <value>Descripción de Categorí­a</value>
        [DataMember(Name = "CatResNom")]
        public string CatResNom { get; set; }

        /// <summary>
        /// Descripción Reducida
        /// </summary>
        /// <value>Descripción Reducida</value>
        [DataMember(Name = "CatResAbv")]
        public string CatResAbv { get; set; }

        /// <summary>
        /// Requiere Punto de Emisión
        /// </summary>
        /// <value>Requiere Punto de Emisión</value>
        [DataMember(Name = "CatResPtoE")]
        public string CatResPtoE { get; set; }

        /// <summary>
        /// Requiere Usuario o Cliente
        /// </summary>
        /// <value>Requiere Usuario o Cliente</value>
        [DataMember(Name = "CatResUsrI")]
        public string CatResUsrI { get; set; }

        /// <summary>
        /// Requiere Calculo Comisiones
        /// </summary>
        /// <value>Requiere Calculo Comisiones</value>
        [DataMember(Name = "CatResCom")]
        public string CatResCom { get; set; }

        /// <summary>
        /// Comisiones Valor por DFT
        /// </summary>
        /// <value>Comisiones Valor por DFT</value>
        [DataMember(Name = "CatResComD")]
        public string CatResComD { get; set; }

        /// <summary>
        /// Requiere Nro de Legajo
        /// </summary>
        /// <value>Requiere Nro de Legajo</value>
        [DataMember(Name = "CatResLeg")]
        public string CatResLeg { get; set; }

        /// <summary>
        /// Id Categorí­a Autonumerico
        /// </summary>
        /// <value>Id Categorí­a Autonumerico</value>
        [DataMember(Name = "CatResAuto")]
        public string CatResAuto { get; set; }

        /// <summary>
        /// Permite Estado Vacante
        /// </summary>
        /// <value>Permite Estado Vacante</value>
        [DataMember(Name = "CatResVac")]
        public string CatResVac { get; set; }

        /// <summary>
        /// Controla Estado de Estructura
        /// </summary>
        /// <value>Controla Estado de Estructura</value>
        [DataMember(Name = "CatResEstF")]
        public string CatResEstF { get; set; }

        /// <summary>
        /// Código de Autonumerador desde
        /// </summary>
        /// <value>Código de Autonumerador desde</value>
        [DataMember(Name = "CatResNroD")]
        public string CatResNroD { get; set; }
    }
}
