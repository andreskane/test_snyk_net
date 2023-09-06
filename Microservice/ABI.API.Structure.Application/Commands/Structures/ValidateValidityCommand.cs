using ABI.API.Structure.Application.Commands.Entities;
using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public class ValidateValidityCommand : IRequest<ValidateDateStructure>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int StructureId { get; private set; }

        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual DateTimeOffset Validity { get; private set; }


        public ValidateValidityCommand(int structureId, DateTimeOffset validityFrom)
        {
            StructureId = structureId;
            Validity = validityFrom;
        }

        public ValidateValidityCommand()
        {
        }
    }

}
