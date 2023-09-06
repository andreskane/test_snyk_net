using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class RequestTrayDetailDTO
    {
        public DateTimeOffset Made { get; set; }
        public IList<RequestTrayDetailChangesDTO> ChangesByNode { get; set; }
    }
}
