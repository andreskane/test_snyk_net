using System;

namespace ABI.API.Structure.Application.Exceptions
{
    public class InteropCallingException : Exception
    {
        private const string _msg = "No es posible acceder al recurso externo";

        public InteropCallingException() : base(_msg) { }
    }
}
