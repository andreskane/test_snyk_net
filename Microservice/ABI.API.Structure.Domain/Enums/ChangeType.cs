using System.ComponentModel;

namespace ABI.API.Structure.Domain.Enums
{
    public enum ChangeType
    {
        [Description ("Estructura")]
        Structure = 4,
        [Description("Rol")]
        Role = 5,
        [Description("Persona")]
        Employee = 6
    }
}
