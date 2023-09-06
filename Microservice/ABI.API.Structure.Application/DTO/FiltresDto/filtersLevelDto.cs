using ABI.API.Structure.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.FiltresDto
{
    public  class filtersLevelDto: GenericKeyValue
    {
        public int levelId { get; set; }
        public List<Int32> parents { get; set; }
    }
}
