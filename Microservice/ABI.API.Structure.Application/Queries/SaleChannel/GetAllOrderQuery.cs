using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.SaleChannel
{
    public class GetAllOrderQuery : IRequest<IList<SaleChannelDTO>>
    {
    }

    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<SaleChannelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISaleChannelRepository _repository;

        public GetAllOrderQueryHandler(ISaleChannelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<SaleChannelDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAll();
            return _mapper.Map<IList<Domain.Entities.SaleChannel>, IList<SaleChannelDTO>>(results);
        }
    }
}
