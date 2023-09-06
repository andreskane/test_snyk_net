using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public class EditStructureCommand : IRequest<int>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int Id { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(50, ErrorMessage = ErrorMessageText.StringLengthMax)]
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public virtual DateTimeOffset? ValidityFrom { get; private set; }

        [DataMember]
        public virtual string Code { get; private set; }

        public EditStructureCommand(int id, string name, DateTimeOffset? validityFrom, string code)
        {
            Id = id;
            Name = name;
            ValidityFrom = validityFrom;
            Code = code;
        }
    }

}
