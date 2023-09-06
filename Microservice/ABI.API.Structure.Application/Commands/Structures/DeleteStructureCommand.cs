using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public class DeleteStructureCommand : IRequest<bool>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int StructureId { get; private set; }

        public DeleteStructureCommand(int structureId)
        {
            StructureId = structureId;
        }

    }

}
