using ABI.Framework.MS.Domain.Exceptions;
using System;

namespace ABI.API.Structure.Application.Exceptions
{
    public class PropertyException : DomainException
    {
        protected  string _property;

        public string Property => _property;


        public PropertyException() { }

        public PropertyException(string message) : base(message) { }

        public PropertyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
