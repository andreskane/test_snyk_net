using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace ABI.API.Structure.Application.Validations
{
    public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
    {
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;


        public CreateNodeCommandValidator(IStructureNodeRepository repositoryStructureNode, IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _repositoryStructureNode = repositoryStructureNode;
            _attentionModeRoleRepo = attentionModeRoleRepo;

            //Pedido por el cliente 30/08 - AB#904320
            //Include(new CreateUpdateNodeEmployeeResponsableZonesValidator(_repositoryStructureNode, _attentionModeRoleRepo));
            //Include(new CreateUpdateNodeEmployeeResponsableTerritoriesValidator(_repositoryStructureNode, _attentionModeRoleRepo));
        }
    }
}
