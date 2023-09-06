using ABI.Framework.MS.Domain.Exceptions;

namespace ABI.API.Structure.Application.Exceptions
{
    public class StructureCodeExistsException : DomainException
    {
        private const string _msg = "El código ya existe";

        public StructureCodeExistsException() : base(_msg) { }
    }
}
