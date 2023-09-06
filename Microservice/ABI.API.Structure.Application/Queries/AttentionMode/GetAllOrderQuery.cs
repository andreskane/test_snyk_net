using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode
{
    public class GetAllOrderQuery : IRequest<IList<AttentionModeDTO>>
    {
    }

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<AttentionModeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repository;

        public GetAllOrderQueryHandler(IAttentionModeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<AttentionModeDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAll();
            return _mapper.Map<IList<Domain.Entities.AttentionMode>, IList<AttentionModeDTO>>(results);
        }
    }
}
