using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModelDefinition
{
    public class GetAllOrderQuery : IRequest<IList<StructureModelDefinitionDTO>>
    {
    }

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<StructureModelDefinitionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelDefinitionRepository _repo;

        public GetAllOrderQueryHandler(IMapper mapper, IStructureModelDefinitionRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<StructureModelDefinitionDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAll();
            return _mapper.Map<IList<Domain.Entities.StructureModelDefinition>, IList<StructureModelDefinitionDTO>>(results);
        }
    }
}
