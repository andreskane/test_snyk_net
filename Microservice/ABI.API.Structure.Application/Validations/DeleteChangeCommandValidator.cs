using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;

using FluentValidation;

namespace ABI.API.Structure.Application.Validations
{
    public class DeleteChangeCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : IDeleteChange
    {
        public DeleteChangeCommandValidator(IChangeTrackingRepository changeTrackingRepository, IVersionedRepository versionedRepository)
        {
            Include(new DeleteChangeTrackingDatesValidator<TCommand>(changeTrackingRepository, versionedRepository));
        }
    }
}
