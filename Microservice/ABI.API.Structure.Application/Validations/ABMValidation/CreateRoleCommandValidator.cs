using ABI.API.Structure.Application.Commands.Role;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class CreateRoleCommandValidator : NameCommandValidator<AddCommand, Role>
    {
        public CreateRoleCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
