﻿using ABI.Framework.MS.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.DTO
{
    public class LevelDTO
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

        public virtual bool? Erasable { get; set; }

        public LevelDTO()
        {
            //TODO Devuelve true si se puede eliminar, o false si no se puede eliminar
            Erasable = true;

        }

    }
}
