using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class SaveChangesWithoutSavingCommand : IRequest<bool>
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int StructureId { get; private set; }
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset ValidityFrom { get; private set; }

        public SaveChangesWithoutSavingCommand(int structureId, DateTimeOffset validity)
        {
            StructureId = structureId;
            ValidityFrom = validity;
        }
    }
}
