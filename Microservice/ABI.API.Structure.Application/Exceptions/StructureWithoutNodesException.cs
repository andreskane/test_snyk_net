using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class StructureWithoutNodesException : DomainException
    {
        public StructureWithoutNodesException() : base("La estructura no contiene nodos")
        {

        }
    }
}
