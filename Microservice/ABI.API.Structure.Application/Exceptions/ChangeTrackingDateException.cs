using ABI.API.Structure.Application.DTO;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Exceptions
{
    public class ChangeTrackingDateException : GenericListException
    {
        public ChangeTrackingDateException(IList<GenericValueDescriptionDto> listChange) : base("Hay cambios futuros programados.")
        {
            _listObject = listChange;
        }
    }
}
