using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class DeleteNodeCommand : IRequest<int>
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public int? NodeIdParent { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int StructureId { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public DateTimeOffset ValidityFrom { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public DateTimeOffset ValidityTo { get; private set; }

        public DeleteNodeCommand(int id, int? nodeIdParent, int structureId, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            Id = id;
            NodeIdParent = nodeIdParent;
            StructureId = structureId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
        }
    }
}
