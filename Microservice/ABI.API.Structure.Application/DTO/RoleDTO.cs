using ABI.Framework.MS.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.DTO
{
    public class RoleDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(50, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(10, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public string ShortName { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public bool Active { get; set; }

        public string[] Tag { get; set; }

        public virtual bool? Erasable { get; set; }

        public ItemDTO AttentionMode { get; set; }


        public RoleDTO()
        {
            //TODO Devuelve true si se puede eliminar, o false si no se puede eliminar
            Erasable = true;
        }
    }
}
