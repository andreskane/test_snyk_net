using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure;
using ABI.Framework.MS.Entity;
using FluentValidation;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class NameCommandValidator<TCommand, TEntity> : AbstractValidator<TCommand> where TCommand : INameValidator where TEntity : BaseEntity<int>
    {
        public NameCommandValidator(StructureContext context)
        {
            Include(new CreateUpdateAbmNameValidator<TCommand, TEntity>(context));
        }
    }
}
