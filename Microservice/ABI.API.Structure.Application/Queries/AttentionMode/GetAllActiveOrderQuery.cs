using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode
{
    public class GetAllActiveOrderQuery : IRequest<IList<AttentionModeDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveOrderQueryHandler : IRequestHandler<GetAllActiveOrderQuery, IList<AttentionModeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repository;

        public GetAllActiveOrderQueryHandler(IAttentionModeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<AttentionModeDTO>> Handle(GetAllActiveOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.AttentionMode>, IList<AttentionModeDTO>>(results);
        }
    }
}
