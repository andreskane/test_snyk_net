using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureQuantityAndClientDto
    {
        public virtual int Quantity { get; set; }
        public virtual List<StructureClientDTO> Clients { get; set; }
        public StructureQuantityAndClientDto()
        {
            Clients = new List<StructureClientDTO>();
        }
    }
}
