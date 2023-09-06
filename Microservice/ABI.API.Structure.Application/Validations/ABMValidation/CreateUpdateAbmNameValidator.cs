using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure;
using ABI.Framework.MS.Entity;
using ABI.Framework.MS.Service.Exceptions;
using FluentValidation;
using System.Linq;

namespace ABI.API.Structure.Application.Validations.ABMValidation
{
    public class CreateUpdateAbmNameValidator<TCommand, TEntity> : AbstractValidator<TCommand> where TCommand : INameValidator where TEntity : BaseEntity<int>
    {
        private readonly StructureContext _context;

        public CreateUpdateAbmNameValidator(StructureContext context)
        {
            _context = context;

            RuleFor(command => command)
                .Must(ValidateName)
                .OnFailure(f => throw new NameExistsException());
        }
        /// <summary>
        /// Si el comando tiene un ID es que se está editando un registro. Sino se está creando uno nuevo.
        /// Si el contexto devuelve valor es que existe entonces NO pasa la validación.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ValidateName(TCommand command)
        {
            if (command.Id.HasValue && command.Id.Value >0)
                return !_context.Set<TEntity>().Any(a => a.Name.ToUpper() == command.Name.ToUpper() && a.Id != command.Id.Value);
            else
                return !_context.Set<TEntity>().Any(a => a.Name.ToUpper() == command.Name.ToUpper());
        }
    }
}
