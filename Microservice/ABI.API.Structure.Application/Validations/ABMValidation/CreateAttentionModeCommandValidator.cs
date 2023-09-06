using ABI.API.Structure.Application.Commands.AttentionMode;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class CreateAttentionModeCommandValidator : NameCommandValidator<AddCommand, AttentionMode>
    {
        public CreateAttentionModeCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
