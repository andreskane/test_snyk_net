using ABI.Framework.MS.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureModelDefinitionV2DTO
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int LevelId { get; set; }

        public virtual int? ParentLevelId { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual bool IsAttentionModeRequired { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual bool IsSaleChannelRequired { get; set; }

        public StructureModelDefinitionV2DTO()
        {
            IsAttentionModeRequired = false;
            IsSaleChannelRequired = false;
        }
    }
}
