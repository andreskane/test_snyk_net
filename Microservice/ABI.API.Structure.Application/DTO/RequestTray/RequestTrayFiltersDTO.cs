using ABI.API.Structure.Application.DTO.FiltresDto;
using ABI.API.Structure.Domain.Entities;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class RequestTrayFiltersDTO
    {
        

        public IList<StructureFilterDto> Structures { get; set; }
        public IList<KindOfChangeFilterDto> KindOfChanges { get; set; }
        public IList<UserFilterDto> Users { get; set; }
        public IList<PortalStatesFilterDto> portalStates { get; set; }
        public IList<ExternalSystemsFilterDto> externalSystems { get; set; }

        public IList<filtersLevelDto> filters { get; set; }

        public IList<GenericKeyValue> levels { get; set; }


    }
}
