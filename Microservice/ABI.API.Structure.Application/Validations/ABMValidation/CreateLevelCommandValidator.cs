using ABI.API.Structure.Application.Commands.Level;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class CreateLevelCommandValidator : NameCommandValidator<AddCommand, Level>
    {
        public CreateLevelCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
