using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes
{
    public class GetOneNodeDefinitionByIdQuery : IRequest<StructureNodeDefinition>
    {
        public int NodeDefinitionId { get; set; }     
    }

    public class GetOneNodeDefinitionByIdQueryHandler : IRequestHandler<GetOneNodeDefinitionByIdQuery, StructureNodeDefinition>
    {

        private readonly IStructureNodePortalRepository _repository;

        public GetOneNodeDefinitionByIdQueryHandler(IStructureNodePortalRepository repository)
        {
            _repository = repository;
        }

        public async Task<StructureNodeDefinition> Handle(GetOneNodeDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetNodeDefinitionsByIdAsync(request.NodeDefinitionId);
           
        }
    }
}
