using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;

using AutoMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureClientNodes
{
    public class AddClientNodeCommand : StructureClientDTO, IRequest<int>
    {
    }
    public class AddClientNodeCommandHandler : IRequestHandler<AddClientNodeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IStructureClientRepository _repo;

        public AddClientNodeCommandHandler(IStructureClientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddClientNodeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<StructureClientNode>(request);

            await _repo.Create(entity,cancellationToken);
            return entity.Id;
        }
    }
}
