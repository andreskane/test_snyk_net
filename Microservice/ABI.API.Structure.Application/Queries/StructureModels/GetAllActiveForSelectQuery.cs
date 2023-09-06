using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModels
{
    public class GetAllActiveForSelectQuery : IRequest<IList<ItemDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveForSelectQueryHandler : IRequestHandler<GetAllActiveForSelectQuery, IList<ItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repo;

        public GetAllActiveForSelectQueryHandler(IStructureModelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ItemDTO>> Handle(GetAllActiveForSelectQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.StructureModel>, IList<ItemDTO>>(results);
        }
    }
}
