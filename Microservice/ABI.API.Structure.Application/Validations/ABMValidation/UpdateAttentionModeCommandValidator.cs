using ABI.API.Structure.Application.Commands.AttentionMode;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class UpdateAttentionModeCommandValidator : NameCommandValidator<UpdateCommand, AttentionMode>
    {
        public UpdateAttentionModeCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
