using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModelDefinition
{
    public class GetByIdQuery : IRequest<StructureModelDefinitionDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, StructureModelDefinitionDTO>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelDefinitionRepository _repo;

        public GetByIdQueryHandler(IMapper mapper, IStructureModelDefinitionRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<StructureModelDefinitionDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetById(request.Id);
            return _mapper.Map<Domain.Entities.StructureModelDefinition, StructureModelDefinitionDTO>(results);
        }
    }
}
