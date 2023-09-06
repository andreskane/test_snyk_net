using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.Domain.Entities
{
    public class StructureModelDefinition : BaseEntity<int>
    {
        public int StructureModelId { get; set; }
        public int LevelId { get; set; }
        public int? ParentLevelId { get; set; }
        public bool IsAttentionModeRequired { get; set; }
        public bool IsSaleChannelRequired { get; set; }
        public StructureModel StructureModel { get; set; }
        public Level Level { get; set; }
        public Level ParentLevel { get; set; }


    }
}
