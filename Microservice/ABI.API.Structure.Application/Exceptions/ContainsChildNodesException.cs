using ABI.Framework.MS.Domain.Exceptions;
using System;

namespace ABI.API.Structure.Application.Exceptions
{
    [Serializable]
    public class ContainsChildNodesException : DomainException
    {
        public ContainsChildNodesException() : base("No se puede eliminar, contiene niveles.")
        {

        }
    }
}
