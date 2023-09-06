using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Role
{
    public class DeleteCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IRoleRepository _repo;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepository;
        private readonly ITypeVendorTruckRepository _typeVendorTruckRepository;

        public DeleteCommandHandler(IRoleRepository repo, IAttentionModeRoleRepository attentionModeRoleRepository, ITypeVendorTruckRepository typeVendorTruckRepository)
        {
            _repo = repo;
            _attentionModeRoleRepository = attentionModeRoleRepository;
            _typeVendorTruckRepository = typeVendorTruckRepository;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var attentionModeRoles = await _attentionModeRoleRepository.GetAllByRoleId(request.Id);

            foreach (var item in attentionModeRoles)
            {
                if (item.RoleId.HasValue)
                {
                    var attentionModeRole = await _attentionModeRoleRepository.GetRoleById(item.RoleId.Value);
                    var typeVendorTruck = await _typeVendorTruckRepository.GetByAttentionModeRoleIdAsync(attentionModeRole.Id);

                    if (typeVendorTruck != null)
                        await _typeVendorTruckRepository.Delete(typeVendorTruck.Id);

                    await _attentionModeRoleRepository.Delete(attentionModeRole.Id);
                }
            }

            await _repo.Delete(request.Id);

            return Unit.Value;
        }
    }
}
