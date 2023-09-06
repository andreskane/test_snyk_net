using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class MasterDTO
    {
        public List<StructureModelV2DTO> structureModels { get; set; }
        public List<RoleDTO> roles { get; set; }
        public List<ItemDTO> saleChannels { get; set; }
        public List<ItemDTO> attentionModes { get; set; }
        public List<ItemDTO> levels { get; set; }

        public MasterDTO()
        {
            structureModels = new List<StructureModelV2DTO>();
            roles = new List<RoleDTO>();
            saleChannels = new List<ItemDTO>();
            attentionModes = new List<ItemDTO>();
            levels = new List<ItemDTO>();
        }
    }
}
