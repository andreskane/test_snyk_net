namespace ABI.API.Structure.Domain.Enums
{
    public enum MotiveStateNode
    {
        /// <summary>
        /// Tabla Motivo_Estado - Borrador
        /// </summary>
        Draft = 1,
        /// <summary>
        /// Tabla Motivo_Estado - Confirmado
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// Tabla Motivo_Estado - Cancelado (por el usuario en la bandeja)
        /// </summary>
        Cancelled = 3,
        /// <summary>
        /// TTabla Motivo_Estado - Anulado (cuando un nodo es editado con la misma fecha)
        /// </summary>
        Dropped = 4
    }
}
