using ABI.API.Structure.Application.Commands.Level;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class UpdateLevelCommandValidator : NameCommandValidator<UpdateCommand, Level>
    {
        public UpdateLevelCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
