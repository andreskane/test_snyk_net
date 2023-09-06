using ABI.API.Structure.Application.DTO;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Exceptions
{
    public class GenericListException: Exception
    {
        protected IList<GenericValueDescriptionDto> _listObject;
        public IList<GenericValueDescriptionDto> listObject => _listObject;

        public int Code { get; set; }

        public GenericListException() { }

        public GenericListException(string message) : base(message) { }

    }
}
