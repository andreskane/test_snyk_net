using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class RequestTrayDetailChangesDTO
    {
        public ItemNodeDTO Node { get; set; }
        public IList<RequestTrayDetailChangeValuesDTO> Changes { get; set; }
    }
}
