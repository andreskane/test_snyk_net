using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModels
{
    public class GetAllActiveOrderQuery : IRequest<IList<StructureModelDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveOrderQueryHandler : IRequestHandler<GetAllActiveOrderQuery, IList<StructureModelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repository;

        public GetAllActiveOrderQueryHandler(IStructureModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<StructureModelDTO>> Handle(GetAllActiveOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.StructureModel>, IList<StructureModelDTO>>(results);
        }
    }
}
