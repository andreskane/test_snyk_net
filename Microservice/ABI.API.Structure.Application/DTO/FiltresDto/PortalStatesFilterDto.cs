using ABI.API.Structure.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class PortalStatesFilterDto : GenericKeyValue
    {

        public List<String> structureIds { get; set; }
        public List<String> kindOfChangeIds { get; set; }
        public List<String> userIds { get; set; }

    }
}
