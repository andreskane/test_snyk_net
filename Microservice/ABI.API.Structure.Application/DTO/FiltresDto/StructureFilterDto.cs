using ABI.API.Structure.Domain.Entities;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureListFilterDto  
    {
        public IList<GenericKeyValue> StructureListFilter { get; set; }
        public StructureListFilterDto() => StructureListFilter = new List<GenericKeyValue>();
         
    }
    public class StructureFilterDto: GenericKeyValue
    {
      

    }
}
