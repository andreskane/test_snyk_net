using ABI.API.Structure.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public  class KindOfChangeFilterDto : GenericKeyValue
    {
      public  List<String> structureIds { get; set; }
    }
}
