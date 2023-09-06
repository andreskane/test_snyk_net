using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.SaleChannel
{
    public class GetAllActiveOrderQuery : IRequest<IList<SaleChannelDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveOrderQueryHandler : IRequestHandler<GetAllActiveOrderQuery, IList<SaleChannelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISaleChannelRepository _repository;

        public GetAllActiveOrderQueryHandler(ISaleChannelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<SaleChannelDTO>> Handle(GetAllActiveOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.SaleChannel>, IList<SaleChannelDTO>>(results);
        }
    }
}
