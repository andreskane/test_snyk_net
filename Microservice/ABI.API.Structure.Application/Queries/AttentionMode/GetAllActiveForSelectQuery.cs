using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode
{
    public class GetAllActiveForSelectQuery : IRequest<IList<ItemDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveForSelectQueryHandler : IRequestHandler<GetAllActiveForSelectQuery, IList<ItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repository;

        public GetAllActiveForSelectQueryHandler(IAttentionModeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ItemDTO>> Handle(GetAllActiveForSelectQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.AttentionMode>, IList<ItemDTO>>(results);
        }
    }
}
