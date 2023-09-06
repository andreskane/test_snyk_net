using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModels
{
    public class GetAllOrderV2Query : IRequest<IList<StructureModelV2DTO>>
    {
    }

    public class GetAllOrderV2QueryHandler : IRequestHandler<GetAllOrderV2Query, IList<StructureModelV2DTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repo;

        public GetAllOrderV2QueryHandler(IMapper mapper, IStructureModelRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<StructureModelV2DTO>> Handle(GetAllOrderV2Query request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAll();
            return _mapper.Map<IList<Domain.Entities.StructureModel>, IList<StructureModelV2DTO>>(results);
        }
    }
}
