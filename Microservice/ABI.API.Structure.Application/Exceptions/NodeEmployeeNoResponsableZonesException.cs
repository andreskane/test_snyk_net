using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class NodeEmployeeNoResponsableZonesException : DomainException
    {
        public NodeEmployeeNoResponsableZonesException() : base("La persona asignada debe ser no responsable en todos los territorios de todas sus zonas")
        {
        }
    }
}
