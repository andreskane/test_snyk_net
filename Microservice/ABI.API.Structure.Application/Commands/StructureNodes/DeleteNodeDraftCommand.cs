using ABI.Framework.MS.Helpers.Message;
using MediatR;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class DeleteNodeDraftCommand : IRequest<int>
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int NodeDefinitionId { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int StructureId { get; private set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public DateTimeOffset ValidityFrom { get; private set; }
 
        public DeleteNodeDraftCommand()
        {

        }

        public DeleteNodeDraftCommand(int nodeDefinitionId, int structureId, DateTimeOffset validityFrom)
        {
            NodeDefinitionId = nodeDefinitionId;
            StructureId = structureId;
            ValidityFrom = validityFrom;
        }
    }
}
