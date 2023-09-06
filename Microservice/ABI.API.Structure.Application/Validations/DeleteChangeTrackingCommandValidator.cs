using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;

namespace ABI.API.Structure.Application.Validations
{
    public class DeleteChangeTrackingCommandValidator : DeleteChangeCommandValidator<DeleteChangeCommand>
    {
        public DeleteChangeTrackingCommandValidator(IChangeTrackingRepository changeTrackingRepo, IVersionedRepository versionedRepository) : base(changeTrackingRepo, versionedRepository)
        {
        }
    }
}
