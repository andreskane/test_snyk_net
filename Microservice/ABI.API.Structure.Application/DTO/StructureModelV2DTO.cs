using ABI.Framework.MS.Helpers.Message;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureModelV2DTO
    {
        public virtual int? Id { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(50, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(10, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public virtual string ShortName { get; set; }

        [StringLength(140, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual bool Active { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public bool CanBeExportedToTruck { get; set; }

        public virtual bool? Erasable { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(3, ErrorMessage = ErrorMessageText.StringLengthMax)]
        public string Code { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int CountryId { get; set; }

        public virtual List<StructureModelDefinitionV2DTO> Definitions { get; set; }
        public StructureModelV2DTO()
        {
            //Devuelve true si se puede eliminar, o false si no se puede eliminar
            Erasable = true;
        }
    }
}
