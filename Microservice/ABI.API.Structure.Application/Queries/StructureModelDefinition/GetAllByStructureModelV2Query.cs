using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModelDefinition
{
    public class GetAllByStructureModelV2Query : IRequest<IList<StructureModelDefinitionV2DTO>>
    {
        public int Id { get; set; }
    }

    public class GetAllByStructureModelV2QueryHandler : IRequestHandler<GetAllByStructureModelV2Query, IList<StructureModelDefinitionV2DTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelDefinitionRepository _repo;

        public GetAllByStructureModelV2QueryHandler(IMapper mapper, IStructureModelDefinitionRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<StructureModelDefinitionV2DTO>> Handle(GetAllByStructureModelV2Query request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAllByStructureModelWithOutLevel(request.Id);
            return _mapper.Map<IList<Domain.Entities.StructureModelDefinition>, IList<StructureModelDefinitionV2DTO>>(results);
        }
    }
}
