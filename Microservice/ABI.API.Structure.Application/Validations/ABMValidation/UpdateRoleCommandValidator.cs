using ABI.API.Structure.Application.Commands.Role;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class UpdateRoleCommandValidator : NameCommandValidator<UpdateCommand, Role>
    {
        public UpdateRoleCommandValidator(StructureContext context) : base(context)
        {
        }
    }
}
