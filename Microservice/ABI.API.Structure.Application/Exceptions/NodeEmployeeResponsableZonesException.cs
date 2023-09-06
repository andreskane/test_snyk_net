using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeEmployeeResponsableZonesException : DomainException
    {
        public NodeEmployeeResponsableZonesException() : base("La persona asignada debe ser responsable en todos los territorios de todas sus zonas")
        {
        }
    }
}
