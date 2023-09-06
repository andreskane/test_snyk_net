using ABI.Framework.MS.Helpers.Message;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.Commands.Entities
{
    public class ValidateDateStructure
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int StructureId { get; set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? ValidateDate { get; set; }
    }
}
