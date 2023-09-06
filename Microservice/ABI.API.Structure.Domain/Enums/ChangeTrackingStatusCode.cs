using System.ComponentModel;

namespace ABI.API.Structure.Domain.Enums
{
    public enum ChangeTrackingStatusCode
    {
        /// <summary>
        /// Tabla Estado - Confirmado
        /// </summary>
        Confirmed = 5,
        /// <summary>
        /// Tabla Estado - Cancelado (por el usuario en la bandeja)
        /// </summary>
        Cancelled = 6
    }

    //todo: esto va a ser siempre asi, es una constante??
    public enum StatusPortal
    {
        [Description("Programado")]
        Programado = 1,
        [Description("Realizado")]
        Realizado = 2,
        [Description("Cancelado")]
        Cancelado = 3
    }
    public enum StatusPortalExternal
    {


        [Description("Operativo")]
        Operativo = 1,
        [Description("Pendiente de envío")]
        Pendiente_envio = 2,
        [Description("Aceptado")]
        Aceptado = 3,
        [Description("-")]
        NoExterno = 4,
        [Description("Sin conexió")]
        Sin_conexion = 5,
        [Description("Rechazado")]
        Rechazado = 6,
        [Description("Cancelado")]
        Cancelado = 7


        //si es "null" se muestra una rayita
    }



}
