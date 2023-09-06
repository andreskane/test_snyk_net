using ABI.Framework.MS.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureModelDefinitionDTO
    {
        public virtual int? Id { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int StructureModelId { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int LevelId { get; set; }

        public virtual int? ParentLevelId { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual bool IsAttentionModeRequired { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual bool IsSaleChannelRequired { get; set; }

        public virtual bool? Erasable { get; set; }
        public virtual LevelDTO Level { get; set; }

        public StructureModelDefinitionDTO()
        {
            Erasable = true;
            IsAttentionModeRequired = false;
            IsSaleChannelRequired = false;
        }
    }
}
