namespace ABI.API.Structure.ACL.Truck.Application
{
    public enum Trasformation
    {
        PortalToTruck = 0,
        TruckToPortal
    }


    public enum TypeProcessTruck
    {
        /// <summary>Genera nueva versión</summary>
        NEW = 0,
        /// <summary>Aplica el cambio segun la fecha de impacto</summary>
        APR,
        /// <summary>Aplica cambio en el momento</summary>
        OPE,
        /// <summary> Recupera la versión que ya había sido aprobada, para editar</summary>
        UPD,
        /// <summary>Desestima la versión (elimina)</summary>
        RCH,
        /// <summary>Cambio de Fecha de Impacto</summary>
        FCH,

    }

    public enum VersionedLogState
    {
        /// <summary>Creacion Nueva Versión</summary>
        CNV = 1,
        /// <summary>Enviar a Bandejas</summary>
        EB = 2,
        /// <summary>Aplica Cambios Programados</summary>
        ACP = 3,
        /// <summary>Aplica Cambios en el momento</summary>
        ACM = 4,
        /// <summary>Desestima la versión (elimina)</summary>
        DV = 5,
        /// <summary>Cambio de Fecha de Impacto</summary>
        CFI = 6,
        /// <summary>Datos enviados a las Bandejas</summary>
        DEB = 7,
        /// <summary>Pasa a edición la Versión</summary>
        PEV = 8,
        /// <summary>Rechazo Versión</summary>
        RV = 9,
        /// <summary>Transformación Portal a Truck</summary>
        TPT = 10,
        /// <summary>Información General del Proceso</summary>
        IGP = 11,
        /// <summary>Error Api envio al Crear nueva Version</summary>
        ECNV = 100,
        /// <summary>Error Api Truck al enviar a las Bandejas </summary>
        EEB = 101,
        /// <<summary>Error Api Truck envio Aplica Cambios Progamados</summary>
        EACP = 102,
        /// <summary>Error Api Truck envio Aplica Cambios en el momento</summary>
        EACM = 103,
        /// <summary>Error Api Truck envio Desestima la Versión</summary>
        EDV = 104,
        /// <summary>Error Api Truck envio Cambio de fecha de Impacto</summary>
        ECFI = 105,
        /// <summary>Error en Bandeja, hay errores informados por truck</summary>
        EBT = 106,
        /// <summary>Error en transformación de Portal a Truck</summary>
        ETPT = 107,
        /// <summary>Estado Pendiente de envió</summary>    
        EPE = 50,
        /// <summary>Estado Procesando</summary>   
        EPR = 51,
        /// <summary>Estado Aceptado</summary> 
        EAC = 52,
        /// <summary>Estado Operativo </summary>   
        EOP = 53,
        /// <summary>Estado Rechazado </summary>   
        EREC = 54,
        /// <summary>Estado Cancelado </summary>   
        ECANC = 55,
        /// <summary>Estado Unificado </summary>   
        EUNIF = 56

    }


    public enum VersionedState
    {
        PendienteDeEnvio = 1,
        Procesando = 2,
        Aceptado = 3,
        Operativo = 4,
        Rechazado = 5,
        Cancelado = 6,
        Unificado = 7
    }

}
