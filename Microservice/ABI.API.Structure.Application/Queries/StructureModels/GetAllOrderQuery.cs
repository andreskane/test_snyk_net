using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModels
{
    public class GetAllOrderQuery : IRequest<IList<StructureModelDTO>>
    {
    }

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<StructureModelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repo;

        public GetAllOrderQueryHandler(IMapper mapper, IStructureModelRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<StructureModelDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAll();
            return _mapper.Map<IList<Domain.Entities.StructureModel>, IList<StructureModelDTO>>(results);
        }
    }
}
